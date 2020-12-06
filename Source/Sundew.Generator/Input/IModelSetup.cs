// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IModelSetup.cs" company="Hukano">
// Copyright (c) Hukano. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sundew.Generator.Input
{
    using System;
    using Sundew.Generator.Core;

    /// <summary>
    /// Default model setup.
    /// </summary>
    public interface IModelSetup
    {
        /// <summary>
        /// Gets the provider.
        /// </summary>
        /// <value>
        /// The provider.
        /// </value>
        TypeOrObject<IModelProvider>? Provider { get; }

        /// <summary>
        /// Gets the type of the model.
        /// </summary>
        /// <value>
        /// The type of the model.
        /// </value>
        Type? ModelType { get; }
    }
}