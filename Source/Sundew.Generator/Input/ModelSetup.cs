// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ModelSetup.cs" company="Hukano">
// Copyright (c) Hukano. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sundew.Generator.Input;

using System;
using Sundew.Generator.Core;

/// <summary>
/// Default model setup.
/// </summary>
/// <seealso cref="Sundew.Generator.Input.IModelSetup" />
public class ModelSetup : IModelSetup
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ModelSetup"/> class.
    /// </summary>
    public ModelSetup()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ModelSetup" /> class.
    /// </summary>
    /// <param name="provider">The provider.</param>
    /// <param name="modelType">Type of the model.</param>
    public ModelSetup(TypeOrObject<IModelProvider> provider, Type modelType)
        : this()
    {
        this.Provider = provider;
        this.ModelType = modelType;
    }

    /// <summary>
    /// Gets the provider.
    /// </summary>
    /// <value>
    /// The provider.
    /// </value>
    public TypeOrObject<IModelProvider>? Provider { get; init; }

    /// <summary>
    /// Gets the type of the model.
    /// </summary>
    /// <value>
    /// The type of the model.
    /// </value>
    public Type? ModelType { get; init; }
}