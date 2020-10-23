// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IMsBuildWriterSetup.cs" company="Hukano">
// Copyright (c) Hukano. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sundew.Generator.CodeAnalysis.MSBuildWorkspace
{
    using Sundew.Generator.Core;
    using Sundew.Generator.Output;

    /// <summary>
    /// Interface for implementing a target setup for MS Build.
    /// </summary>
    /// <seealso cref="IWriterSetup" />
    [DefaultImplementation(typeof(MsBuildWriterSetup))]
    public interface IMsBuildWriterSetup : IFileWriterSetup
    {
        /// <summary>
        /// Gets a value indicating whether [add files to project].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [add files to project]; otherwise, <c>false</c>.
        /// </value>
        bool AddFilesToProject { get; }
    }
}