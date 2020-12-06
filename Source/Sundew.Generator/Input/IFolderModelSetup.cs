// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IFolderModelSetup.cs" company="Hukano">
// Copyright (c) Hukano. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sundew.Generator.Input
{
    /// <summary>
    /// Interface for implementing a file model setup.
    /// </summary>
    /// <seealso cref="Sundew.Generator.Input.IModelSetup" />
    public interface IFolderModelSetup : IModelSetup
    {
        /// <summary>
        /// Gets the folder.
        /// </summary>
        /// <value>
        /// The folder.
        /// </value>
        string? Folder { get; }

        /// <summary>
        /// Gets the files search pattern.
        /// </summary>
        /// <value>
        /// The files search pattern.
        /// </value>
        string? FilesSearchPattern { get; }
    }
}