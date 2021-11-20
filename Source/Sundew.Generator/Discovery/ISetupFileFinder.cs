// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ISetupFileFinder.cs" company="Hukano">
// Copyright (c) Hukano. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sundew.Generator.Discovery;

using System.Collections.Generic;
using System.Threading.Tasks;

/// <summary>
/// Interface for implementing a finder for generator settings.
/// </summary>
public interface ISetupFileFinder
{
    /// <summary>
    /// Searches the specified directory.
    /// </summary>
    /// <param name="directory">The directory.</param>
    /// <param name="searchPattern">The search pattern.</param>
    /// <returns>The found files.</returns>
    Task<IEnumerable<string>> SearchAsync(string directory, string searchPattern);
}