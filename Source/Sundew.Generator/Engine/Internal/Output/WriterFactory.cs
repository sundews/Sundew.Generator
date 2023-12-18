// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WriterFactory.cs" company="Sundews">
// Copyright (c) Sundews. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sundew.Generator.Engine.Internal.Output;

using System;
using Sundew.Generator.Core;
using Sundew.Generator.Output;
using Sundew.Generator.Reflection;

internal static class WriterFactory
{
    public static IWriter<IWriterSetup, ITarget, IRun, object> CreateWriter(
        TypeOrObject<IWriter>? writerTypeOrObject,
        SetupInfo setupInfo,
        int writerSetupIndex,
        int? generatorSetupIndex,
        IWriter? previousWriter)
    {
        if (writerTypeOrObject == null)
        {
            if (previousWriter is IHaveWriter haveWriter)
            {
                previousWriter = haveWriter.Writer;
            }

            if (previousWriter != null)
            {
                writerTypeOrObject = new TypeOrObject<IWriter>((IWriter)Activator.CreateInstance(previousWriter.GetType()));
            }
            else
            {
                throw new InitializationException(
                    $"Could not create writer, because {GetPropertyPath(writerSetupIndex, generatorSetupIndex)} was not defined and no previous writer was found.",
                    setupInfo.Origin,
                    GetPropertyPath(writerSetupIndex, generatorSetupIndex));
            }
        }

        var interfaceType = writerTypeOrObject.Type.GetGenericInterface(typeof(IWriter<,,,>));
        if (interfaceType == null)
        {
            throw new InitializationException(
                $"Could not create writer, because {writerTypeOrObject.Type.Name} does not implement {typeof(IWriter<,,,>)}, please check the setup.",
                setupInfo.Origin,
                GetPropertyPath(writerSetupIndex, generatorSetupIndex));
        }

        var writer = writerTypeOrObject.Object ?? (IWriter)Activator.CreateInstance(writerTypeOrObject.Type);

        if (interfaceType.GenericTypeArguments[0] != typeof(IWriterSetup) || interfaceType.GenericTypeArguments[1] != typeof(ITarget))
        {
            writer = (IWriter)Activator.CreateInstance(typeof(WriterAdapter<,,,>).MakeGenericType(interfaceType.GenericTypeArguments), writer);
        }

        return (IWriter<IWriterSetup, ITarget, IRun, object>)writer;
    }

    private static string GetPropertyPath(int writerSetupIndex, int? generatorSetupIndex)
    {
        return $"{(generatorSetupIndex.HasValue ? $"GeneratorSetups[{generatorSetupIndex.Value}]." : string.Empty)}WriterSetup[{writerSetupIndex}].Writer";
    }
}