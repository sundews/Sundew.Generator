// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ReportType.cs" company="Hukano">
// Copyright (c) Hukano. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sundew.Generator.Reporting
{
    /// <summary>
    /// Indicates the type of report.
    /// </summary>
    public enum ReportType
    {
        /// <summary>
        /// Report for starting generation.
        /// </summary>
        StartingGeneration,

        /// <summary>
        /// The adding items
        /// </summary>
        AddingItems,

        /// <summary>
        /// The completed adding
        /// </summary>
        CompletedAdding,

        /// <summary>
        /// The generated item
        /// </summary>
        GeneratedItem,

        /// <summary>
        /// The applied content to the target.
        /// </summary>
        AppliedContent,

        /// <summary>
        /// Report for target completed.
        /// </summary>
        CompletedTarget,

        /// <summary>
        /// Report for target changed.
        /// </summary>
        TargetChanged,

        /// <summary>
        /// Report for generation completed.
        /// </summary>
        CompletedGeneration,

        /// <summary>
        /// The cancelled.
        /// </summary>
        Cancelled,

        /// <summary>
        /// Report for an error.
        /// </summary>
        Error,
    }
}