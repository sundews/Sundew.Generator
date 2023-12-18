// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GeneratorRunner.cs" company="Sundews">
// Copyright (c) Sundews. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sundew.Generator.Engine.Internal;

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Sundew.Base.Collections;
using Sundew.Base.Computation;
using Sundew.Generator.Core;
using Sundew.Generator.Engine.Internal.Output;
using Sundew.Generator.Reporting;

internal class GeneratorRunner : IGeneratorRunner
{
    private readonly IReadOnlyList<WriterInfo> writerInfos;

    public GeneratorRunner(IReadOnlyList<WriterInfo> writerInfos)
    {
        this.writerInfos = writerInfos;
    }

    public async Task<ConcurrentBag<string>> GenerateAsync(IGeneratorOptions generatorOptions, IProgressTracker<Report>? progressTracker = null)
    {
        progressTracker ??= IgnoringProgressReporter.Default;
        var result = new ConcurrentBag<string>();
        var generatorRuns = new BlockingCollection<GeneratorRun>(new ConcurrentBag<GeneratorRun>());

        try
        {
            var cancellationToken = generatorOptions.CancellationToken;
            progressTracker.Report(new Report(ReportType.StartingGeneration));
            var targetRuns = new ConcurrentBag<TargetRun>();
            var prepareTask = Task.Run(async () => await this.PrepareAsync(generatorRuns, targetRuns, progressTracker, cancellationToken).ConfigureAwait(false), cancellationToken);
            var generateTask = Task.Run(async () => await this.GenerateAsync(generatorRuns, progressTracker, cancellationToken).ConfigureAwait(false), cancellationToken);
            await Task.WhenAll(prepareTask, generateTask).ConfigureAwait(false);
            if (cancellationToken.IsCancellationRequested)
            {
                progressTracker.Report(new Report(ReportType.Cancelled));
                return new ConcurrentBag<string>();
            }

            await FinalizeAsync(progressTracker, targetRuns, result).ConfigureAwait(false);
            progressTracker.Report(new Report(ReportType.CompletedGeneration));
            return result;
        }
        catch (Exception exception)
        {
            generatorRuns.CompleteAdding();
            progressTracker.Report(new Report(ReportType.Error, exception));
            throw new GenerationException("Error", exception);
        }
    }

    private static async Task<TargetRun?> CreateTargetRunAsync(
        WriterInfo? writerInfo,
        BlockingCollection<GeneratorRun> generatorRuns,
        IProgressTracker<Report> progressTracker)
    {
        if (writerInfo == null)
        {
            return null;
        }

        var targetGeneratorRuns = new List<GeneratorRun>();
        var target = await writerInfo.Writer.GetTargetAsync(writerInfo.WriterSetup).ConfigureAwait(false);

        foreach (var generatorInfo in writerInfo.GeneratorInfos)
        {
            var modelInfos = await generatorInfo.ModelCache.GetModelInfosAsync().ConfigureAwait(false);
            foreach (var modelInfo in modelInfos)
            {
                var runs = generatorInfo.Generator.Prepare(
                    generatorInfo.Setup,
                    generatorInfo.GeneratorSetup,
                    target,
                    modelInfo.Model,
                    modelInfo.ModelOrigin);
                progressTracker.AddItems(runs.Count * 2, new Report(ReportType.AddingItems, runs.Count * 2));
                var generatorRun = new GeneratorRun(
                    generatorInfo.Setup,
                    generatorInfo.GeneratorSetup,
                    generatorInfo.Generator,
                    target,
                    modelInfo,
                    runs);
                generatorRuns.Add(generatorRun);
                targetGeneratorRuns.Add(generatorRun);
            }
        }

        progressTracker.AddItems(1, new Report(ReportType.AddingItems, 1));
        return new TargetRun(writerInfo.WriterSetup, writerInfo.Writer, target, targetGeneratorRuns);
    }

    private static Task FinalizeAsync(IProgressTracker<Report> progressTracker, IReadOnlyCollection<TargetRun> targetRuns, ConcurrentBag<string> outputs)
    {
        return GeneratorFinalizer.FinalizeAsync(targetRuns, progressTracker, outputs);
    }

    private async Task PrepareAsync(BlockingCollection<GeneratorRun> generatorRuns, ConcurrentBag<TargetRun> targetRuns, IProgressTracker<Report> progressTracker, CancellationToken cancellationToken)
    {
        try
        {
            await this.writerInfos.ForEachAsync(async writerInfo =>
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    return;
                }

                var targetRun = await CreateTargetRunAsync(writerInfo, generatorRuns, progressTracker).ConfigureAwait(false);
                if (targetRun != null)
                {
                    targetRuns.Add(targetRun);
                }
            }).ConfigureAwait(false);
        }
        finally
        {
            generatorRuns.CompleteAdding();
            progressTracker.CompleteAdding(new Report(ReportType.CompletedAdding));
        }
    }

    private async Task GenerateAsync(BlockingCollection<GeneratorRun> generatorRuns, IProgressTracker<Report> progressTracker, CancellationToken cancellationToken)
    {
        while (!generatorRuns.IsCompleted)
        {
            if (generatorRuns.Count == 0)
            {
                await Task.Delay(10, cancellationToken).ConfigureAwait(false);
                continue;
            }

            var partitioner = Partitioner.Create(generatorRuns.GetConsumingEnumerable(), EnumerablePartitionerOptions.NoBuffering);
            var result = Parallel.ForEach(partitioner, (preparedGeneratorRun, state) =>
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    state.Stop();
                    return;
                }

                preparedGeneratorRun.Runs.ForEach((run, runIndex) =>
                {
                    preparedGeneratorRun.Outputs[runIndex] = preparedGeneratorRun.Generator.Generate(
                        preparedGeneratorRun.Setup,
                        preparedGeneratorRun.GeneratorSetup,
                        preparedGeneratorRun.Target,
                        preparedGeneratorRun.ModelInfo.Model,
                        run,
                        runIndex);

                    progressTracker.CompleteItem(new Report(ReportType.GeneratedItem, run.Name));
                });
            });
        }
    }
}