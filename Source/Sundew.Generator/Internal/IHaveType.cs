// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IHaveType.cs" company="Sundews">
// Copyright (c) Sundews. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sundew.Generator.Internal;

using System;

/// <summary>
/// Interface that knows a type.
/// </summary>
internal interface IHaveType
{
    /// <summary>
    /// Gets the type.
    /// </summary>
    /// <value>
    /// The type.
    /// </value>
    public Type Type { get; }
}