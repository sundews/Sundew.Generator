// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProjectTextFileWriter.cs" company="Hukano">
// Copyright (c) Hukano. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sundew.Generator.CodeAnalysis.MSBuildWorkspace
{
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Xml.Linq;
    using Sundew.Base.Collections;
    using Sundew.Generator.Code;
    using Sundew.Generator.Code.CSharp;
    using Sundew.Generator.Core;
    using Sundew.Generator.Output;
    using Sundew.Generator.Reporting;

    /// <summary>
    /// MS Build target handler, saves generated output to files and adds them to target projects if needed.
    /// </summary>
    /// <seealso cref="IWriter{TTargetSetup,TTarget,IRun,TOutput}" />
    public class ProjectTextFileWriter : IWriter<IMsBuildWriterSetup, IProject, ICodeRun, ITextOutput>
    {
        private Task<Microsoft.CodeAnalysis.MSBuild.MSBuildWorkspace> workspaceTask;
        private Microsoft.CodeAnalysis.MSBuild.MSBuildWorkspace workspace;
        private Microsoft.CodeAnalysis.Project originalProject;
        private Microsoft.CodeAnalysis.Project project;

        /// <summary>
        /// Prepares the target.
        /// </summary>
        /// <param name="writerSetup">The target setup.</param>
        /// <returns>
        /// The <see cref="T:Sundew.Generation.Common.Target" />.
        /// </returns>
        public async Task<IProject> GetTargetAsync(IMsBuildWriterSetup writerSetup)
        {
            var fullTargetPath = Path.GetFullPath(writerSetup.Target);
            if (writerSetup.AddFilesToProject)
            {
                this.workspaceTask = MSBuildWorkspaceFactory.CreateAsync();
            }

            var fileInfo = new FileInfo(fullTargetPath);
            var projectContent = XDocument.Parse(await IO.File.ReadAllTextAsync(fullTargetPath).ConfigureAwait(false));
            var fallbackName = Path.GetFileNameWithoutExtension(fileInfo.Name);
            var projectName = ProjectHelper.GetAssemblyName(projectContent, fallbackName);

            return new Project(
                projectName,
                fullTargetPath,
                fileInfo.DirectoryName,
                ProjectHelper.GetNamespace(projectContent, fallbackName),
                writerSetup.FileNameSuffix + writerSetup.FileExtension);
        }

        /// <summary>
        /// Prepares the target.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <param name="writerSetup">The target setup.</param>
        /// <returns>
        /// The <see cref="T:Sundew.Generation.Common.Target" />.
        /// </returns>
        public async Task PrepareTargetAsync(IProject target, IMsBuildWriterSetup writerSetup)
        {
            if (writerSetup.AddFilesToProject)
            {
                this.workspace = await this.workspaceTask.ConfigureAwait(false);
                this.originalProject = this.project = await this.workspace.OpenProjectAsync(target.Path).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Applies the content to target.
        /// </summary>
        /// <param name="target">The target information.</param>
        /// <param name="run">The target output information.</param>
        /// <param name="writerSetup">The writer setup.</param>
        /// <param name="textOutput">The output.</param>
        /// <returns>
        /// A <see cref="Task" />.
        /// </returns>
        public async Task<string> ApplyContentToTargetAsync(IProject target, ICodeRun run, IMsBuildWriterSetup writerSetup, ITextOutput textOutput)
        {
            var filePath = Path.Combine(
                target.FolderPath,
                Path.Combine(NameHelper.GetFolderPath(run.Namespace).ToArrayIfNeeded()),
                run.Name + writerSetup.FileNameSuffix + writerSetup.FileExtension);

            if (this.project == null)
            {
                await IO.File.WriteAllTextAsync(filePath, textOutput.Text).ConfigureAwait(false);
            }
            else
            {
                var document = this.project.Documents.FirstOrDefault(x => filePath.Equals(x.FilePath));
                if (document != null)
                {
                    await IO.File.WriteAllTextAsync(filePath, textOutput.Text).ConfigureAwait(false);
                }
                else
                {
                    document = this.project.AddDocument(run.Name, textOutput.Text, NameHelper.GetFolderPath(run.Namespace), filePath);
                    this.project = document.Project;
                }
            }

            return filePath;
        }

        /// <summary>
        /// Completes the target.
        /// </summary>
        /// <param name="targetCompletionTracker">The target completion tracker.</param>
        /// <returns>
        /// A <see cref="Task" />.
        /// </returns>
        public Task CompleteTargetAsync(ITargetCompletionTracker targetCompletionTracker)
        {
            if (this.workspace != null && this.project != null && !ReferenceEquals(this.originalProject, this.project))
            {
                targetCompletionTracker.Report(this.workspace.TryApplyChanges(this.project.Solution)
                    ? $"Saved project: {this.project.Name}"
                    : $"Error saving project: {this.project.Name}");
            }

            this.workspace?.CloseSolution();
            return Task.CompletedTask;
        }
    }
}
