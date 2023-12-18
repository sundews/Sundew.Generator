// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ISetupDiscoverer.cs" company="Sundews">
// Copyright (c) Sundews. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sundew.Generator.Discovery;

using System.Collections.Generic;
using System.Threading.Tasks;
using Sundew.Generator.Core;

/// <summary>
/// Interface for implementing a setup discoverer.
/// </summary>
public interface ISetupDiscoverer
{
    /// <summary>
    /// Discovers the specified directory.
    /// </summary>
    /// <param name="directory">The directory.</param>
    /// <param name="searchPattern">The search pattern.</param>
    /// <returns>An <see cref="IEnumerable{SetupInfo}"/>.</returns>
    Task<IEnumerable<SetupInfo>> DiscoverAsync(string directory, string searchPattern);
}