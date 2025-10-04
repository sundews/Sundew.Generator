// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Setup.cs" company="Sundews">
// Copyright (c) Sundews. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sundew.Generator.Development.Tests;

using global::System;
using global::System.Collections.Generic;
using Sundew.Generator.Core;
using Sundew.Generator.Input;
using Sundew.Generator.Output;

public class Setup : ISetup
{
    public Setup()
    {
        this.Type = this.GetType();
    }

    public Type Type { get; set; }

    public IModelSetup? ModelSetup { get; set; }

    public IReadOnlyList<IWriterSetup>? WriterSetups { get; set; }

    public IReadOnlyList<IGeneratorSetup>? GeneratorSetups { get; set; }
}