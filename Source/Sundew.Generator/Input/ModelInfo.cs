// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ModelInfo.cs" company="Hukano">
// Copyright (c) Hukano. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sundew.Generator.Input
{
    /// <summary>
    /// Contains a models and it's origin.
    /// </summary>
    /// <typeparam name="TModel">The type of the model.</typeparam>
    public class ModelInfo<TModel> : IModelInfo<TModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ModelInfo{TModel}"/> class.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="modelOrigin">The model origin.</param>
        public ModelInfo(TModel model, string modelOrigin)
        {
            this.Model = model;
            this.ModelOrigin = modelOrigin;
        }

        /// <summary>
        /// Gets the model.
        /// </summary>
        /// <value>
        /// The model.
        /// </value>
        public TModel Model { get; }

        /// <summary>
        /// Gets the model origin.
        /// </summary>
        /// <value>
        /// The model origin.
        /// </value>
        public string ModelOrigin { get; }
    }
}