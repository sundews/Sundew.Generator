// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DefaultImplementation.cs" company="Sundews">
// Copyright (c) Sundews. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sundew.Generator.Core;

using System;
using System.Reflection;

/// <summary>
/// Allows to specify a default implementation on an interface.
/// </summary>
/// <seealso cref="System.Attribute" />
[AttributeUsage(AttributeTargets.Interface)]

public class DefaultImplementation : Attribute
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DefaultImplementation"/> class.
    /// </summary>
    /// <param name="defaultImplementationType">Default type of the implementation.</param>
    public DefaultImplementation(Type defaultImplementationType)
    {
        this.DefaultImplementationType = defaultImplementationType;
    }

    /// <summary>
    /// Gets the default type of the implementation.
    /// </summary>
    /// <value>
    /// The default type of the implementation.
    /// </value>
    public Type DefaultImplementationType { get; }

    /// <summary>
    /// Gets the specified type.
    /// </summary>
    /// <param name="type">The type.</param>
    /// <returns>A <see cref="DefaultImplementation"/>.</returns>
    public static DefaultImplementation Get(Type type)
    {
        return (DefaultImplementation)type.GetTypeInfo().GetCustomAttribute(typeof(DefaultImplementation));
    }

    /// <summary>
    /// Gets the default type of the implementation.
    /// </summary>
    /// <param name="type">The type.</param>
    /// <returns>The default implementation type.</returns>
    public static Type? GetDefaultImplementationType(Type? type)
    {
        if (type?.IsAbstract == true)
        {
            return Get(type).DefaultImplementationType;
        }

        return type;
    }
}