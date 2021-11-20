// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TextFileWriter.cs" company="Hukano">
// Copyright (c) Hukano. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sundew.Generator.Output;

using System.IO;
using System.Threading.Tasks;
using Sundew.Generator.Core;
using Sundew.Generator.Reporting;

/// <summary>
/// Target handler for outputting to a directory.
/// </summary>
/// <seealso cref="IWriter{TTargetSetup,TTarget,IRun,TOutput}" />
public class TextFileWriter : IWriter<IFileWriterSetup, IFolderTarget, IRun, ITextOutput>
{
    /// <summary>
    /// Prepares the target.
    /// </summary>
    /// <param name="writerSetup">The writer setup.</param>
    /// <returns>
    /// The <see cref="T:Sundew.Generator.Common.Target" />.
    /// </returns>
    public Task<IFolderTarget> GetTargetAsync(IFileWriterSetup writerSetup)
    {
        var fullPath = Path.GetFullPath(writerSetup.Target);
        var directoryPath = Path.GetDirectoryName(fullPath);
        return Task.FromResult((IFolderTarget)new FolderTarget(directoryPath, fullPath, directoryPath));
    }

    /// <summary>
    /// Prepares the target.
    /// </summary>
    /// <param name="target">The target.</param>
    /// <param name="writerSetup">The target setup.</param>
    /// <returns>
    /// The <see cref="T:Sundew.Generator.Common.Target" />.
    /// </returns>
    public Task PrepareTargetAsync(IFolderTarget target, IFileWriterSetup writerSetup)
    {
        Directory.CreateDirectory(target.FolderPath);
        return Task.CompletedTask;
    }

    /// <summary>
    /// Applies the content to target.
    /// </summary>
    /// <param name="target">The target information.</param>
    /// <param name="run">The target output information.</param>
    /// <param name="writerSetup">The writer setup.</param>
    /// <param name="output">The output.</param>
    /// <returns>
    /// A <see cref="T:System.Threading.Tasks.Task`1" />.
    /// </returns>
    public async Task<string> ApplyContentToTargetAsync(IFolderTarget target, IRun run, IFileWriterSetup writerSetup, ITextOutput output)
    {
        var filePath = Path.Combine(
            target.FolderPath,
            run.Name + writerSetup.FileNameSuffix + writerSetup.FileExtension);

        await IO.File.WriteAllTextAsync(filePath, output.Text).ConfigureAwait(false);
        return filePath;
    }

    /// <summary>
    /// Completes the target.
    /// </summary>
    /// <param name="targetCompletionTracker">The target completion tracker.</param>
    /// <returns>
    /// A <see cref="T:System.Threading.Tasks.Task" />.
    /// </returns>
    public Task CompleteTargetAsync(ITargetCompletionTracker targetCompletionTracker)
    {
        return Task.CompletedTask;
    }
}