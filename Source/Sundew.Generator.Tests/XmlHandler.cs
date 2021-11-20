// --------------------------------------------------------------------------------------------------------------------
// <copyright file="XmlHandler.cs" company="Hukano">
// Copyright (c) Hukano. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sundew.Generator.Tests;

using System.Threading.Tasks;
using Sundew.Generator.Core;
using Sundew.Generator.Output;
using Sundew.Generator.Reporting;

public class XmlHandler : IWriter<IWriterSetup, ITarget, IRun, string>
{
    public Task<ITarget> GetTargetAsync(IWriterSetup writerSetup)
    {
        throw new System.NotImplementedException();
    }

    public Task PrepareTargetAsync(ITarget target, IWriterSetup writerSetup)
    {
        throw new System.NotImplementedException();
    }

    public Task<string> ApplyContentToTargetAsync(ITarget target, IRun run, IWriterSetup writerSetup, string content)
    {
        throw new System.NotImplementedException();
    }

    public Task CompleteTargetAsync(ITargetCompletionTracker targetCompletionTracker)
    {
        throw new System.NotImplementedException();
    }
}