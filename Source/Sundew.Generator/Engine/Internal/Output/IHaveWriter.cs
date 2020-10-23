// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IHaveWriter.cs" company="Hukano">
// Copyright (c) Hukano. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sundew.Generator.Engine.Internal.Output
{
    using Sundew.Generator.Output;

    /// <summary>
    /// Interface that provides access to the real writer in a WriterAdapter.
    /// </summary>
    internal interface IHaveWriter
    {
        /// <summary>
        /// Gets the writer.
        /// </summary>
        /// <value>
        /// The writer.
        /// </value>
        IWriter Writer { get; }
    }
}