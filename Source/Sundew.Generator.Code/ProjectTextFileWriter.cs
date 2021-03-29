// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProjectTextFileWriter.cs" company="Hukano">
// Copyright (c) Hukano. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sundew.Generator.Code
{
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Xml.Linq;
    using Sundew.Base.Primitives;
    using Sundew.Generator.Code.CSharp;
    using Sundew.Generator.Core;
    using Sundew.Generator.Output;
    using Sundew.Generator.Reporting;

    /// <summary>
    /// Target handler that writes content to a file.
    /// </summary>
    public class ProjectTextFileWriter : IWriter<IFileWriterSetup, IProject, ICodeRun, ITextOutput>
    {
        /// <summary>
        /// Prepares the target.
        /// </summary>
        /// <param name="writerSetup">The target setup.</param>
        /// <returns>
        /// The <see cref="T:Sundew.Generator.Common.Target" />.
        /// </returns>
        public async Task<IProject> GetTargetAsync(IFileWriterSetup writerSetup)
        {
            var fullTargetPath = Path.GetFullPath(writerSetup.Target);
            var fileInfo = new FileInfo(fullTargetPath);
            var directoryName = fileInfo.DirectoryName ?? string.Empty;
            var projectContent = XDocument.Parse(await IO.File.ReadAllTextAsync(fullTargetPath).ConfigureAwait(false));
            var projectName = ProjectHelper.GetAssemblyName(projectContent, fileInfo.Name);

            return new Project(
                projectName,
                fullTargetPath,
                writerSetup.Folder != null ? Path.Combine(directoryName, writerSetup.Folder) : directoryName,
                ProjectHelper.GetNamespace(projectContent, fileInfo.Name),
                writerSetup.FileNameSuffix + writerSetup.FileExtension);
        }

        /// <summary>
        /// Prepares the target.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <param name="writerSetup">The target setup.</param>
        /// <returns>
        /// The <see cref="T:Sundew.Generator.Common.Target" />.
        /// </returns>
        public Task PrepareTargetAsync(IProject target, IFileWriterSetup writerSetup)
        {
            return Task.CompletedTask;
        }

        /// <summary>
        /// Applies the content to target asynchronous.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <param name="run">The run.</param>
        /// <param name="writerSetup">The writer setup.</param>
        /// <param name="textOutput">The text output.</param>
        /// <returns>
        /// The file path.
        /// </returns>
        public async Task<string> ApplyContentToTargetAsync(IProject target, ICodeRun run, IFileWriterSetup writerSetup, ITextOutput textOutput)
        {
            var filePath = Path.Combine(
                target.FolderPath,
                Path.Combine(NameHelper.GetFolderPath(run.Namespace).ToArray()),
                run.FileName.ToStringOrEmpty());

            await IO.File.WriteAllTextAsync(filePath, textOutput.Text).ConfigureAwait(false);

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
}