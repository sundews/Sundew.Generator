// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WriterSetup.cs" company="Sundews">
// Copyright (c) Sundews. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sundew.Generator.Development.Tests;

using Newtonsoft.Json;
using Sundew.Generator.Core;
using Sundew.Generator.Output;

public class WriterSetup : IMsBuildWriterSetup
{
    public WriterSetup(string target)
    {
        this.Target = target;
    }

    [JsonConstructor]
    public WriterSetup(string target, string path, TypeOrObject<IWriter> writer, bool addFilesToProject)
    {
        this.Target = target;
        this.Path = path;
        this.Writer = writer;
        this.AddFilesToProject = addFilesToProject;
    }

    public string Target { get; init; }

    public string? Path { get; init; }

    public TypeOrObject<IWriter>? Writer { get; init; }

    public bool AddFilesToProject { get; init; }
}