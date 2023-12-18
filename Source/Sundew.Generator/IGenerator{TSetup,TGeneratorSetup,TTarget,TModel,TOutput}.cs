// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IGenerator{TSetup,TGeneratorSetup,TTarget,TModel,TOutput}.cs" company="Sundews">
// Copyright (c) Sundews. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sundew.Generator;

using Sundew.Generator.Core;

/// <summary>
/// Interface for implementing a Text generator.
/// </summary>
/// <typeparam name="TSetup">The type of the global settings.</typeparam>
/// <typeparam name="TGeneratorSetup">The type of the generator settings.</typeparam>
/// <typeparam name="TTarget">The type of the target data.</typeparam>
/// <typeparam name="TModel">The type of the model.</typeparam>
/// <typeparam name="TOutput">The type of the output.</typeparam>
/// <seealso cref="IGenerator{TSetup, TGeneratorSetup, TTarget, TModel, IRun, TOutput}" />
public interface IGenerator<in TSetup, in TGeneratorSetup, in TTarget, in TModel, out TOutput> : IGenerator<TSetup, TGeneratorSetup, TTarget, TModel, IRun, TOutput>
    where TTarget : ITarget
{
}