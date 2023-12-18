// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IMsBuildWriterSetup.cs" company="Sundews">
// Copyright (c) Sundews. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sundew.Generator.Tests;

using Sundew.Generator.Core;
using Sundew.Generator.Output;

[DefaultImplementation(typeof(MsBuildWriterSetup))]
public interface IMsBuildWriterSetup : IWriterSetup
{
    bool AddFilesToProject { get; }
}