// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IModelProvider{TSetup,TModelSetup,TModel}.cs" company="Hukano">
// Copyright (c) Hukano. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sundew.Generator.Input
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// Interface for implementing a model provider.
    /// </summary>
    /// <typeparam name="TSetup">The type of the setup.</typeparam>
    /// <typeparam name="TModelSetup">The type of the model setup.</typeparam>
    /// <typeparam name="TModel">The type of the model.</typeparam>
    public interface IModelProvider<in TSetup, in TModelSetup, TModel> : IModelProvider
        where TModelSetup : class
        where TModel : class?
    {
        /// <summary>
        /// Gets the models.
        /// </summary>
        /// <param name="setup">The setup.</param>
        /// <param name="modelSetup">The model setup.</param>
        /// <returns>
        /// The models.
        /// </returns>
        Task<IReadOnlyList<IModelInfo<TModel>>> GetModelsAsync(TSetup setup, TModelSetup? modelSetup);
    }
}