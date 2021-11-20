// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InitializationException.cs" company="Hukano">
// Copyright (c) Hukano. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sundew.Generator.Core;

/// <summary>
/// Exception for indicating an exception during initialization.
/// </summary>
/// <seealso cref="GenerationException" />
public sealed class InitializationException : GenerationException
{
    /// <summary>
    /// Initializes a new instance of the <see cref="InitializationException" /> class.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    /// <param name="setupOrigin">The setup path.</param>
    /// <param name="property">The property.</param>
    public InitializationException(string message, string setupOrigin, string? property)
        : base(message)
    {
        this.SetupOrigin = setupOrigin;
        this.Property = property;
    }

    /// <summary>
    /// Gets the setup origin.
    /// </summary>
    /// <value>
    /// The setup origin.
    /// </value>
    public string SetupOrigin { get; }

    /// <summary>
    /// Gets the property.
    /// </summary>
    /// <value>
    /// The property.
    /// </value>
    public string? Property { get; }
}