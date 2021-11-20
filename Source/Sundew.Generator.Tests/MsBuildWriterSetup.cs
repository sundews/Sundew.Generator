// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MsBuildWriterSetup.cs" company="Hukano">
// Copyright (c) Hukano. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sundew.Generator.Tests;

using Newtonsoft.Json;
using Sundew.Generator.Core;
using Sundew.Generator.Output;

public class MsBuildWriterSetup : IMsBuildWriterSetup
{
    public MsBuildWriterSetup(string target)
    {
        this.Target = target;
    }

    [JsonConstructor]
    public MsBuildWriterSetup(string target, string path, TypeOrObject<IWriter> writer, bool addFilesToProject)
    {
        this.Target = target;
        this.Path = path;
        this.Writer = writer;
        this.AddFilesToProject = addFilesToProject;
    }

    public string Target { get; init; }

    public string? Path { get; }

    public TypeOrObject<IWriter>? Writer { get; init; }

    public bool AddFilesToProject { get; init; }
}