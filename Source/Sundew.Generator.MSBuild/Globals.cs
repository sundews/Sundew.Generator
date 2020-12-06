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
        [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:Fields should be private", Justification = "Script globals")]
        public IProgressReporter? ProgressReporter;

        /// <summary>
        /// The setups factory.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:Fields should be private", Justification = "Script globals")]
        public ISetupsFactory? SetupsFactory;
    }
}