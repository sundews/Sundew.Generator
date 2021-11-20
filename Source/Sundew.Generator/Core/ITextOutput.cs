// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ITextOutput.cs" company="Hukano">
// Copyright (c) Hukano. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sundew.Generator.Core;

using Sundew.Generator.Output;

/// <summary>
/// Interface for providing source text for <see cref="TextFileWriter"/>.
/// </summary>
public interface ITextOutput
{
    /// <summary>
    /// Gets the source text.
    /// </summary>
    /// <value>
    /// The source text.
    /// </value>
    string Text { get; }
}