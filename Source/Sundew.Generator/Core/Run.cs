// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Run.cs" company="Hukano">
// Copyright (c) Hukano. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sundew.Generator.Core
{
    /// <summary>
    /// Provides information about the run.
    /// </summary>
    public class Run : IRun
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Run" /> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public Run(string name)
        {
            this.Name = name;
        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; }
    }
}