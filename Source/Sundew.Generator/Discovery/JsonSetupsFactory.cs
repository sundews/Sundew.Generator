// --------------------------------------------------------------------------------------------------------------------
// <copyright file="JsonSetupsFactory.cs" company="Hukano">
// Copyright (c) Hukano. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sundew.Generator.Discovery;

using System.Collections.Generic;
using System.Threading.Tasks;
using Sundew.Base.Collections;
using Sundew.Generator.Core;

/// <summary>
/// Factory for json setups.
/// </summary>
/// <seealso cref="ISetupsFactory" />
public class JsonSetupsFactory : ISetupsFactory
{
    private readonly IEnumerable<string> setupPaths;
    private readonly JsonSetupProvider jsonSetupProvider;

    /// <summary>
    /// Initializes a new instance of the <see cref="JsonSetupsFactory"/> class.
    /// </summary>
    /// <param name="setupPaths">The generator setup paths.</param>
    public JsonSetupsFactory(IEnumerable<string> setupPaths)
    {
        this.setupPaths = setupPaths;
        this.jsonSetupProvider = new JsonSetupProvider();
    }

    /// <summary>
    /// Gets the setups.
    /// </summary>
    /// <returns>
    /// The setups.
    /// </returns>
    public async Task<IEnumerable<ISetup>> GetSetupsAsync()
    {
        return await this.setupPaths.SelectAsync(async path => await this.jsonSetupProvider.GetSetupAsync(path).ConfigureAwait(false)).ConfigureAwait(false);
    }
}