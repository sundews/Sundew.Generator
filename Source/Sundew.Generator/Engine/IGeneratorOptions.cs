// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IGeneratorOptions.cs" company="Hukano">
// Copyright (c) Hukano. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ------------

namespace Sundew.Generator.Engine;

using System.Threading;

/// <summary>
/// Interface for specifying generator options.
/// </summary>
public interface IGeneratorOptions
{
    /// <summary>
    /// Gets the cancellation token.
    /// </summary>
    /// <value>
    /// The cancellation token.
    /// </value>
    CancellationToken CancellationToken { get; }
}