// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GeneratorAdapter.cs" company="Sundews">
// Copyright (c) Sundews. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sundew.Generator.Engine.Internal;

using System;
using System.Collections.Generic;
using Sundew.Generator.Core;

/// <summary>
/// Adapts a generic generator to the type agnostic generator interface.
/// </summary>
/// <typeparam name="TSetup">The type of the setup.</typeparam>
/// <typeparam name="TGeneratorSetup">The type of the generator setup.</typeparam>
/// <typeparam name="TTarget">The type of the target.</typeparam>
/// <typeparam name="TModel">The type of the model.</typeparam>
/// <typeparam name="TRun">The type of the run information.</typeparam>
/// <typeparam name="TOutput">The type of the output.</typeparam>
/// <seealso cref="IGenerator" />
internal sealed class GeneratorAdapter<TSetup, TGeneratorSetup, TTarget, TModel, TRun, TOutput> : IGenerator<ISetup, IGeneratorSetup, ITarget, object, IRun, object>, IHaveGenerator
    where TTarget : ITarget
    where TRun : IRun
{
    private readonly IGenerator<TSetup, TGeneratorSetup, TTarget, TModel, TRun, TOutput> generator;

    /// <summary>
    /// Initializes a new instance of the <see cref="GeneratorAdapter{TSetup, TGeneratorSetup, TTarget, TModel, TRun, TOutput}" /> class.
    /// </summary>
    /// <param name="generator">The generator.</param>
    public GeneratorAdapter(IGenerator<TSetup, TGeneratorSetup, TTarget, TModel, TRun, TOutput> generator)
    {
        this.generator = generator;
    }

    /// <summary>
    /// Gets the generator.
    /// </summary>
    /// <value>
    /// The generator.
    /// </value>
    public IGenerator Generator => this.generator;

    /// <summary>
    /// Prepares the specified setup.
    /// </summary>
    /// <param name="setup">The setup.</param>
    /// <param name="generatorSetup">The generator setup.</param>
    /// <param name="target">The target.</param>
    /// <param name="model">The model.</param>
    /// <param name="modelOrigin">The model path.</param>
    /// <returns>
    /// A list of <see cref="IRun" />s.
    /// </returns>
    public IReadOnlyList<IRun> Prepare(ISetup setup, IGeneratorSetup generatorSetup, ITarget target, object model, string modelOrigin)
    {
        return (IReadOnlyList<IRun>)this.generator.Prepare((TSetup)setup, (TGeneratorSetup)generatorSetup, (TTarget)target, (TModel)model, modelOrigin);
    }

    /// <summary>
    /// Generates a string based on the specified parameters.
    /// </summary>
    /// <param name="setup">The setup.</param>
    /// <param name="generatorSetup">The generator setup.</param>
    /// <param name="target">The target information.</param>
    /// <param name="model">The model.</param>
    /// <param name="run">The run information.</param>
    /// <param name="index">The index.</param>
    /// <returns>
    /// The generated string.
    /// </returns>
    public object Generate(ISetup setup, IGeneratorSetup generatorSetup, ITarget target, object model, IRun run, long index)
    {
        return this.generator.Generate((TSetup)setup, (TGeneratorSetup)generatorSetup, (TTarget)target, (TModel)model, (TRun)run, index) ?? throw new InvalidOperationException($"Generator: {this.generator} returned null.");
    }
}