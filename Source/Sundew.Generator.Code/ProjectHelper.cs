// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProjectHelper.cs" company="Hukano">
// Copyright (c) Hukano. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sundew.Generator.Code;

using System;
using System.Linq;
using System.Xml.Linq;

/// <summary>
/// Helper class for MSBuild projects.
/// </summary>
public static class ProjectHelper
{
    private const string PropertyGroupText = "PropertyGroup";
    private const string RootNamespaceText = "RootNamespace";
    private const string DefaultNamespaceText = "DefaultNamespace";
    private const string AssemblyNameText = "AssemblyName";

    /// <summary>
    /// Gets the namespace.
    /// </summary>
    /// <param name="xDocument">The x document.</param>
    /// <param name="fallbackNamespace">The fallback namespace.</param>
    /// <returns>
    /// The namespace.
    /// </returns>
    public static string GetNamespace(XDocument xDocument, string fallbackNamespace)
    {
        var defaultNamespace = string.Empty;
        if (xDocument.Root != null)
        {
            foreach (
                var propertyGroupElement in
                xDocument.Root.Elements().Where(x => x.Name.LocalName.Equals(PropertyGroupText)))
            {
                var namespaceElement =
                    propertyGroupElement.Elements().FirstOrDefault(x => x.Name.LocalName.Equals(RootNamespaceText));
                if (namespaceElement != null)
                {
                    return namespaceElement.Value;
                }

                if (string.IsNullOrEmpty(defaultNamespace))
                {
                    namespaceElement =
                        propertyGroupElement.Elements()
                            .FirstOrDefault(x => x.Name.LocalName.Equals(DefaultNamespaceText));
                    if (namespaceElement != null)
                    {
                        return namespaceElement.Value;
                    }
                }
            }
        }

        if (string.IsNullOrEmpty(fallbackNamespace))
        {
            throw new NotSupportedException("A namespace is required");
        }

        return fallbackNamespace;
    }

    /// <summary>
    /// Gets the name of the assembly.
    /// </summary>
    /// <param name="xDocument">The x document.</param>
    /// <param name="fallbackName">The default name.</param>
    /// <returns>The assembly name.</returns>
    public static string GetAssemblyName(XDocument xDocument, string fallbackName)
    {
        if (xDocument.Root != null)
        {
            foreach (
                var propertyGroupElement in
                xDocument.Root.Elements().Where(x => x.Name.LocalName.Equals(PropertyGroupText)))
            {
                var assemblyNameElement =
                    propertyGroupElement.Elements().FirstOrDefault(x => x.Name.LocalName.Equals(AssemblyNameText));
                if (assemblyNameElement != null)
                {
                    return assemblyNameElement.Value;
                }
            }
        }

        return fallbackName;
    }
}