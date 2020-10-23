// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WriterAdapter.cs" company="Hukano">
// Copyright (c) Hukano. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sundew.Generator.Engine.Internal.Output
{
    using System.Threading.Tasks;
    using Sundew.Generator.Core;
    using Sundew.Generator.Output;
    using Sundew.Generator.Reporting;

    internal class WriterAdapter<TWriterSetup, TTarget, TRun, TOutput> : IWriter<IWriterSetup, ITarget, IRun, object>, IHaveWriter
        where TWriterSetup : IWriterSetup
        where TTarget : ITarget
        where TRun : IRun
    {
        private readonly IWriter<TWriterSetup, TTarget, TRun, TOutput> writer;

        public WriterAdapter(IWriter<TWriterSetup, TTarget, TRun, TOutput> writer)
        {
            this.writer = writer;
        }

        public IWriter Writer => this.writer;

        public async Task<ITarget> GetTargetAsync(IWriterSetup writerSetup)
        {
            return await this.writer.GetTargetAsync((TWriterSetup)writerSetup).ConfigureAwait(false);
        }

        public Task PrepareTargetAsync(ITarget target, IWriterSetup writerSetup)
        {
            return this.writer.PrepareTargetAsync((TTarget)target, (TWriterSetup)writerSetup);
        }

        public Task<string> ApplyContentToTargetAsync(ITarget target, IRun run, IWriterSetup writerSetup, object output)
        {
            return this.writer.ApplyContentToTargetAsync((TTarget)target, (TRun)run, (TWriterSetup)writerSetup, (TOutput)output);
        }

        public Task CompleteTargetAsync(ITargetCompletionTracker targetCompletionTracker)
        {
            return this.writer.CompleteTargetAsync(targetCompletionTracker);
        }
    }
}