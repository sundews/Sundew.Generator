// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IModelInfo.cs" company="Hukano">
// Copyright (c) Hukano. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sundew.Generator.Input;

/// <summary>
/// Model info interface.
/// </summary>
/// <typeparam name="TModel">The type of the model.</typeparam>
public interface IModelInfo<out TModel>
{
    /// <summary>
    /// Gets the model.
    /// </summary>
    /// <value>
    /// The model.
    /// </value>
    TModel Model { get; }

    /// <summary>
    /// Gets the model origin.
    /// </summary>
    /// <value>
    /// The model origin.
    /// </value>
    string ModelOrigin { get; }
}