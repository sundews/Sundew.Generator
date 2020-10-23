// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SimpleProgressReporter.cs" company="Hukano">
// Copyright (c) Hukano. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sundew.Generator.Reporting
{
    using System.IO;

    /// <summary>
    /// A simple progress reporter.
    /// </summary>
    public class SimpleProgressReporter : IProgressReporter
    {
        private readonly TextWriter textWriter;

        /// <summary>
        /// Initializes a new instance of the <see cref="SimpleProgressReporter"/> class.
        /// </summary>
        /// <param name="textWriter">The text writer.</param>
        public SimpleProgressReporter(TextWriter textWriter)
        {
            this.textWriter = textWriter;
        }

        /// <summary>
        /// Starts this instance.
        /// </summary>
        public void Start()
        {
        }

        /// <summary>
        /// Stops this instance.
        /// </summary>
        public void Stop()
        {
        }

        /// <summary>
        /// Reports the specified progress.
        /// </summary>
        /// <param name="progress">The progress.</param>
        public void Report(Sundew.Base.Computation.Progress<Report> progress)
        {
            if (progress.Report == null)
            {
                return;
            }

            switch (progress.Report.ReportType)
            {
                case ReportType.StartingGeneration:
                    this.textWriter.WriteLine("Starting generation");
                    break;
                case ReportType.GeneratedItem:
                case ReportType.CompletedTarget:
                    this.textWriter.WriteLine(Path.GetFileName(progress.Report.Parameter.ToString()));
                    break;
                case ReportType.CompletedGeneration:
                    this.textWriter.WriteLine("Completed generation");
                    break;
            }
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
        }
    }
}