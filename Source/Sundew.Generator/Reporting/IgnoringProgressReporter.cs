// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IgnoringProgressReporter.cs" company="Hukano">
// Copyright (c) Hukano. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sundew.Generator.Reporting
{
    using Sundew.Base.Primitives.Computation;

    /// <summary>
    /// Implementation of <see cref="IProgressReporter{TReport}"/> that ignores reports.
    /// </summary>
    /// <seealso cref="IProgressReporter{Report}" />
    public class IgnoringProgressReporter : IProgressReporter<Report>
    {
        /// <summary>
        /// Gets the default ignoring progress tracker.
        /// </summary>
        /// <value>
        /// The default.
        /// </value>
        public static IProgressTracker<Report> Default { get; } = new ProgressTracker<Report>(new IgnoringProgressReporter());

        /// <summary>
        /// Reports the specified progress.
        /// </summary>
        /// <param name="progress">The progress.</param>
        public void Report(Progress<Report> progress)
        {
        }
    }
}