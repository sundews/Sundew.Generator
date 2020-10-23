// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Globals.cs" company="Hukano">
// Copyright (c) Hukano. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sundew.Generator.MSBuild
{
    using Sundew.Generator.Discovery;
    using Sundew.Generator.Reporting;

    /// <summary>
    /// Contains globals for code generation script.
    /// </summary>
    public class Globals
    {
        /// <summary>
        /// The progress reporter.
        /// </summary>
        public IProgressReporter ProgressReporter;

        /// <summary>
        /// The setups factory.
        /// </summary>
        public ISetupsFactory SetupsFactory;
    }
}