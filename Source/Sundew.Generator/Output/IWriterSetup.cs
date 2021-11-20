// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IWriterSetup.cs" company="Hukano">
// Copyright (c) Hukano. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sundew.Generator.Output;

using Sundew.Generator.Core;

/// <summary>
/// Interface for the generator setup.
/// </summary>
[DefaultImplementation(typeof(WriterSetup))]
public interface IWriterSetup
{
    /// <summary>
    /// Gets the target.
    /// </summary>
    /// <value>
    /// The type.
    /// </value>
    string Target { get; }

    /// <summary>
    /// Gets the writer.
    /// </summary>
    /// <value>
    /// The writer.
    /// </value>
    TypeOrObject<IWriter>? Writer { get; }
}