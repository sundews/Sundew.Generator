// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SetupsFactoryTypeSetupsFactory.cs" company="Sundews">
// Copyright (c) Sundews. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sundew.Generator.Discovery;

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using Sundew.Generator.Core;
using Sundew.Generator.Reflection;

/// <summary>
/// Setups Factory for types of <see cref="ISetupsFactory"/>.
/// </summary>
/// <seealso cref="Sundew.Generator.Discovery.ISetupsFactory" />
public class SetupsFactoryTypeSetupsFactory : ISetupsFactory
{
    private readonly IEnumerable<string> typeNames;

    /// <summary>
    /// Initializes a new instance of the <see cref="SetupsFactoryTypeSetupsFactory"/> class.
    /// </summary>
    /// <param name="typeNames">The type names.</param>
    public SetupsFactoryTypeSetupsFactory(IEnumerable<string> typeNames)
    {
        this.typeNames = typeNames;
    }

    /// <summary>
    /// Gets the setups.
    /// </summary>
    /// <returns>
    /// The setups.
    /// </returns>
    public async Task<IEnumerable<ISetup>> GetSetupsAsync()
    {
        var setups = new ConcurrentBag<ISetup>();
        foreach (var typeName in this.typeNames)
        {
            foreach (var setup in await ((ISetupsFactory)Activator.CreateInstance(TypeAssemblyLoader.GetType(typeName))).GetSetupsAsync().ConfigureAwait(false))
            {
                setups.Add(setup);
            }
        }

        return setups;
    }
}