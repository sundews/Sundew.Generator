// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GeneratorSetup{TWriterSetup}.cs" company="Hukano">
// Copyright (c) Hukano. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sundew.Generator.Core
{
    using System.Collections.Generic;
    using Sundew.Generator.Output;

    /// <summary>
    /// Minimal implementation of <see cref="IGeneratorSetup" />.
    /// </summary>
    /// <typeparam name="TWriterSetup">The type of the writer setup.</typeparam>
    /// <seealso cref="IGeneratorSetup" />
    public abstract class GeneratorSetup<TWriterSetup> : IGeneratorSetup
        where TWriterSetup : class, IWriterSetup
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GeneratorSetup{TWriterSetup}"/> class.
        /// </summary>
        protected GeneratorSetup()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GeneratorSetup{TWriterSetup}" /> class.
        /// </summary>
        /// <param name="generator">The generator.</param>
        /// <param name="writerSetups">The target setups.</param>
        /// <param name="skipGlobalWriterSetup">if set to <c>true</c> [skip global writer setup].</param>
        /// <param name="shareGlobalWriters">if set to <c>true</c> [share global writers].</param>
        protected GeneratorSetup(TypeOrObject<IGenerator> generator, IReadOnlyList<TWriterSetup>? writerSetups, bool skipGlobalWriterSetup, bool shareGlobalWriters)
        {
            this.Generator = generator;
            this.WriterSetups = writerSetups;
            this.SkipGlobalWriterSetups = skipGlobalWriterSetup;
            this.ShareGlobalWriters = shareGlobalWriters;
        }

        /// <summary>
        /// Gets the generator.
        /// </summary>
        /// <value>
        /// The generator.
        /// </value>
        public TypeOrObject<IGenerator>? Generator { get; init; }

        /// <summary>
        /// Gets the target setups.
        /// </summary>
        /// <value>
        /// The target setups.
        /// </value>
        public IReadOnlyList<TWriterSetup>? WriterSetups { get; init; }

        /// <summary>
        /// Gets a value indicating whether [skip global writer setup].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [skip global writer setup]; otherwise, <c>false</c>.
        /// </value>
        public bool SkipGlobalWriterSetups { get; init; }

        /// <summary>
        /// Gets a value indicating whether the global writer should shared between generators.
        /// </summary>
        /// <value>
        ///   <c>true</c> if global writers are shared otherwise, <c>false</c>.
        /// </value>
        public bool ShareGlobalWriters { get; init; }

        /// <summary>
        /// Gets the writer setups.
        /// </summary>
        /// <value>
        /// The writer setups.
        /// </value>
        IReadOnlyList<IWriterSetup>? IGeneratorSetup.WriterSetups => this.WriterSetups;
    }
}