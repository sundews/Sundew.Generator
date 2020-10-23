// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TypeAssemblyLoader.cs" company="Hukano">
// Copyright (c) Hukano. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sundew.Generator.Reflection
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    /// Helper class for loading assemblies.
    /// </summary>
    public static class TypeAssemblyLoader
    {
        private static readonly char[] TypeAssemblySeparator = ", ".ToCharArray();

        /// <summary>
        /// Finds the specified type and loads its assembly if needed.
        /// </summary>
        /// <param name="typeAndAssembly">The type and assembly.</param>
        /// <returns>The loaded type.</returns>
        public static Type GetType(string typeAndAssembly)
        {
            var nameAndAssembly = typeAndAssembly.Split(TypeAssemblySeparator, StringSplitOptions.RemoveEmptyEntries);
            if (nameAndAssembly.Length >= 2)
            {
                var type = TryGetType(nameAndAssembly);
                if (type != null)
                {
                    return type;
                }
            }

            try
            {
                var type = Type.GetType(typeAndAssembly);
                if (type != null)
                {
                    return type;
                }
            }
            catch (Exception e)
            {
                if (e.GetType().Name != "InteractiveAssemblyLoaderException")
                {
                    return TryGetType(nameAndAssembly);
                }

                throw;
            }

            return null;
        }

        private static Type TryGetType(string[] nameAndAssembly)
        {
            var assembly = AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(x => x.GetName().Name == nameAndAssembly[1]);
            if (assembly == null && File.Exists(nameAndAssembly[1] + ".dll"))
            {
                assembly = Assembly.Load(AssemblyName.GetAssemblyName(nameAndAssembly[1] + ".dll"));
            }

            if (assembly != null)
            {
                return assembly.GetType(nameAndAssembly[0]);
            }

            return null;
        }
    }
}