// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IGeneratorRunner.cs" company="Hukano">
// Copyright (c) Hukano. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sundew.Generator.Engine
{
    using System.Collections.Concurrent;
    using System.Threading.Tasks;
    using Sundew.Base.Computation;
    using Sundew.Generator.Reporting;

    /// <summary>
    /// Interface for implementing a generator engine.
    /// </summary>
    public interface IGeneratorRunner
    {
        /// <summary>
        /// Run the generation asynchronously.
        /// </summary>
        /// <param name="generatorOptions">The generation options.</param>
        /// <param name="progressTracker">The generation progress tracker.</param>
        /// <returns>
        /// An async task.
        /// </returns>
        Task<ConcurrentBag<string>> GenerateAsync(IGeneratorOptions generatorOptions, IProgressTracker<Report> progressTracker = null);
    }
}