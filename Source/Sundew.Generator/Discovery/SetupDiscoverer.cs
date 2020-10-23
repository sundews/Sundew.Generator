// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SetupDiscoverer.cs" company="Hukano">
// Copyright (c) Hukano. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sundew.Generator.Discovery
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Sundew.Base.Collections;
    using Sundew.Generator.Core;
    using Sundew.Generator.Engine;

    /// <summary>
    /// Discovers generators and provides a batch processor for the found generators.
    /// </summary>
    /// <seealso cref="ISetupDiscoverer" />
    public class SetupDiscoverer : ISetupDiscoverer
    {
        private readonly ISetupFileFinder setupFileFinder;
        private readonly ISetupProvider setupProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="SetupDiscoverer" /> class.
        /// </summary>
        /// <param name="setupFileFinder">The setup file finder.</param>
        /// <param name="setupProvider">The setup provider.</param>
        public SetupDiscoverer(ISetupFileFinder setupFileFinder, ISetupProvider setupProvider)
        {
            this.setupFileFinder = setupFileFinder;
            this.setupProvider = setupProvider;
        }

        /// <summary>
        /// Discovers files for Text generation in the specified directory.
        /// </summary>
        /// <param name="directory">The directory.</param>
        /// <param name="searchPattern">The search pattern.</param>
        /// <returns>A <see cref="IGeneratorRunner"/>.</returns>
        public async Task<IEnumerable<SetupInfo>> DiscoverAsync(string directory, string searchPattern)
        {
            var setupFiles = await this.setupFileFinder.SearchAsync(directory, searchPattern).ConfigureAwait(false);
            return await setupFiles.SelectAsync(
                async filePath =>
                {
                    var setup = await this.setupProvider.GetSetupAsync(filePath).ConfigureAwait(false);
                    return new SetupInfo(filePath, setup);
                }).ConfigureAwait(false);
        }
    }
}
