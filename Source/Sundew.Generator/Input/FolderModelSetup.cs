// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FolderModelSetup.cs" company="Hukano">
// Copyright (c) Hukano. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sundew.Generator.Input;

using System;
using Sundew.Generator.Core;

/// <summary>
/// Default file model setup.
/// </summary>
/// <seealso cref="Sundew.Generator.Input.ModelSetup" />
/// <seealso cref="IFolderModelSetup" />
public class FolderModelSetup : ModelSetup, IFolderModelSetup
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FolderModelSetup"/> class.
    /// </summary>
    public FolderModelSetup()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="FolderModelSetup"/> class.
    /// </summary>
    /// <param name="provider">The provider.</param>
    /// <param name="modelType">Type of the model.</param>
    /// <param name="folder">The folder.</param>
    /// <param name="filesSearchPattern">The files search pattern.</param>
    public FolderModelSetup(TypeOrObject<IModelProvider> provider, Type modelType, string folder, string filesSearchPattern)
        : base(provider, modelType)
    {
        this.Folder = folder;
        this.FilesSearchPattern = filesSearchPattern;
    }

    /// <summary>
    /// Gets the folder.
    /// </summary>
    /// <value>
    /// The folder.
    /// </value>
    public string? Folder { get; init; }

    /// <summary>
    /// Gets the files search pattern.
    /// </summary>
    /// <value>
    /// The files search pattern.
    /// </value>
    public string? FilesSearchPattern { get; init; }
}