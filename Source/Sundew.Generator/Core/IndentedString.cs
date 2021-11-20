// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IndentedString.cs" company="Hukano">
// Copyright (c) Hukano. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sundew.Generator.Core;

using System;

/// <summary>
/// String class, which adds indentation to the specified string.
/// </summary>
public class IndentedString
{
    private const char IndentChar = ' ';
    private static readonly char[] NewLineChars = Environment.NewLine.ToCharArray();
    private readonly string value;
    private readonly int lineIndent;

    /// <summary>
    /// Initializes a new instance of the <see cref="IndentedString"/> class.
    /// </summary>
    /// <param name="value">The value.</param>
    public IndentedString(string value)
        : this(0, value)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="IndentedString"/> class.
    /// </summary>
    /// <param name="lineIndent">The line indent.</param>
    /// <param name="value">The value.</param>
    public IndentedString(int lineIndent, string value)
    {
        this.value = value;
        this.lineIndent = lineIndent;
    }

    /// <summary>
    /// Performs an implicit conversion from <see cref="IndentedString"/> to <see cref="string"/>.
    /// </summary>
    /// <param name="indentedString">The indented string.</param>
    /// <returns>
    /// The result of the conversion.
    /// </returns>
    public static implicit operator string(IndentedString indentedString)
    {
        return indentedString.ToString();
    }

    /// <summary>
    /// Performs an implicit conversion from <see cref="string"/> to <see cref="IndentedString"/>.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>
    /// The result of the conversion.
    /// </returns>
    public static implicit operator IndentedString(string value)
    {
        return new(value);
    }

    /// <summary>
    /// Returns a <see cref="string" /> that represents this instance.
    /// </summary>
    /// <returns>
    /// A <see cref="string" /> that represents this instance.
    /// </returns>
    public override string ToString()
    {
        if (this.lineIndent > 0)
        {
            var indent = new string(IndentChar, this.lineIndent);
            return
                this.value.Replace(Environment.NewLine, Environment.NewLine + indent)
                    .Replace(Environment.NewLine + indent + Environment.NewLine, Environment.NewLine + Environment.NewLine)
                    .Replace(Environment.NewLine + Environment.NewLine + Environment.NewLine, Environment.NewLine + Environment.NewLine)
                    .TrimEnd(IndentChar)
                    .Trim(NewLineChars);
        }

        return this.value.Trim(NewLineChars);
    }
}