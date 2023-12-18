// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ISetup.cs" company="Sundews">
// Copyright (c) Sundews. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sundew.Generator;

using System.Collections.Generic;
using Sundew.Generator.Core;
using Sundew.Generator.Input;
using Sundew.Generator.Output;

/// <summary>
/// Interface for the generator setup.
/// </summary>
public interface ISetup
{
    /// <summary>
    /// Gets the model setup.
    /// </summary>
    /// <value>
    /// The model setup.
    /// </value>
    IModelSetup? ModelSetup { get; }

    /// <summary>
    /// Gets the writer setups.
    /// </summary>
    /// <value>
    /// The output setups.
    /// </value>
    IReadOnlyList<IWriterSetup>? WriterSetups { get; }

    /// <summary>
    /// Gets the generator setups.
    /// </summary>
    /// <value>
    /// The generator setups.
    /// </value>
    IReadOnlyList<IGeneratorSetup>? GeneratorSetups { get; }
}