// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SetupInfoFactory.cs" company="Sundews">
// Copyright (c) Sundews. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sundew.Generator.Discovery;

using System;
using System.Collections.Generic;
using System.Linq;
using Sundew.Generator.Core;

/// <summary>
/// Factory for creating a <see cref="SetupInfo"/>s.
/// </summary>
public static class SetupInfoFactory
{
    /// <summary>
    /// Creates the setup infos.
    /// </summary>
    /// <param name="setups">The setups.</param>
    /// <returns>An <see cref="IEnumerable{SetupInfo}"/>.</returns>
    public static IEnumerable<SetupInfo> CreateSetupInfos(IEnumerable<ISetup> setups)
    {
        return setups.Select((setup, index) => new SetupInfo(Environment.StackTrace, setup));
    }
}