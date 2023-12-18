// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConsoleLine.cs" company="Sundews">
// Copyright (c) Sundews. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sundew.Generator.Reporting;

/// <summary>
/// Represents a line printed to the console.
/// </summary>
public class ConsoleLine
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ConsoleLine"/> class.
    /// </summary>
    /// <param name="overwriteLastLine">if set to <c>true</c> [overwrite last line].</param>
    /// <param name="text">The text.</param>
    public ConsoleLine(bool overwriteLastLine, string text)
    {
        this.OverwriteLastLine = overwriteLastLine;
        this.Text = text;
    }

    /// <summary>
    /// Gets a value indicating whether [overwrite last line].
    /// </summary>
    /// <value>
    ///   <c>true</c> if [overwrite last line]; otherwise, <c>false</c>.
    /// </value>
    public bool OverwriteLastLine { get; }

    /// <summary>
    /// Gets the text.
    /// </summary>
    /// <value>
    /// The text.
    /// </value>
    public string Text { get; }
}