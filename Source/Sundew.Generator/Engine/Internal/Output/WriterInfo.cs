// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WriterInfo.cs" company="Hukano">
// Copyright (c) Hukano. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sundew.Generator.Engine.Internal.Output
{
    using System.Collections.Generic;
    using Sundew.Generator.Core;
    using Sundew.Generator.Output;

    internal class WriterInfo
    {
        public WriterInfo(IWriterSetup writerSetup, IWriter<IWriterSetup, ITarget, IRun, object> writer, IEnumerable<GeneratorInfo> generatorInfos)
        {
            this.WriterSetup = writerSetup;
            this.Writer = writer;
            this.GeneratorInfos = generatorInfos;
        }

        public IWriterSetup WriterSetup { get; }

        public IWriter<IWriterSetup, ITarget, IRun, object> Writer { get; }

        public IEnumerable<GeneratorInfo> GeneratorInfos { get; }
    }
}