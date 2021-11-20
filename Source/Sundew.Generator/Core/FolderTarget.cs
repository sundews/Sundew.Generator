// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FolderTarget.cs" company="Hukano">
// Copyright (c) Hukano. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sundew.Generator.Core;

/// <summary>
/// Stores info about a file target.
/// </summary>
public class FolderTarget : IFolderTarget
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FolderTarget" /> class.
    /// </summary>
    /// <param name="name">The name.</param>
    /// <param name="path">The path.</param>
    /// <param name="folderPath">The folder path.</param>
    public FolderTarget(string name, string path, string folderPath)
    {
        this.Name = name;
        this.Path = path;
        this.FolderPath = folderPath;
    }

    /// <summary>
    /// Gets the name.
    /// </summary>
    /// <value>
    /// The name.
    /// </value>
    public string Name { get; }

    /// <summary>
    /// Gets the path.
    /// </summary>
    /// <value>
    /// The path.
    /// </value>
    public string Path { get; }

    /// <summary>
    /// Gets the folder path.
    /// </summary>
    /// <value>
    /// The folder path.
    /// </value>
    public string FolderPath { get; }
}