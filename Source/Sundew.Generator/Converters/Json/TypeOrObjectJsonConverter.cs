// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TypeOrObjectJsonConverter.cs" company="Hukano">
// Copyright (c) Hukano. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sundew.Generator.Converters.Json
{
    using System;
    using Newtonsoft.Json;
    using Sundew.Generator.Core;
    using Sundew.Generator.Internal;
    using Sundew.Generator.Reflection;

    /// <summary>
    /// Json converter for <see cref="Type"/>.
    /// </summary>
    /// <seealso cref="Newtonsoft.Json.JsonConverter" />
    public class TypeOrObjectJsonConverter : JsonConverter
    {
        /// <summary>
        /// Determines whether this instance can convert the specified object type.
        /// </summary>
        /// <param name="objectType">Type of the object.</param>
        /// <returns>
        /// <c>true</c> if this instance can convert the specified object type; otherwise, <c>false</c>.
        /// </returns>
        public override bool CanConvert(Type objectType)
        {
            return objectType.IsGenericType && typeof(TypeOrObject<>) == objectType.GetGenericTypeDefinition();
        }

        /// <summary>
        /// Writes the JSON representation of the object.
        /// </summary>
        /// <param name="writer">The <see cref="T:Newtonsoft.Json.JsonWriter" /> to write to.</param>
        /// <param name="value">The value.</param>
        /// <param name="serializer">The calling serializer.</param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value is IHaveType haveType)
            {
                writer.WriteValue(haveType.Type.AssemblyQualifiedName);
                return;
            }

            writer.WriteNull();
        }

        /// <summary>
        /// Reads the JSON representation of the object.
        /// </summary>
        /// <param name="reader">The <see cref="T:Newtonsoft.Json.JsonReader" /> to read from.</param>
        /// <param name="objectType">Type of the object.</param>
        /// <param name="existingValue">The existing value of object being read.</param>
        /// <param name="serializer">The calling serializer.</param>
        /// <returns>
        /// The object value.
        /// </returns>
        /// <exception cref="JsonReaderException">Could not load the type.</exception>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var typeString = reader.Value?.ToString();
            if (!string.IsNullOrEmpty(typeString))
            {
                var type = TypeAssemblyLoader.GetType(typeString);
                if (type == null)
                {
                    throw new JsonReaderException($"Could not load the type {typeString}");
                }

                return Activator.CreateInstance(objectType, Activator.CreateInstance(type));
            }

            return null;
        }
    }
}
