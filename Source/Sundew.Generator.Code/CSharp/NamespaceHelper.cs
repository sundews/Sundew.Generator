// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NamespaceHelper.cs" company="Hukano">
// Copyright (c) Hukano. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sundew.Generator.Code.CSharp
{
    using System.Collections.Generic;

    /// <summary>
    /// Helper class for creating namespaces.
    /// </summary>
    public static class NamespaceHelper
    {
        /// <summary>
        /// Gets the namespace.
        /// </summary>
        /// <param name="rootNamespace">The root namespace.</param>
        /// <param name="folderPath">The folder path.</param>
        /// <returns>A C# namespace.</returns>
        public static string GetNamespace(string rootNamespace, IReadOnlyList<string> folderPath)
        {
            var folderNamespace = GetNamespace(folderPath);
            return CombineNamespaces(rootNamespace, folderNamespace);
        }

        /// <summary>Combines the two namespaces.</summary>
        /// <param name="rootNamespace">The root namespace.</param>
        /// <param name="otherNamespace">The other namespace.</param>
        public static string CombineNamespaces(string rootNamespace, string otherNamespace)
        {
            if (string.IsNullOrEmpty(rootNamespace))
            {
                return otherNamespace;
            }

            if (string.IsNullOrEmpty(otherNamespace))
            {
                return rootNamespace;
            }

            return rootNamespace + $".{otherNamespace}";
        }

        /// <summary>
        /// Gets the namespace.
        /// </summary>
        /// <param name="folderPath">The folder path.</param>
        /// <returns>A C# namespace.</returns>
        public static string GetNamespace(IReadOnlyList<string> folderPath)
        {
            if (folderPath == null || folderPath.Count == 0)
            {
                return string.Empty;
            }

            return string.Join(".", folderPath);
        }
    }
}