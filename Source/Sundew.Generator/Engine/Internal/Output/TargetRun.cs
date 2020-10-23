// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TargetRun.cs" company="Hukano">
// Copyright (c) Hukano. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sundew.Generator.Engine.Internal.Output
{
    using System.Collections.Generic;
    using Sundew.Generator.Core;
    using Sundew.Generator.Output;

    internal class TargetRun
    {
        public TargetRun(
            IWriterSetup writerSetup,
            IWriter<IWriterSetup, ITarget, IRun, object> writer,
            ITarget target,
            List<GeneratorRun> targetGeneratorRuns)
        {
            this.WriterSetup = writerSetup;
            this.Writer = writer;
            this.Target = target;
            this.GeneratorRuns = targetGeneratorRuns;
        }

        /// <summary>
        /// Gets the target setup.
        /// </summary>
        /// <value>
        /// The target setup.
        /// </value>
        public IWriterSetup WriterSetup { get; }

        public IWriter<IWriterSetup, ITarget, IRun, object> Writer { get; }

        public ITarget Target { get; }

        /// <summary>
        /// Gets the generator runs.
        /// </summary>
        /// <value>
        /// The generator runs.
        /// </value>
        public IReadOnlyList<GeneratorRun> GeneratorRuns { get; }
    }
}