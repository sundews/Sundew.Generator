// --------------------------------------------------------------------------------------------------------------------
// <copyright file="JsonHelper.cs" company="Hukano">
// Copyright (c) Hukano. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sundew.Generator.Converters.Json
{
    using System;
    using System.Reflection;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using Sundew.Generator.Core;
    using Sundew.Generator.Reflection;

    internal static class JsonHelper
    {
        public static Type? GetType(JObject jObject)
        {
            var typeToken = jObject["Type"];
            var typeName = typeToken?.Value<string>();
            if (typeName != null)
            {
                return TypeAssemblyLoader.GetType(typeName);
            }

            return null;
        }

        public static void WriteWithType(JsonWriter writer, object? value, JsonSerializer serializer, string typePropertyName)
        {
            if (value == null)
            {
                return;
            }

            var item = JObject.FromObject(value, serializer);
            var objectType = value.GetType();
            item[typePropertyName] = $"{objectType.FullName}, {objectType.GetTypeInfo().Assembly.GetName().Name}";
            item.WriteTo(writer);
        }

        public static Type? GetSetupTypeFromInterface(JToken token, Type genericInterfaceType, int setupTypeIndex)
        {
            var setupType = TypeAssemblyLoader.GetType(token.Value<string>());
            var interfaceType = setupType?.GetGenericInterface(genericInterfaceType);
            if (interfaceType != null)
            {
                var defaultImplementationType = DefaultImplementation.GetDefaultImplementationType(interfaceType.GenericTypeArguments[setupTypeIndex]);
                if (defaultImplementationType != null)
                {
                    return defaultImplementationType;
                }

                throw new JsonReaderException($"Error: No instantiable setup type was found for {setupType}.");
            }

            return null;
        }
    }
}