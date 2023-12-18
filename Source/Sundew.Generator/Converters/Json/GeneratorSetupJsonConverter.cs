// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GeneratorSetupJsonConverter.cs" company="Sundews">
// Copyright (c) Sundews. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sundew.Generator.Converters.Json;

using System;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Sundew.Generator.Core;
using Sundew.Generator.Reflection;

internal class GeneratorSetupJsonConverter : JsonConverter
{
    private const string GeneratorPropertyName = "Generator";
    private bool isActive = false;
    private IGeneratorSetup? lastGeneratorSetup;

    public override bool CanConvert(Type objectType)
    {
        if (this.isActive)
        {
            return false;
        }

        return typeof(IGeneratorSetup).IsAssignableFrom(objectType);
    }

    public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
    {
        JsonHelper.WriteWithType(writer, value, serializer, GeneratorPropertyName);
    }

    public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
    {
        JObject item = JObject.Load(reader);
        var generatorTypeObject = item[GeneratorPropertyName];
        if (generatorTypeObject != null)
        {
            var generatorType = TypeAssemblyLoader.GetType(generatorTypeObject.Value<string>()!);
            var interfaceType = generatorType?.GetGenericInterface(typeof(IGenerator<,,,,,>));
            if (interfaceType != null)
            {
                var generatorSetupType = typeof(GeneratorSetup);
                if (interfaceType.GenericTypeArguments.Length >= 3 && typeof(IGeneratorSetup).GetTypeInfo().IsAssignableFrom(interfaceType.GenericTypeArguments[1].GetTypeInfo()))
                {
                    generatorSetupType = interfaceType.GenericTypeArguments[1];
                }

                if (generatorSetupType?.IsAbstract != false)
                {
                    generatorSetupType = DefaultImplementation.GetDefaultImplementationType(generatorSetupType);
                }

                if (generatorSetupType?.IsAbstract != false)
                {
                    generatorSetupType = objectType;
                }

                this.isActive = true;
                this.lastGeneratorSetup = (IGeneratorSetup?)item.ToObject(generatorSetupType, serializer);
                this.isActive = false;
                return this.lastGeneratorSetup;
            }

            throw new JsonReaderException($"Error: The generator type: {generatorType} is invalid or is a type that does not implement {typeof(IGenerator<,,,>)}, {typeof(IGenerator<,,,,>)} or {typeof(IGenerator<,,,,,>)}.");
        }

        if (this.lastGeneratorSetup != null)
        {
            this.isActive = true;
            var newObject = item.ToObject(this.lastGeneratorSetup.GetType(), serializer);
            this.isActive = false;
            return newObject;
        }

        throw new JsonReaderException("Error: No generator setup was specified and no previous generator setup was found.");
    }
}