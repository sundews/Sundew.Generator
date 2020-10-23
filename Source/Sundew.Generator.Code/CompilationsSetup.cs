// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CompilationsSetup.cs" company="Hukano">
// Copyright (c) Hukano. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sundew.Generator.Code
{
    using System.Collections.Generic;
    using Sundew.Generator.Core;
    using Sundew.Generator.Input;
    using Sundew.Generator.Output;

    /// <summary>
    /// Default implementation of <see cref="ICompilationsSetup"/>.
    /// </summary>
    /// <seealso cref="Setup" />
    /// <seealso cref="ICompilationsSetup" />
    public class CompilationsSetup : Setup, ICompilationsSetup
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CompilationsSetup"/> class.
        /// </summary>
        public CompilationsSetup()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CompilationsSetup"/> class.
        /// </summary>
        /// <param name="modelSetup">The model setup.</param>
        /// <param name="writerSetups">The target setups.</param>
        /// <param name="generatorSetups">The generator setups.</param>
        /// <param name="compilationPaths">The compilation paths.</param>
        public CompilationsSetup(IModelSetup modelSetup, IReadOnlyList<IWriterSetup> writerSetups, IReadOnlyList<IGeneratorSetup> generatorSetups, IReadOnlyList<string> compilationPaths)
            : base(modelSetup, writerSetups, generatorSetups)
        {
            this.CompilationPaths = compilationPaths;
        }

        /// <summary>
        /// Gets the compilation paths.
        /// </summary>
        /// <value>
        /// The compilation paths.
        /// </value>
        public IReadOnlyList<string> CompilationPaths { get; }
    }
}