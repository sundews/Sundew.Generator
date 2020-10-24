// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WriterSetupJsonConverter.cs" company="Hukano">
// Copyright (c) Hukano. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sundew.Generator.Converters.Json
{
    using System;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using Sundew.Generator.Output;

    internal class WriterSetupJsonConverter : JsonConverter
    {
        private const string WriterPropertyName = "Writer";

        private IWriterSetup lastWriterSetup;

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(IWriterSetup);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            JsonHelper.WriteWithType(writer, value, serializer, WriterPropertyName);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject item = JObject.Load(reader);
            var writerSetupType = JsonHelper.GetType(item);
            if (writerSetupType == null)
            {
                var writerToken = item[WriterPropertyName];
                if (writerToken != null)
                {
                    writerSetupType = JsonHelper.GetSetupTypeFromInterface(writerToken, typeof(IWriter<,,,>), 0);
                    if (writerSetupType == null)
                    {
                        throw new JsonReaderException(
                            $"Error: The writer type: {writerToken} is invalid or is a type that does not implement {typeof(IWriter<,,,>)}.");
                    }
                }
            }

            if (writerSetupType != null)
            {
                this.lastWriterSetup = (IWriterSetup)item.ToObject(writerSetupType, serializer);
                return this.lastWriterSetup;
            }

            if (this.lastWriterSetup != null)
            {
                return item.ToObject(this.lastWriterSetup.GetType(), serializer);
            }

            throw new JsonReaderException("Error: No writer type was specified and no previous target setup was found.");
        }
    }
}