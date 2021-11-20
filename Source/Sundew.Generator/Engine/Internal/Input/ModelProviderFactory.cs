// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ModelProviderFactory.cs" company="Hukano">
// Copyright (c) Hukano. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sundew.Generator.Engine.Internal.Input;

using System;
using System.Linq;
using System.Reflection;
using Sundew.Generator.Core;
using Sundew.Generator.Input;
using Sundew.Generator.Reflection;

internal static class ModelProviderFactory
{
    private static readonly Type ModelProviderInterfaceType = typeof(IModelProvider<,,>);

    public static IModelProvider<ISetup, IModelSetup, object> CreateModelProvider(SetupInfo setupInfo)
    {
        var setup = setupInfo.Setup;
        var modelSetup = setup.ModelSetup;
        var modelType = GetModelType(modelSetup?.ModelType, setupInfo, modelSetup?.Provider?.Type);
        if (modelSetup == null)
        {
            return CreateWrappedModelProvider(setup, null, modelType, (IModelProvider)Activator.CreateInstance(typeof(EmptyModelProvider<>).MakeGenericType(modelType ?? typeof(object))));
        }

        if (modelType == null)
        {
            throw new InitializationException("The model type could not be determined, please check the setup.", setupInfo.Origin, null);
        }

        var modelProviderTypeOrObject = modelSetup.Provider;
        var modelProviderType = modelProviderTypeOrObject == null ? typeof(JsonModelProvider<>) : modelProviderTypeOrObject.Type;
        if (modelProviderTypeOrObject?.Object != null)
        {
            if (modelProviderTypeOrObject.Object is IModelProvider<ISetup, IModelSetup, object> modelProvider)
            {
                return modelProvider;
            }

            return CreateWrappedModelProvider(setup, modelSetup, modelType, modelProviderTypeOrObject.Object);
        }

        var modelProviderTypeInfo = modelProviderType.GetTypeInfo();
        if (modelProviderTypeInfo.IsGenericTypeDefinition)
        {
            if (modelProviderTypeInfo.GenericTypeParameters.Length == 1)
            {
                return CreateWrappedModelProvider(
                    setup,
                    modelSetup,
                    modelType,
                    (IModelProvider)Activator.CreateInstance(modelProviderType.MakeGenericType(modelType)));
            }

            throw new InitializationException(
                $"The model provider: {modelProviderType.Name} is generic, but could not be constructed, please check the setup.",
                setupInfo.Origin,
                nameof(modelSetup.Provider));
        }

        return CreateWrappedModelProvider(setup, modelSetup, modelType, (IModelProvider)Activator.CreateInstance(modelProviderType));
    }

    private static Type GetModelType(Type? modelType, SetupInfo setupInfo, Type? modelProviderType)
    {
        if (modelType != null)
        {
            return modelType;
        }

        var modelProviderInterfaceModelType = GetModelProviderInterfaceModelType(modelProviderType);
        if (modelProviderInterfaceModelType != null)
        {
            return modelProviderInterfaceModelType;
        }

        var generatorInterfaceModelType = GetGeneratorInterfaceModelType(setupInfo);
        if (generatorInterfaceModelType != null)
        {
            return generatorInterfaceModelType;
        }

        throw new InitializationException(
            "The model type could not be determined, please check the setup.",
            setupInfo.Origin,
            null);
    }

    private static Type? GetModelProviderInterfaceModelType(Type? modelProviderType)
    {
        var modelProviderInterfaceType = modelProviderType?.GetGenericInterface(ModelProviderInterfaceType);
        return DefaultImplementation.GetDefaultImplementationType(modelProviderInterfaceType?.GenericTypeArguments[2]);
    }

    private static Type? GetGeneratorInterfaceModelType(SetupInfo setupInfo)
    {
        var generatorSetup = setupInfo.Setup.GeneratorSetups?.FirstOrDefault();
        if (generatorSetup != null)
        {
            var interfaceType = generatorSetup.Generator?.Type.GetGenericInterface(typeof(IGenerator<,,,,,>));
            return DefaultImplementation.GetDefaultImplementationType(interfaceType?.GenericTypeArguments.ElementAtOrDefault(3));
        }

        return null;
    }

    private static IModelProvider<ISetup, IModelSetup, object> CreateWrappedModelProvider(ISetup setup, IModelSetup? modelSetup, Type modelType, IModelProvider modelProvider)
    {
        return (IModelProvider<ISetup, IModelSetup, object>)Activator.CreateInstance(
            typeof(ModelProviderAdapter<,,>).MakeGenericType(setup.GetType(), modelSetup?.GetType() ?? typeof(IModelSetup), modelType),
            modelProvider);
    }
}