// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FileWriterSetup.cs" company="Hukano">
// Copyright (c) Hukano. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sundew.Generator.Output
{
    using Newtonsoft.Json;
    using Sundew.Generator.Core;

    /// <summary>
    /// A file writer setup.
    /// </summary>
    /// <seealso cref="Sundew.Generator.Output.WriterSetup" />
    /// <seealso cref="Sundew.Generator.Output.IFileWriterSetup" />
    public class FileWriterSetup : WriterSetup, IFileWriterSetup
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FileWriterSetup" /> class.
        /// </summary>
        /// <param name="target">The target.</param>
        public FileWriterSetup(string target)
         : base(target)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FileWriterSetup" /> class.
        /// </summary>
        /// <param name="fileExtension">The file extension.</param>
        /// <param name="fileNameSuffix">The file name suffix.</param>
        /// <param name="folder">The folder.</param>
        /// <param name="target">The target.</param>
        /// <param name="writer">The writer.</param>
        [JsonConstructor]
        public FileWriterSetup(string fileExtension, string fileNameSuffix, string folder, string target, TypeOrObject<IWriter> writer)
            : base(target, writer)
        {
            this.FileExtension = fileExtension;
            this.FileNameSuffix = fileNameSuffix;
            this.Folder = folder;
        }

        /// <summary>
        /// Gets the file extension.
        /// </summary>
        /// <value>
        /// The file extension.
        /// </value>
        public string? FileExtension { get; init; }

        /// <summary>
        /// Gets the file name suffix.
        /// </summary>
        /// <value>
        /// The file name suffix.
        /// </value>
        public string? FileNameSuffix { get; init; }

        /// <summary>
        /// Gets the folder.
        /// </summary>
        /// <value>
        /// The folder.
        /// </value>
        public string? Folder { get; init; }
    }
}