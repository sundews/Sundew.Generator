// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GeneratorOptions.cs" company="Sundews">
// Copyright (c) Sundews. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sundew.Generator;

using System.IO;
using System.Threading;
using Sundew.Generator.Engine;
using Sundew.Generator.Reporting;

/// <summary>
/// Options for the generator.
/// </summary>
public class GeneratorOptions : IGeneratorOptions
{
    /// <summary>
    /// Gets the default options.
    /// </summary>
    public static GeneratorOptions Default { get; } = new();

    /// <summary>
    /// Gets the cancellation token.
    /// </summary>
    /// <value>
    /// The cancellation token.
    /// </value>
    public CancellationToken CancellationToken { get; init; } = CancellationToken.None;

    /// <summary>
    /// Gets the progress reporter.
    /// </summary>
    /// <value>
    /// The progress reporter.
    /// </value>
    public IProgressReporter? ProgressReporter { get; init; }

    /// <summary>
    /// Gets the progress text writer.
    /// </summary>
    /// <value>
    /// The progress text writer.
    /// </value>
    public TextWriter? ProgressTextWriter { get; init; }
}