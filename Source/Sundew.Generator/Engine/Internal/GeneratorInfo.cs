// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GeneratorInfo.cs" company="Hukano">
// Copyright (c) Hukano. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sundew.Generator.Engine.Internal
{
    using Sundew.Generator.Core;
    using Sundew.Generator.Engine.Internal.Input;

    internal class GeneratorInfo
    {
        public GeneratorInfo(ISetup setup, ModelCache modelCache, IGeneratorSetup generatorSetup, IGenerator<ISetup, IGeneratorSetup, ITarget, object, IRun, object> generator)
        {
            this.Setup = setup;
            this.ModelCache = modelCache;
            this.GeneratorSetup = generatorSetup;
            this.Generator = generator;
        }

        public ISetup Setup { get; }

        public ModelCache ModelCache { get; }

        public IGeneratorSetup GeneratorSetup { get; }

        public IGenerator<ISetup, IGeneratorSetup, ITarget, object, IRun, object> Generator { get; }
    }
}