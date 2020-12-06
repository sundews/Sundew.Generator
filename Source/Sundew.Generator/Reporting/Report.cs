// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Report.cs" company="Hukano">
// Copyright (c) Hukano. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sundew.Generator.Reporting
{
    /// <summary>
    /// Report for reporting progress including the current level.
    /// </summary>
    public class Report
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Report" /> class.
        /// </summary>
        /// <param name="reportType">Type of the report.</param>
        /// <param name="parameter">The parameter.</param>
        internal Report(ReportType reportType, object? parameter = null)
        {
            this.ReportType = reportType;
            this.Parameter = parameter;
        }

        /// <summary>
        /// Gets the type of the report.
        /// </summary>
        /// <value>
        /// The type of the report.
        /// </value>
        public ReportType ReportType { get; }

        /// <summary>
        /// Gets the parameter.
        /// </summary>
        /// <value>
        /// The parameter.
        /// </value>
        public object? Parameter { get; }
    }
}