// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SetupInfo.cs" company="Hukano">
// Copyright (c) Hukano. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sundew.Generator.Core;

/// <summary>
/// Contains an <see cref="ISetup"/> and its origin.
/// </summary>
public sealed class SetupInfo
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SetupInfo" /> class.
    /// </summary>
    /// <param name="origin">The setup origin.</param>
    /// <param name="setup">The setup.</param>
    public SetupInfo(string origin, ISetup setup)
    {
        this.Origin = origin;
        this.Setup = setup;
    }

    /// <summary>
    /// Gets the setup origin.
    /// </summary>
    /// <value>
    /// The setup origin.
    /// </value>
    public string Origin { get; }

    /// <summary>
    /// Gets the setup.
    /// </summary>
    /// <value>
    /// The setup.
    /// </value>
    public ISetup Setup { get; }
}