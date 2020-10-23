// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IFolderTarget.cs" company="Hukano">
// Copyright (c) Hukano. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sundew.Generator.Core
{
    /// <summary>
    /// Interface for implementing a file target.
    /// </summary>
    public interface IFolderTarget : ITarget
    {
        /// <summary>
        /// Gets the file path.
        /// </summary>
        /// <value>
        /// The file path.
        /// </value>
        string Path { get; }

        /// <summary>
        /// Gets the folder path.
        /// </summary>
        /// <value>
        /// The folder path.
        /// </value>
        string FolderPath { get; }
    }
}