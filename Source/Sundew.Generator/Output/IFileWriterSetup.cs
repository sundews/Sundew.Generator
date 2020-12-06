// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IFileWriterSetup.cs" company="Hukano">
// Copyright (c) Hukano. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sundew.Generator.Output
{
    using Sundew.Generator.Core;

    /// <summary>
    /// Interface for implementing a file writer setup.
    /// </summary>
    [DefaultImplementation(typeof(FileWriterSetup))]
    public interface IFileWriterSetup : IWriterSetup
    {
        /// <summary>
        /// Gets the file extension.
        /// </summary>
        /// <value>
        /// The file extension.
        /// </value>
        string? FileExtension { get; }

        /// <summary>
        /// Gets the file name suffix.
        /// </summary>
        /// <value>
        /// The file name suffix.
        /// </value>
        string? FileNameSuffix { get; }

        /// <summary>
        /// Gets the folder.
        /// </summary>
        /// <value>
        /// The folder.
        /// </value>
        string? Folder { get; }
    }
}