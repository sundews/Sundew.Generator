// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IHaveGenerator.cs" company="Sundews">
// Copyright (c) Sundews. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sundew.Generator.Engine.Internal;

using Sundew.Generator.Core;

/// <summary>
/// Interface that provides access to the real generator in a GeneratorAdapter.
/// </summary>
internal interface IHaveGenerator
{
    /// <summary>
    /// Gets the generator.
    /// </summary>
    /// <value>
    /// The generator.
    /// </value>
    IGenerator Generator { get; }
}