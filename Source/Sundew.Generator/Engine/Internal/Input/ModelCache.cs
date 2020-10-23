// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ModelCache.cs" company="Hukano">
// Copyright (c) Hukano. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sundew.Generator.Engine.Internal.Input
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Sundew.Base.Threading;
    using Sundew.Generator.Core;
    using Sundew.Generator.Input;

    internal class ModelCache
    {
        private readonly IModelProvider<ISetup, IModelSetup, object> modelProvider;
        private readonly ISetup setup;
        private readonly AsyncLazy<IReadOnlyList<IModelInfo<object>>> models;

        public ModelCache(IModelProvider<ISetup, IModelSetup, object> modelProvider, ISetup setup)
        {
            this.modelProvider = modelProvider;
            this.setup = setup;
            this.models = new AsyncLazy<IReadOnlyList<IModelInfo<object>>>(() => this.modelProvider.GetModelsAsync(this.setup, this.setup.ModelSetup));
        }

        public async Task<IReadOnlyList<IModelInfo<object>>> GetModelInfosAsync()
        {
            return await this.models;
        }
    }
}