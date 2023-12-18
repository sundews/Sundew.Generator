// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EmptyModelProvider.cs" company="Sundews">
// Copyright (c) Sundews. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sundew.Generator.Input;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Sundew.Generator.Core;

/// <summary>
/// A model provider that return a single empty model.
/// </summary>
/// <typeparam name="TModel">The type of the model.</typeparam>
public class EmptyModelProvider<TModel> : IModelProvider<ISetup, IModelSetup, TModel>
    where TModel : class
{
    /// <summary>
    /// Gets the models.
    /// </summary>
    /// <param name="setup">The setup.</param>
    /// <param name="modelSetup">The model setup.</param>
    /// <returns>
    /// The models.
    /// </returns>
    public Task<IReadOnlyList<IModelInfo<TModel>>> GetModelsAsync(ISetup setup, IModelSetup? modelSetup)
    {
        return Task.FromResult((IReadOnlyList<IModelInfo<TModel>>)new IModelInfo<TModel>[] { new ModelInfo<TModel>(Activator.CreateInstance<TModel>(), this.GetType().ToString()) });
    }
}