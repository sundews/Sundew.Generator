// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CompositeProgressReporter.cs" company="Hukano">
// Copyright (c) Hukano. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sundew.Generator.Reporting
{
    using Sundew.Base.Computation;

    /// <summary>
    /// Composite progress reporter consisting of two progress reporters.
    /// </summary>
    /// <seealso cref="Sundew.Generator.Reporting.IProgressReporter" />
    public class CompositeProgressReporter : IProgressReporter
    {
        private readonly IProgressReporter progressReporter;
        private readonly IProgressReporter nestedProgressReporter;

        /// <summary>
        /// Initializes a new instance of the <see cref="CompositeProgressReporter"/> class.
        /// </summary>
        /// <param name="progressReporter">The progress reporter.</param>
        /// <param name="nestedProgressReporter">The nested progress reporter.</param>
        public CompositeProgressReporter(IProgressReporter progressReporter, IProgressReporter nestedProgressReporter)
        {
            this.progressReporter = progressReporter;
            this.nestedProgressReporter = nestedProgressReporter;
        }

        /// <summary>
        /// Starts this instance.
        /// </summary>
        public void Start()
        {
            this.progressReporter.Start();
            this.nestedProgressReporter.Start();
        }

        /// <summary>
        /// Stops this instance.
        /// </summary>
        public void Stop()
        {
            this.progressReporter.Stop();
            this.nestedProgressReporter.Stop();
        }

        /// <summary>
        /// Reports the specified progress.
        /// </summary>
        /// <param name="progress">The progress.</param>
        public void Report(Progress<Report> progress)
        {
            this.progressReporter.Report(progress);
            this.nestedProgressReporter.Report(progress);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            this.progressReporter.Dispose();
            this.nestedProgressReporter.Dispose();
        }
    }
}