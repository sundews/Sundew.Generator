// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Program.cs" company="Hukano">
// Copyright (c) Hukano. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sundew.Generator.CommandLine
{
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using Sundew.Base.Computation;
    using Sundew.CommandLine;
    using Sundew.Generator.ModelHandling;
    using Sundew.Generator.ProgressReporting;

    /// <summary>
    /// The Text generator program.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// Mains the specified arguments.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <returns>The result value.</returns>
        public static int Main(string[] args)
        {
            try
            {
                var commandLineParser = new CommandLineParser<int, int>();
                commandLineParser.WithArguments(new Arguments(), x =>
                {
                    Execute(x).Wait();
                    return Result.Success(0);
                });

                var result = commandLineParser.Parse(args);
                if (!result)
                {
                    result.WriteToConsole();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            return 0;
        }

        private static async Task Execute(Arguments arguments)
        {
            using (var progressReporter = new ConsoleProgressReporter())
            {
                progressReporter.Start();
                var directory = string.IsNullOrEmpty(arguments.Directory)
                ? Environment.CurrentDirectory
                : Path.IsPathRooted(arguments.Directory)
                    ? arguments.Directory
                    : Path.Combine(Environment.CurrentDirectory, arguments.Directory);
                var searchPattern = string.IsNullOrEmpty(arguments.Pattern) ? "*.gs.json" : arguments.Pattern;
                var generatorDiscoverer = new SetupDiscoverer(new SetupFileFinder(), new JsonSetupProvider());
                var setupInfos = await generatorDiscoverer.DiscoverAsync(directory, searchPattern);
                var generatorRunnerFactory = new GeneratorRunnerFactory(new JsonModelProviderFactory());
                var generatorRunner = generatorRunnerFactory.Create(setupInfos);

                await generatorRunner.GenerateAsync(new ProgressTracker<Report>(progressReporter));
            }
        }
    }
}
