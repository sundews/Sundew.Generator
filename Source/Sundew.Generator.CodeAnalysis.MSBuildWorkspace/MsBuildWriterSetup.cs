// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MsBuildWriterSetup.cs" company="Sundews">
// Copyright (c) Sundews. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sundew.Generator.CodeAnalysis.MSBuildWorkspace;

using Sundew.Generator.Core;
using Sundew.Generator.Output;

/// <summary>
/// Default implementation of <see cref="IMsBuildWriterSetup"/>.
/// </summary>
/// <seealso cref="IMsBuildWriterSetup" />
public class MsBuildWriterSetup : FileWriterSetup, IMsBuildWriterSetup
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MsBuildWriterSetup" /> class.
    /// </summary>
    /// <param name="target">The target.</param>
    public MsBuildWriterSetup(string target)
        : base(target)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="MsBuildWriterSetup" /> class.
    /// </summary>
    /// <param name="fileExtension">The file extension.</param>
    /// <param name="fileNameSuffix">The file name suffix.</param>
    /// <param name="folder">The folder.</param>
    /// <param name="target">The target.</param>
    /// <param name="writer">The writer.</param>
    /// <param name="addFilesToProject">if set to <c>true</c> [add files to project].</param>
    public MsBuildWriterSetup(string fileExtension, string fileNameSuffix, string folder, string target, TypeOrObject<IWriter> writer, bool addFilesToProject)
        : base(fileExtension, fileNameSuffix, folder, target, writer)
    {
        this.AddFilesToProject = addFilesToProject;
    }

    /// <summary>
    /// Gets a value indicating whether [add files to project].
    /// </summary>
    /// <value>
    ///   <c>true</c> if [add files to project]; otherwise, <c>false</c>.
    /// </value>
    public bool AddFilesToProject { get; init; }
}