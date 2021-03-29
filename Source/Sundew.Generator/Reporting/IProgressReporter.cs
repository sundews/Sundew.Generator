// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IProgressReporter.cs" company="Hukano">
// Copyright (c) Hukano. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sundew.Generator.Reporting
{
    using System;
    using Sundew.Base.Primitives.Computation;

    /// <summary>
    /// Interface for implementing a progress reporter.
    /// </summary>
    public interface IProgressReporter : IProgressReporter<Report>, IDisposable
    {
        /// <summary>
        /// Starts this instance.
        /// </summary>
        void Start();

        /// <summary>
        /// Stops this instance.
        /// </summary>
        void Stop();
    }
}
