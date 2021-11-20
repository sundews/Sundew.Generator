// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TextWriter.cs" company="Hukano">
// Copyright (c) Hukano. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sundew.Generator.Tests;

using System.Threading.Tasks;
using Sundew.Generator.Core;
using Sundew.Generator.Output;
using Sundew.Generator.Reporting;

public class TextWriter : IWriter<IMsBuildWriterSetup, ITarget, IRun, string>
{
    public Task<ITarget> GetTargetAsync(IMsBuildWriterSetup writerSetup)
    {
        throw new System.NotImplementedException();
    }

    public Task PrepareTargetAsync(ITarget target, IMsBuildWriterSetup writerSetup)
    {
        throw new System.NotImplementedException();
    }

    Task<string> IWriter<IMsBuildWriterSetup, ITarget, IRun, string>.ApplyContentToTargetAsync(ITarget target, IRun run, IMsBuildWriterSetup writerSetup, string output)
    {
        throw new System.NotImplementedException();
    }

    public Task CompleteTargetAsync(ITargetCompletionTracker targetCompletionTracker)
    {
        throw new System.NotImplementedException();
    }
}