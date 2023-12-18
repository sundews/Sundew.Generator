// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProgressTrackerToTargetCompletionAdapter.cs" company="Sundews">
// Copyright (c) Sundews. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sundew.Generator.Reporting;

using Sundew.Base.Computation;

internal class ProgressTrackerToTargetCompletionAdapter : ITargetCompletionTracker
{
    private readonly IProgressTracker<Report> progressTracker;

    public ProgressTrackerToTargetCompletionAdapter(IProgressTracker<Report> progressTracker)
    {
        this.progressTracker = progressTracker;
    }

    public void Report(string message)
    {
        this.progressTracker.Report(new Report(ReportType.TargetChanged, message));
    }
}