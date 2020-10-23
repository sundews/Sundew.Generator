// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ReflectionExtensions.cs" company="Hukano">
// Copyright (c) Hukano. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sundew.Generator.Reflection
{
    using System;
    using System.Linq;
    using System.Reflection;

    internal static class ReflectionExtensions
    {
        public static Type GetGenericInterface(this Type type, Type interfaceType)
        {
            return type.GetTypeInfo().ImplementedInterfaces.FirstOrDefault(x => x.IsGenericType && x.GetGenericTypeDefinition() == interfaceType);
        }
    }
}