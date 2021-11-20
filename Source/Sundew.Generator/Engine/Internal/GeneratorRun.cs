// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GeneratorRun.cs" company="Hukano">
// Copyright (c) Hukano. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sundew.Generator.Engine.Internal;

using System.Collections.Generic;
using Sundew.Generator.Core;
using Sundew.Generator.Input;

internal class GeneratorRun
{
    public GeneratorRun(ISetup setup, IGeneratorSetup generatorSetup, IGenerator<ISetup, IGeneratorSetup, ITarget, object, IRun, object> generator, ITarget target, IModelInfo<object> modelInfo, IReadOnlyList<IRun> runs)
    {
        this.Setup = setup;
        this.GeneratorSetup = generatorSetup;
        this.Generator = generator;
        this.Target = target;
        this.ModelInfo = modelInfo;
        this.Runs = runs;
        this.Outputs = new object[this.Runs.Count];
    }

    public ISetup Setup { get; }

    public IGeneratorSetup GeneratorSetup { get; }

    public IGenerator<ISetup, IGeneratorSetup, ITarget, object, IRun, object> Generator { get; }

    public ITarget Target { get; }

    public IModelInfo<object> ModelInfo { get; }

    public IReadOnlyList<IRun> Runs { get; }

    public object[] Outputs { get; }
}