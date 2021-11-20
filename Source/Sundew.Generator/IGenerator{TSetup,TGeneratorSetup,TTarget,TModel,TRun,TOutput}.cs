// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IGenerator{TSetup,TGeneratorSetup,TTarget,TModel,TRun,TOutput}.cs" company="Hukano">
// Copyright (c) Hukano. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sundew.Generator;

using System.Collections.Generic;
using Sundew.Generator.Core;

/// <summary>
/// Interface for implementing a Text generator.
/// </summary>
/// <typeparam name="TSetup">The type of the global settings.</typeparam>
/// <typeparam name="TGeneratorSetup">The type of the generator settings.</typeparam>
/// <typeparam name="TTarget">The type of the target data.</typeparam>
/// <typeparam name="TModel">The type of the model.</typeparam>
/// <typeparam name="TRun">The type of the model child.</typeparam>
/// <typeparam name="TOutput">The type of the output.</typeparam>
public interface IGenerator<in TSetup, in TGeneratorSetup, in TTarget, in TModel, TRun, out TOutput> : IGenerator
    where TTarget : ITarget
    where TRun : IRun
{
    /// <summary>
    /// Prepares for the generator to run.
    /// </summary>
    /// <param name="setup">The setup.</param>
    /// <param name="generatorSetup">The generator setup.</param>
    /// <param name="target">The target information.</param>
    /// <param name="model">The model.</param>
    /// <param name="modelOrigin">The model origin.</param>
    /// <returns>
    /// A list of <see cref="IRun" />s.
    /// </returns>
    IReadOnlyList<TRun> Prepare(TSetup setup, TGeneratorSetup generatorSetup, TTarget target, TModel model, string modelOrigin);

    /// <summary>
    /// Generates a string based of the specified parameters.
    /// </summary>
    /// <param name="setup">The setup.</param>
    /// <param name="generatorSetup">The generator setup.</param>
    /// <param name="target">The target information.</param>
    /// <param name="model">The model.</param>
    /// <param name="run">The child model.</param>
    /// <param name="index">The index.</param>
    /// <returns>
    /// The generated content.
    /// </returns>
    TOutput Generate(TSetup setup, TGeneratorSetup generatorSetup, TTarget target, TModel model, TRun run, long index);
}