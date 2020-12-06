// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ModelProviderAdapter.cs" company="Hukano">
// Copyright (c) Hukano. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sundew.Generator.Engine.Internal.Input
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Sundew.Generator.Input;

    internal class ModelProviderAdapter<TSetup, TModelSetup, TModel> : IModelProvider<ISetup, IModelSetup, object>
        where TSetup : ISetup
        where TModelSetup : class
        where TModel : class
    {
        private readonly IModelProvider<TSetup, TModelSetup, TModel> modelProvider;

        public ModelProviderAdapter(IModelProvider<TSetup, TModelSetup, TModel> modelProvider)
        {
            this.modelProvider = modelProvider;
        }

        public async Task<IReadOnlyList<IModelInfo<object>>> GetModelsAsync(ISetup setup, IModelSetup? modelSetup)
        {
            return await this.modelProvider.GetModelsAsync((TSetup)setup, (TModelSetup?)modelSetup).ConfigureAwait(false);
        }
    }
}