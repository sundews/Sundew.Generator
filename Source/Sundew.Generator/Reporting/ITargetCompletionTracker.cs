// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ITargetCompletionTracker.cs" company="Hukano">
// Copyright (c) Hukano. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sundew.Generator.Reporting
{
    /// <summary>
    /// Interface for implementing a completion tracker.
    /// </summary>
    public interface ITargetCompletionTracker
    {
        /// <summary>
        /// Reports the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        void Report(string message);
    }
}