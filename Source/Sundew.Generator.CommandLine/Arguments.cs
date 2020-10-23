// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Arguments.cs" company="Hukano">
// Copyright (c) Hukano. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sundew.Generator.CommandLine
{
    using System.Globalization;
    using Sundew.CommandLine;

    /// <summary>
    /// Arguments for the generator command line.
    /// </summary>
    public class Arguments : IArguments
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Arguments"/> class.
        /// </summary>
        public Arguments()
            : this(string.Empty, string.Empty, 0)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Arguments" /> class.
        /// </summary>
        /// <param name="directory">The directory.</param>
        /// <param name="pattern">The pattern.</param>
        /// <param name="maxDegreeOfParallelism">The degree of parallelism.</param>
        public Arguments(string directory, string pattern, int maxDegreeOfParallelism)
        {
            this.Directory = directory;
            this.Pattern = pattern;
            this.MaxDegreeOfParallelism = maxDegreeOfParallelism;
        }

        /// <summary>
        /// Gets the directory.
        /// </summary>
        /// <value>
        /// The directory.
        /// </value>
        public string Directory { get; private set; }

        /// <summary>
        /// Gets the pattern.
        /// </summary>
        /// <value>
        /// The pattern.
        /// </value>
        public string Pattern { get; private set; }

        /// <summary>
        /// Gets a value indicating the max degree of parallelism.
        /// A positive value specifies the number of threads.
        /// A negative value is subtracted from the available logical processors.
        /// Both positive and negative values will be limited by the actual amount of logical processors.
        /// </summary>
        /// <value>
        ///   <c>true</c> if [run sequentially]; otherwise, <c>false</c>.
        /// </value>
        public int MaxDegreeOfParallelism { get; private set; }

        /// <summary>
        /// Configures the specified arguments builder.
        /// </summary>
        /// <param name="argumentsBuilder">The arguments builder.</param>
        public void Configure(IArgumentsBuilder argumentsBuilder)
        {
            argumentsBuilder.AddOptional("d", "directory", () => this.Directory, x => this.Directory = x, "The directory to scan.");
            argumentsBuilder.AddOptional("p", "pattern", () => this.Pattern, x => this.Pattern = x, "The file pattern.");
            argumentsBuilder.AddOptional(
                "mp",
                "maxparallelism",
                () => this.MaxDegreeOfParallelism.ToString(CultureInfo.InvariantCulture),
                s => this.MaxDegreeOfParallelism = int.Parse(s),
                "Specifies the maximum degree parallelism used for processing generators");
        }
    }
}