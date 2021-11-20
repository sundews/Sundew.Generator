// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IGenerator{TSetup,TGeneratorSetup,TModel,TOutput}.cs" company="Hukano">
// Copyright (c) Hukano. All rights reserved.
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
/// <typeparam name="TModel">The type of the model.</typeparam>
/// <typeparam name="TOutput">The type of the output.</typeparam>
/// <seealso cref="IGenerator{TSetup, TGeneratorSetup, ITarget, TModel, IRun}" />
public interface IGenerator<in TSetup, in TGeneratorSetup, in TModel, out TOutput> : IGenerator<TSetup, TGeneratorSetup, ITarget, TModel, IRun, TOutput>
{
}