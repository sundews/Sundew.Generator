// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GeneratorFinalizer.cs" company="Hukano">
// Copyright (c) Hukano. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sundew.Generator.Engine.Internal;

using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using Sundew.Base.Collections;
using Sundew.Base.Primitives.Computation;
using Sundew.Generator.Engine.Internal.Output;
using Sundew.Generator.Reporting;

internal static class GeneratorFinalizer
{
    public static Task FinalizeAsync(IEnumerable<TargetRun> targetRuns, IProgressTracker<Report> progressTracker, ConcurrentBag<string> outputs)
    {
        return targetRuns.ForEachAsync(targetRun => FinalizeOutputsAsync(targetRun, progressTracker, outputs));
    }

    private static async Task FinalizeOutputsAsync(TargetRun targetRun, IProgressTracker<Report> progressTracker, ConcurrentBag<string> outputs)
    {
        await targetRun.Writer.PrepareTargetAsync(targetRun.Target, targetRun.WriterSetup).ConfigureAwait(false);
        foreach (var targetRunGeneratorRun in targetRun.GeneratorRuns)
        {
            for (int i = 0; i < targetRunGeneratorRun.Outputs.Length; i++)
            {
                var result = await targetRun.Writer.ApplyContentToTargetAsync(targetRunGeneratorRun.Target, targetRunGeneratorRun.Runs[i], targetRun.WriterSetup, targetRunGeneratorRun.Outputs[i]).ConfigureAwait(false);
                outputs.Add(result);
                progressTracker.CompleteItem(new Report(ReportType.AppliedContent, result));
            }
        }

        await targetRun.Writer.CompleteTargetAsync(new ProgressTrackerToTargetCompletionAdapter(progressTracker)).ConfigureAwait(false);
        progressTracker.CompleteItem(new Report(ReportType.CompletedTarget, targetRun.WriterSetup.Target));
    }
}