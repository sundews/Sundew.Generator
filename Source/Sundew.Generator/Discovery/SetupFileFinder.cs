// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SetupFileFinder.cs" company="Hukano">
// Copyright (c) Hukano. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sundew.Generator.Discovery
{
    using System.Collections.Generic;
    using System.IO;
    using System.Threading.Tasks;

    /// <summary>
    /// File finder providing files from the specified directory, which matches the search pattern.
    /// </summary>
    /// <seealso cref="ISetupFileFinder" />
    public class SetupFileFinder : ISetupFileFinder
    {
        /// <summary>
        /// Searches the specified directory.
        /// </summary>
        /// <param name="directory">The directory.</param>
        /// <param name="searchPattern">The search pattern.</param>
        /// <returns>
        /// The found files.
        /// </returns>
        public Task<IEnumerable<string>> SearchAsync(string directory, string searchPattern)
        {
            return Sundew.IO.Directory.EnumerateFilesAsync(directory, searchPattern, SearchOption.AllDirectories);
        }
    }
}