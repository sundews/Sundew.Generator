// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProgressReporterFactory.cs" company="Hukano">
// Copyright (c) Hukano. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sundew.Generator.Reporting
{
    using System.IO;

    /// <summary>
    /// Factory for creating a progress reporter.
    /// </summary>
    public class ProgressReporterFactory
    {
        /// <summary>
        /// Creates the specified text writer.
        /// </summary>
        /// <param name="progressReporter">The generator options progress reporter.</param>
        /// <param name="textWriter">The text writer.</param>
        /// <returns>
        /// A progress reporter.
        /// </returns>
        public IProgressReporter Create(
            IProgressReporter progressReporter,
            TextWriter textWriter = null)
        {
            var simpleProgressReporter = textWriter != null ? new SimpleProgressReporter(textWriter) : null;
            if (progressReporter != null && simpleProgressReporter != null)
            {
                return new CompositeProgressReporter(progressReporter, simpleProgressReporter);
            }

            if (simpleProgressReporter != null)
            {
                return simpleProgressReporter;
            }

            return progressReporter ?? new ConsoleProgressReporter();
        }
    }
}
