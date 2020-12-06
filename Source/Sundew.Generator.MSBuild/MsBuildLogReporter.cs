// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MsBuildLogReporter.cs" company="Hukano">
// Copyright (c) Hukano. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sundew.Generator.MSBuild
{
    using System;
    using Microsoft.Build.Framework;
    using Microsoft.Build.Utilities;
    using Sundew.Base;
    using Sundew.Generator.Reporting;

    /// <summary>
    /// Implements <see cref="IProgressReporter"/> for logging to MSBuild.
    /// </summary>
    /// <seealso cref="Sundew.Generator.Reporting.IProgressReporter" />
    public class MsBuildLogReporter : IProgressReporter
    {
        private readonly TaskLoggingHelper log;

        /// <summary>
        /// Initializes a new instance of the <see cref="MsBuildLogReporter"/> class.
        /// </summary>
        /// <param name="log">The log.</param>
        public MsBuildLogReporter(TaskLoggingHelper log)
        {
            this.log = log;
        }

        /// <summary>
        /// Starts this instance.
        /// </summary>
        public void Start()
        {
        }

        /// <summary>
        /// Reports the specified progress.
        /// </summary>
        /// <param name="progress">The progress.</param>
        public void Report(Sundew.Base.Computation.Progress<Report> progress)
        {
            switch (progress.Report?.ReportType)
            {
                case ReportType.StartingGeneration:
                    this.log.LogMessage(MessageImportance.Normal, "Starting code generation");
                    break;
                case ReportType.AddingItems:
                    this.log.LogMessage(MessageImportance.Low, $"Added item: {progress.Report.Parameter}");
                    break;
                case ReportType.CompletedAdding:
                    this.log.LogMessage(MessageImportance.Normal, "Completed adding items");
                    break;
                case ReportType.GeneratedItem:
                    this.log.LogMessage(MessageImportance.High, $"Completed item: {progress.Report.Parameter}");
                    break;
                case ReportType.CompletedTarget:
                    this.log.LogMessage(MessageImportance.Low, $"Completed target: {progress.Report.Parameter}");
                    break;
                case ReportType.TargetChanged:
                    this.log.LogMessage(MessageImportance.Low, $"Changed target: {progress.Report.Parameter}");
                    break;
                case ReportType.CompletedGeneration:
                    this.log.LogMessage(MessageImportance.High, "Completed code generation");
                    break;
                case ReportType.Cancelled:
                    this.log.LogMessage(MessageImportance.High, "Cancelled code generation");
                    break;
                case ReportType.Error:
                    this.log.LogError(progress.Report.Parameter.ToStringOrEmpty());
                    break;
                case null:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(progress.Report.ReportType));
            }
        }

        /// <summary>
        /// Stops this instance.
        /// </summary>
        public void Stop()
        {
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
        }
    }
}