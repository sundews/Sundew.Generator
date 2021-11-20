// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ICompilationsSetup.cs" company="Hukano">
// Copyright (c) Hukano. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sundew.Generator.Code;

using System.Collections.Generic;
using Sundew.Generator.Core;

/// <summary>
/// Interface for implementing a setup that can provide paths for compilations.
/// </summary>
[DefaultImplementation(typeof(CompilationsSetup))]
public interface ICompilationsSetup : ISetup
{
    /// <summary>
    /// Gets the compilation paths.
    /// </summary>
    /// <value>
    /// The compilation paths.
    /// </value>
    IReadOnlyList<string>? CompilationPaths { get; }
}