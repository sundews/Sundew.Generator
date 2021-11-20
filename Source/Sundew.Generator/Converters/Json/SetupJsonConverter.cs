// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SetupJsonConverter.cs" company="Hukano">
// Copyright (c) Hukano. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sundew.Generator.Converters.Json;

using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

internal class SetupJsonConverter : JsonConverter
{
    private const string TypePropertyName = "Type";

    public override bool CanConvert(Type objectType)
    {
        return objectType == typeof(ISetup);
    }

    public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
    {
        JsonHelper.WriteWithType(writer, value, serializer, TypePropertyName);
    }

    public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
    {
        JObject item = JObject.Load(reader);
        var setupType = JsonHelper.GetType(item);
        if (setupType == null)
        {
            throw new JsonReaderException("The root element should declare it's type in Type property.");
        }

        return item.ToObject(setupType, serializer);
    }
}