// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ModelSetupJsonConverter.cs" company="Hukano">
// Copyright (c) Hukano. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sundew.Generator.Converters.Json
{
    using System;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using Sundew.Generator.Input;
    using Sundew.Generator.Output;

    internal class ModelSetupJsonConverter : JsonConverter
    {
        private const string ProviderPropertyName = "Provider";

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(IWriterSetup);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            JsonHelper.WriteWithType(writer, value, serializer, ProviderPropertyName);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject item = JObject.Load(reader);
            var modelSetupType = JsonHelper.GetType(item);
            if (modelSetupType == null)
            {
                var modelProviderToken = item[ProviderPropertyName];
                if (modelProviderToken != null)
                {
                    modelSetupType = JsonHelper.GetSetupTypeFromInterface(modelProviderToken, typeof(IModelProvider<,,>), 1);
                    if (modelSetupType != null)
                    {
                        throw new JsonReaderException(
                            $"Error: The model provider type: {modelProviderToken} is invalid or is a type that does not implement {typeof(IModelProvider<,,>)}.");
                    }
                }
            }

            if (modelSetupType != null)
            {
                return (IWriterSetup)item.ToObject(modelSetupType, serializer);
            }

            throw new JsonReaderException("Error: No model provider type was specified.");
        }
    }
}