// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TextOutput.cs" company="Sundews">
// Copyright (c) Sundews. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sundew.Generator.Core;

/// <summary>
/// Default implementation of <see cref="ITextOutput"/>.
/// </summary>
public class TextOutput : ITextOutput
{
    /// <summary>
    /// Initializes a new instance of the <see cref="TextOutput" /> class.
    /// </summary>
    /// <param name="text">The text.</param>
    public TextOutput(string text)
    {
        this.Text = text;
    }

    /// <summary>
    /// Gets the source text.
    /// </summary>
    /// <value>
    /// The source text.
    /// </value>
    public string Text { get; }
}