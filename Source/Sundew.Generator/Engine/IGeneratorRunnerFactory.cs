// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IGeneratorRunnerFactory.cs" company="Hukano">
// Copyright (c) Hukano. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sundew.Generator.Engine
{
    using System.Collections.Generic;
    using Sundew.Generator.Core;

    /// <summary>
    /// Factory interface for <see cref="IGeneratorRunner"/>.
    /// </summary>
    public interface IGeneratorRunnerFactory
    {
        /// <summary>
        /// Creates an <see cref="IGeneratorRunner" />.
        /// </summary>
        /// <param name="setupInfos">The setupInfos.</param>
        /// <returns>
        /// An <see cref="IGeneratorRunner" />.
        /// </returns>
        IGeneratorRunner Create(IEnumerable<SetupInfo> setupInfos);
    }
}