// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GeneratorFacade.cs" company="Hukano">
// Copyright (c) Hukano. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sundew.Generator;

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Sundew.Base.Primitives.Computation;
using Sundew.Generator.Core;
using Sundew.Generator.Discovery;
using Sundew.Generator.Reporting;

/// <summary>
/// Facade for using generation.
/// </summary>
public static class GeneratorFacade
{
    /// <summary>
    /// Runs the asynchronous.
    /// </summary>
    /// <param name="setups">The setups.</param>
    /// <returns>
    /// A list of output names.
    /// </returns>
    public static Task<ConcurrentBag<string>> RunAsync(params ISetup[] setups)
    {
        return RunAsync(null, setups);
    }

    /// <summary>
    /// Runs the asynchronous.
    /// </summary>
    /// <param name="generatorOptions">The generator options.</param>
    /// <param name="setups">The setups.</param>
    /// <returns>
    /// A list of output names.
    /// </returns>
    public static Task<ConcurrentBag<string>> RunAsync(GeneratorOptions? generatorOptions, params ISetup[] setups)
    {
        return RunAsync(new Setups(setups), generatorOptions);
    }

    /// <summary>
    /// Runs the asynchronous.
    /// </summary>
    /// <param name="setupsFactory">The setups factory.</param>
    /// <param name="generatorOptions">The generator options.</param>
    /// <returns>
    /// A list of output names.
    /// </returns>
    public static async Task<ConcurrentBag<string>> RunAsync(ISetupsFactory setupsFactory, GeneratorOptions? generatorOptions = null)
    {
        generatorOptions ??= GeneratorOptions.Default;
        using var progressReporter = new ProgressReporterFactory().Create(generatorOptions.ProgressReporter, generatorOptions.ProgressTextWriter);
        try
        {
            progressReporter.Start();
            var generatorRunnerFactory = new GeneratorRunnerFactory();
            var generatorRunner = generatorRunnerFactory.Create((await setupsFactory.GetSetupsAsync().ConfigureAwait(false))
                .Select(x => new SetupInfo(setupsFactory.GetType().ToString(), x)));

            return await generatorRunner.GenerateAsync(generatorOptions, new ProgressTracker<Report>(progressReporter)).ConfigureAwait(false);
        }
        catch (Exception)
        {
            progressReporter.Stop();
            throw;
        }
    }

    /// <summary>
    /// Runs the specified directory.
    /// </summary>
    /// <param name="directory">The directory.</param>
    /// <param name="pattern">The pattern.</param>
    /// <param name="generatorOptions">The generator options.</param>
    /// <returns>
    /// A list of output names.
    /// </returns>
    public static async Task<ConcurrentBag<string>> RunAsync(string directory, string pattern, GeneratorOptions? generatorOptions = null)
    {
        generatorOptions ??= GeneratorOptions.Default;
        using var progressReporter = new ProgressReporterFactory().Create(generatorOptions.ProgressReporter, generatorOptions.ProgressTextWriter);
        try
        {
            progressReporter.Start();
            directory = string.IsNullOrEmpty(directory)
                ? Environment.CurrentDirectory
                : Path.IsPathRooted(directory)
                    ? directory
                    : Path.Combine(Environment.CurrentDirectory, directory);
            var searchPattern = string.IsNullOrEmpty(pattern) ? "*.gs.json" : pattern;
            var generatorDiscoverer = new SetupDiscoverer(new SetupFileFinder(), new JsonSetupProvider());
            var setupInfos = await generatorDiscoverer.DiscoverAsync(directory, searchPattern).ConfigureAwait(false);
            var generatorRunnerFactory = new GeneratorRunnerFactory();
            var generatorRunner = generatorRunnerFactory.Create(setupInfos);

            return await generatorRunner.GenerateAsync(generatorOptions, new ProgressTracker<Report>(progressReporter)).ConfigureAwait(false);
        }
        catch (Exception)
        {
            progressReporter.Stop();
            throw;
        }
    }

    private class Setups : ISetupsFactory
    {
        private readonly ISetup[] setups;

        public Setups(ISetup[] setups)
        {
            this.setups = setups;
        }

        public Task<IEnumerable<ISetup>> GetSetupsAsync()
        {
            return Task.FromResult((IEnumerable<ISetup>)this.setups);
        }
    }
}