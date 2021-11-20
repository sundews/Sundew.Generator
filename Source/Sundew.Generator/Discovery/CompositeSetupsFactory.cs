// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CompositeSetupsFactory.cs" company="Hukano">
// Copyright (c) Hukano. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sundew.Generator.Discovery;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sundew.Generator.Core;

/// <summary>
/// Combine the output of two <see cref="ISetupsFactory"/>.
/// </summary>
/// <seealso cref="Sundew.Generator.Discovery.ISetupsFactory" />
public class CompositeSetupsFactory : ISetupsFactory
{
    private readonly ISetupsFactory setupsFactory;
    private readonly ISetupsFactory additionalSetupsFactory;

    /// <summary>
    /// Initializes a new instance of the <see cref="CompositeSetupsFactory"/> class.
    /// </summary>
    /// <param name="setupsFactory">The setups factory.</param>
    /// <param name="additionalSetupsFactory">The additional setups factory.</param>
    public CompositeSetupsFactory(ISetupsFactory setupsFactory, ISetupsFactory additionalSetupsFactory)
    {
        this.setupsFactory = setupsFactory;
        this.additionalSetupsFactory = additionalSetupsFactory;
    }

    /// <summary>
    /// Gets the setups.
    /// </summary>
    /// <returns>
    /// The setups.
    /// </returns>
    public async Task<IEnumerable<ISetup>> GetSetupsAsync()
    {
        return (await this.setupsFactory.GetSetupsAsync()).Concat(await this.additionalSetupsFactory.GetSetupsAsync());
    }
}