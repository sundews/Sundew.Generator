// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EmptyGenerator.cs" company="Hukano">
// Copyright (c) Hukano. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sundew.Generator.Tests
{
    using System.Collections.Generic;
    using Sundew.Generator.Core;

    public class EmptyGenerator : IGenerator<Setup, GeneratorSetup, int, string>
    {
        public IReadOnlyList<IRun> Prepare(Setup setup, GeneratorSetup generatorSetup, ITarget target, int model, string modelOrigin)
        {
            throw new System.NotSupportedException();
        }

        public string Generate(Setup setup, GeneratorSetup generatorSetup, ITarget target, int model, IRun run, long index)
        {
            throw new System.NotSupportedException();
        }
    }
}