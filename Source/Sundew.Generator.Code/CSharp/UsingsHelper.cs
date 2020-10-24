// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UsingsHelper.cs" company="Hukano">
// Copyright (c) Hukano. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sundew.Generator.Code.CSharp
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Helper class for creating CSharp usings.
    /// </summary>
    public static class UsingsHelper
    {
        /// <summary>
        /// Gets the usings.
        /// </summary>
        /// <param name="useGlobalUsings">if set to <c>true</c> [use global usings].</param>
        /// <param name="indent">The indent.</param>
        /// <param name="usings">The usings.</param>
        /// <returns>A string containing usings.</returns>
        public static string GetUsings(bool useGlobalUsings, int indent, params IEnumerable<string>[] usings)
        {
            return GetUsings(useGlobalUsings, indent, true, usings);
        }

        /// <summary>
        /// Gets the usings.
        /// </summary>
        /// <param name="useGlobalUsings">if set to <c>true</c> [use global usings].</param>
        /// <param name="indent">The indent.</param>
        /// <param name="placeSystemUsingsFirst">if set to <c>true</c> [place system usings first].</param>
        /// <param name="usings">The usings.</param>
        /// <returns>A string containing usings.</returns>
        public static string GetUsings(bool useGlobalUsings, int indent, bool placeSystemUsingsFirst, params IEnumerable<string>[] usings)
        {
            var stringBuilder = new StringBuilder();
            IEnumerable<string> flattenedUsings = usings.Where(x => x != null).SelectMany(x => x).Distinct().OrderBy(x => x);
            if (placeSystemUsingsFirst)
            {
                var groupedUsings = flattenedUsings.GroupBy(x => x.StartsWith("System.") || x.Equals("System"));
                var groupSortedUsings = Enumerable.Empty<string>();
                foreach (var groupedUsing in groupedUsings.OrderByDescending(x => x.Key))
                {
                    groupSortedUsings = groupSortedUsings.Concat(groupedUsing);
                }

                flattenedUsings = groupSortedUsings;
            }

            var global = string.Empty;
            if (useGlobalUsings)
            {
                global = "global::";
            }

            foreach (var @using in flattenedUsings)
            {
                stringBuilder.Append(' ', indent)
                             .Append("using ")
                             .Append(global)
                             .Append(@using)
                             .AppendLine(";");
            }

            if (stringBuilder.Length < 3)
            {
                return string.Empty;
            }

            return stringBuilder.ToString(0, stringBuilder.Length - Environment.NewLine.Length);
        }
    }
}