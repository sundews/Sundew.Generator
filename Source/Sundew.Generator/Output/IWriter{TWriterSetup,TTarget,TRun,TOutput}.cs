// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IWriter{TWriterSetup,TTarget,TRun,TOutput}.cs" company="Sundews">
// Copyright (c) Sundews. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sundew.Generator.Output;

using System.Threading.Tasks;
using Sundew.Generator.Core;
using Sundew.Generator.Reporting;

/// <summary>
/// Interface for implementing a target handler.
/// A target handler writes the output of generator to files and handles modification of project files.
/// </summary>
/// <typeparam name="TWriterSetup">The type of the target setup.</typeparam>
/// <typeparam name="TTarget">The type of the target data.</typeparam>
/// <typeparam name="TRun">The type of the run.</typeparam>
/// <typeparam name="TOutput">The type of the output.</typeparam>
/// <seealso cref="Sundew.Generator.Output.IWriter" />
public interface IWriter<in TWriterSetup, TTarget, in TRun, in TOutput> : IWriter
    where TWriterSetup : IWriterSetup
    where TTarget : ITarget
    where TRun : IRun
{
    /// <summary>
    /// Gets the target.
    /// </summary>
    /// <param name="writerSetup">The target setup.</param>
    /// <returns>
    /// The <see cref="ITarget" />.
    /// </returns>
    Task<TTarget> GetTargetAsync(TWriterSetup writerSetup);

    /// <summary>
    /// Prepares the target.
    /// </summary>
    /// <param name="target">The target.</param>
    /// <param name="writerSetup">The target setup.</param>
    /// <returns>
    /// The <see cref="ITarget" />.
    /// </returns>
    Task PrepareTargetAsync(TTarget target, TWriterSetup writerSetup);

    /// <summary>
    /// Applies the content to target.
    /// </summary>
    /// <param name="target">The target information.</param>
    /// <param name="run">The target output information.</param>
    /// <param name="writerSetup">The writer setup.</param>
    /// <param name="output">The output.</param>
    /// <returns>
    /// A <see cref="Task{String}" />.
    /// </returns>
    Task<string> ApplyContentToTargetAsync(TTarget target, TRun run, TWriterSetup writerSetup, TOutput output);

    /// <summary>
    /// Completes the target.
    /// </summary>
    /// <param name="targetCompletionTracker">The target completion tracker.</param>
    /// <returns>A <see cref="Task"/>.</returns>
    Task CompleteTargetAsync(ITargetCompletionTracker targetCompletionTracker);
}