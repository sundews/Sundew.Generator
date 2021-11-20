// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CodeRun.cs" company="Hukano">
// Copyright (c) Hukano. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sundew.Generator.Code;

using Sundew.Generator.Core;

/// <summary>
/// A run for outputting code.
/// </summary>
/// <seealso cref="Sundew.Generator.Core.Run" />
/// <seealso cref="ICodeRun" />
public class CodeRun : Run, ICodeRun
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CodeRun"/> class.
    /// </summary>
    /// <param name="name">The name.</param>
    public CodeRun(string name)
        : base(name)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="CodeRun" /> class.
    /// </summary>
    /// <param name="name">The name.</param>
    /// <param name="fileName">Name of the file.</param>
    /// <param name="namespace">The namespace.</param>
    public CodeRun(string name, string? fileName, string? @namespace)
        : base(name)
    {
        this.FileName = fileName;
        this.Namespace = @namespace;
    }

    /// <summary>
    /// Gets the name of the file.
    /// </summary>
    /// <value>
    /// The name of the file.
    /// </value>
    public string? FileName { get; init; }

    /// <summary>
    /// Gets the namespace.
    /// </summary>
    /// <value>
    /// The namespace.
    /// </value>
    public string? Namespace { get; init; }
}