// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NameHelper.cs" company="Hukano">
// Copyright (c) Hukano. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sundew.Generator.Code.CSharp
{
    using System.Collections.Generic;

    /// <summary>
    /// Helper class for creating file names.
    /// </summary>
    public static class NameHelper
    {
        private const char Dot = '.';

        /// <summary>
        /// Gets the folder path.
        /// </summary>
        /// <param name="namespace">The namespace.</param>
        /// <returns>
        /// The folder path.
        /// </returns>
        public static IReadOnlyList<string> GetFolderPath(string @namespace)
        {
            return string.IsNullOrEmpty(@namespace)
                ? new string[0]
                : @namespace.Split(Dot);
        }
    }
}