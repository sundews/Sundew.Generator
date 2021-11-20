// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IProject.cs" company="Hukano">
// Copyright (c) Hukano. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sundew.Generator.Code;

using Sundew.Generator.Core;

/// <summary>
/// Interface for project info derived from <see cref="ITarget"/>.
/// </summary>
public interface IProject : IFolderTarget
{
    /// <summary>
    /// Gets the root namespace.
    /// </summary>
    /// <value>
    /// The root namespace.
    /// </value>
    string RootNamespace { get; }

    /// <summary>
    /// Gets the file suffix.
    /// </summary>
    /// <value>
    /// The file suffix.
    /// </value>
    string FileSuffix { get; }
}