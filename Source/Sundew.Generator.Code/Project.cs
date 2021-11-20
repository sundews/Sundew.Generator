// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Project.cs" company="Hukano">
// Copyright (c) Hukano. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sundew.Generator.Code;

using Sundew.Generator.Core;

/// <summary>
/// Extends <see cref="FolderTarget"/> with a root namespace.
/// </summary>
/// <seealso cref="FolderTarget" />
/// <seealso cref="IProject" />
public class Project : FolderTarget, IProject
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Project" /> class.
    /// </summary>
    /// <param name="name">The name.</param>
    /// <param name="path">The file path.</param>
    /// <param name="folderPath">The folder path.</param>
    /// <param name="rootNamespace">The root namespace.</param>
    /// <param name="fileSuffix">The file suffix.</param>
    public Project(string name, string path, string folderPath, string rootNamespace, string fileSuffix)
        : base(name, path, folderPath)
    {
        this.RootNamespace = rootNamespace;
        this.FileSuffix = fileSuffix;
    }

    /// <summary>
    /// Gets the root namespace.
    /// </summary>
    /// <value>
    /// The root namespace.
    /// </value>
    public string RootNamespace { get; init; }

    /// <summary>
    /// Gets the file suffix.
    /// </summary>
    /// <value>
    /// The file suffix.
    /// </value>
    public string FileSuffix { get; init; }
}