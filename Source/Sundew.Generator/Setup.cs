// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Setup.cs" company="Hukano">
// Copyright (c) Hukano. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sundew.Generator;

using System.Collections.Generic;
using Sundew.Generator.Core;
using Sundew.Generator.Input;
using Sundew.Generator.Output;

/// <summary>
/// Minimal implementation of <see cref="ISetup"/>.
/// </summary>
/// <seealso cref="ISetup" />
public class Setup : Setup<IModelSetup, IWriterSetup, IGeneratorSetup>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Setup" /> class.
    /// </summary>
    public Setup()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Setup" /> class.
    /// </summary>
    /// <param name="modelSetup">The model setup.</param>
    /// <param name="writerSetups">The writer setups.</param>
    /// <param name="generatorSetups">The generator setups.</param>
    public Setup(IModelSetup modelSetup, IReadOnlyList<IWriterSetup> writerSetups, IReadOnlyList<IGeneratorSetup> generatorSetups)
        : base(modelSetup, writerSetups, generatorSetups)
    {
    }
}