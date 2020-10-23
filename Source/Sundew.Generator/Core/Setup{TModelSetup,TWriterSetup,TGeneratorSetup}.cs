// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Setup{TModelSetup,TWriterSetup,TGeneratorSetup}.cs" company="Hukano">
// Copyright (c) Hukano. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sundew.Generator.Core
{
    using System.Collections.Generic;
    using Sundew.Generator.Input;
    using Sundew.Generator.Output;

    /// <summary>
    /// Minimal implementation of <see cref="ISetup" />.
    /// </summary>
    /// <typeparam name="TModelSetup">The type of the model setup.</typeparam>
    /// <typeparam name="TWriterSetup">The type of the writer setup.</typeparam>
    /// <typeparam name="TGeneratorSetup">The type of the generator setup.</typeparam>
    /// <seealso cref="ISetup" />
    public abstract class Setup<TModelSetup, TWriterSetup, TGeneratorSetup> : ISetup
        where TModelSetup : class, IModelSetup
        where TWriterSetup : class, IWriterSetup
        where TGeneratorSetup : class, IGeneratorSetup
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Setup{TModelSetup, TWriterSetup, TGeneratorSetup}"/> class.
        /// </summary>
        protected Setup()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Setup{TModelSetup, TWriterSetup, TGeneratorSetup}" /> class.
        /// </summary>
        /// <param name="modelSetup">The model setup.</param>
        /// <param name="writerSetups">The writer setups.</param>
        /// <param name="generatorSetups">The generator setups.</param>
        protected Setup(TModelSetup modelSetup, IReadOnlyList<TWriterSetup> writerSetups, IReadOnlyList<TGeneratorSetup> generatorSetups)
            : this()
        {
            this.ModelSetup = modelSetup;
            this.WriterSetups = writerSetups;
            this.GeneratorSetups = generatorSetups;
        }

        /// <summary>
        /// Gets the model setup.
        /// </summary>
        /// <value>
        /// The model setup.
        /// </value>
        public TModelSetup ModelSetup { get; init; }

        /// <summary>
        /// Gets the target setups.
        /// </summary>
        /// <value>
        /// The target setups.
        /// </value>
        public IReadOnlyList<TWriterSetup> WriterSetups { get; init; }

        /// <summary>
        /// Gets the generator setups.
        /// </summary>
        /// <value>
        /// The generator setups.
        /// </value>
        public IReadOnlyList<TGeneratorSetup> GeneratorSetups { get; init; }

        /// <summary>
        /// Gets the model setup.
        /// </summary>
        /// <value>
        /// The model setup.
        /// </value>
        IModelSetup ISetup.ModelSetup => this.ModelSetup;

        /// <summary>
        /// Gets the writer setups.
        /// </summary>
        /// <value>
        /// The output setups.
        /// </value>
        IReadOnlyList<IWriterSetup> ISetup.WriterSetups => this.WriterSetups;

        /// <summary>
        /// Gets the generator setups.
        /// </summary>
        /// <value>
        /// The generator setups.
        /// </value>
        IReadOnlyList<IGeneratorSetup> ISetup.GeneratorSetups => this.GeneratorSetups;
    }
}