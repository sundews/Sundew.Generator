// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ITarget.cs" company="Hukano">
// Copyright (c) Hukano. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sundew.Generator.Core;

/// <summary>
/// Interface for implementing a target info.
/// </summary>
public interface ITarget
{
    /// <summary>
    /// Gets the name.
    /// </summary>
    /// <value>
    /// The name.
    /// </value>
    string Name { get; }
}