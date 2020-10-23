// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ICodeSetup.cs" company="Hukano">
// Copyright (c) Hukano. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sundew.Generator.Code
{
    using System.Collections.Generic;
    using Sundew.Generator.Core;

    /// <summary>
    /// Interface for implementing a code setup.
    /// </summary>
    /// <seealso cref="ISetup" />
    [DefaultImplementation(typeof(CodeSetup))]
    public interface ICodeSetup : ISetup
    {
        /// <summary>
        /// Gets the target namespace.
        /// </summary>
        /// <value>
        /// The target namespace.
        /// </value>
        string TargetNamespace { get; }

        /// <summary>
        /// Gets the usings.
        /// </summary>
        /// <value>
        /// The usings.
        /// </value>
        IReadOnlyList<string> Usings { get; }

        /// <summary>
        /// Gets a value indicating whether [use global usings].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [use global usings]; otherwise, <c>false</c>.
        /// </value>
        bool UseGlobalUsings { get; }
    }
}