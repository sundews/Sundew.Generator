// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ICodeRun.cs" company="Hukano">
// Copyright (c) Hukano. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sundew.Generator.Code
{
    using Sundew.Generator.Core;

    /// <summary>
    /// A run for outputting code.
    /// </summary>
    public interface ICodeRun : IRun
    {
        /// <summary>
        /// Gets the name of the file.
        /// </summary>
        /// <value>
        /// The name of the file.
        /// </value>
        string? FileName { get; }

        /// <summary>
        /// Gets the namespace.
        /// </summary>
        /// <value>
        /// The namespace.
        /// </value>
        string? Namespace { get; }
    }
}