// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GeneratorSetup.cs" company="Hukano">
// Copyright (c) Hukano. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sundew.Generator;

using System.Collections.Generic;
using Sundew.Generator.Core;
using Sundew.Generator.Output;

/// <summary>
/// Minimal implementation of <see cref="IGeneratorSetup"/>.
/// </summary>
/// <seealso cref="IGeneratorSetup" />
public class GeneratorSetup : GeneratorSetup<IWriterSetup>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GeneratorSetup"/> class.
    /// </summary>
    public GeneratorSetup()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="GeneratorSetup" /> class.
    /// </summary>
    /// <param name="generator">The generator.</param>
    /// <param name="writerSetups">The writer setups.</param>
    /// <param name="skipGlobalWriterSetup">if set to <c>true</c> [skip global writer setup].</param>
    /// <param name="shareGlobalWriters">if set to <c>true</c> [share global writers].</param>
    public GeneratorSetup(TypeOrObject<IGenerator> generator, IReadOnlyList<IWriterSetup>? writerSetups, bool skipGlobalWriterSetup, bool shareGlobalWriters)
        : base(generator, writerSetups, skipGlobalWriterSetup, shareGlobalWriters)
    {
    }
}