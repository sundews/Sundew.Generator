// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IGeneratorSetup.cs" company="Hukano">
// Copyright (c) Hukano. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sundew.Generator.Core
{
    using System.Collections.Generic;
    using Sundew.Generator.Output;

    /// <summary>
    /// Interface for implementing settings for a generator.
    /// </summary>
    public interface IGeneratorSetup
    {
        /// <summary>
        /// Gets the generator.
        /// </summary>
        /// <value>
        /// The generator.
        /// </value>
        TypeOrObject<IGenerator> Generator { get; }

        /// <summary>
        /// Gets the writer setups.
        /// </summary>
        /// <value>
        /// The writer setups.
        /// </value>
        IReadOnlyList<IWriterSetup> WriterSetups { get; }

        /// <summary>
        /// Gets a value indicating whether [skip global writer setup].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [skip global writer setup]; otherwise, <c>false</c>.
        /// </value>
        bool SkipGlobalWriterSetups { get; }

        /// <summary>
        /// Gets a value indicating whether the global writers should shared between generators.
        /// </summary>
        /// <value>
        ///   <c>true</c> if global writers are shared otherwise, <c>false</c>.
        /// </value>
        bool ShareGlobalWriters { get; }
    }
}