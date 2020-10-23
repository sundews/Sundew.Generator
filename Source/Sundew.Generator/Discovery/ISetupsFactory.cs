// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ISetupsFactory.cs" company="Hukano">
// Copyright (c) Hukano. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sundew.Generator.Discovery
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Sundew.Generator.Core;

    /// <summary>
    /// Interface for implementing a setup factory.
    /// </summary>
    public interface ISetupsFactory
    {
        /// <summary>
        /// Gets the setups.
        /// </summary>
        /// <returns>The setups.</returns>
        Task<IEnumerable<ISetup>> GetSetupsAsync();
    }
}