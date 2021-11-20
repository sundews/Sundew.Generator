// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ISetupProvider.cs" company="Hukano">
// Copyright (c) Hukano. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sundew.Generator.Discovery;

using System.Threading.Tasks;

/// <summary>
/// Provider for getting an <see cref="ISetup"/> based on a file path.
/// </summary>
public interface ISetupProvider
{
    /// <summary>
    /// Gets the setup.
    /// </summary>
    /// <param name="path">The path.</param>
    /// <returns>An <see cref="ISetup"/>.</returns>
    Task<ISetup> GetSetupAsync(string path);
}