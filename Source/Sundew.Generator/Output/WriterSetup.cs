// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WriterSetup.cs" company="Hukano">
// Copyright (c) Hukano. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sundew.Generator.Output
{
    using Sundew.Generator.Core;

    /// <summary>
    /// Default implementation of <see cref="IWriterSetup"/>.
    /// </summary>
    /// <seealso cref="IWriterSetup" />
    public class WriterSetup : IWriterSetup
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WriterSetup"/> class.
        /// </summary>
        public WriterSetup()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WriterSetup" /> class.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <param name="writer">The writer.</param>
        public WriterSetup(string target, TypeOrObject<IWriter> writer)
        {
            this.Target = target;
            this.Writer = writer;
        }

        /// <summary>
        /// Gets the target.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public string Target { get; init; }

        /// <summary>
        /// Gets the writer.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public TypeOrObject<IWriter> Writer { get; init; }
    }
}