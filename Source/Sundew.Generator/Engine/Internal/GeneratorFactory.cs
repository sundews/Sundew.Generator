// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GeneratorFactory.cs" company="Hukano">
// Copyright (c) Hukano. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sundew.Generator.Engine.Internal
{
    using System;
    using Sundew.Generator.Core;
    using Sundew.Generator.Reflection;

    internal static class GeneratorFactory
    {
        public static IGenerator<ISetup, IGeneratorSetup, ITarget, object, IRun, object> CreateGenerator(TypeOrObject<IGenerator> generatorObjectOrType, SetupInfo setupInfo, int generatorSetupIndex, IGenerator previousGenerator)
        {
            if (generatorObjectOrType == null)
            {
                if (previousGenerator is IHaveGenerator haveGenerator)
                {
                    previousGenerator = haveGenerator.Generator;
                }

                if (previousGenerator != null)
                {
                    generatorObjectOrType = new TypeOrObject<IGenerator>((IGenerator)Activator.CreateInstance(previousGenerator.GetType()));
                }
                else
                {
                    throw new InitializationException(
                        $"Could not create generator because {GetPropertyPath(generatorSetupIndex)} was not defined and no previous generator was found.",
                        setupInfo.Origin,
                        GetPropertyPath(generatorSetupIndex));
                }
            }

            var generator = generatorObjectOrType.Object ?? (IGenerator)Activator.CreateInstance(generatorObjectOrType.Type);
            if (generator is IGenerator<ISetup, IGeneratorSetup, ITarget, object, IRun, object> genericGenerator)
            {
                return genericGenerator;
            }

            var generatorType = generator.GetType();
            var generatorInterfaceType = generatorType.GetGenericInterface(typeof(IGenerator<,,,,,>));
            if (generatorInterfaceType != null)
            {
                return (IGenerator<ISetup, IGeneratorSetup, ITarget, object, IRun, object>)Activator.CreateInstance(typeof(GeneratorAdapter<,,,,,>).MakeGenericType(generatorInterfaceType.GenericTypeArguments), generator);
            }

            throw new InitializationException(
                $"Could not create generator, because the {generatorType.Name} does not implement {typeof(IGenerator<,,,>).Name} or {typeof(IGenerator<,,,,>).Name} or {typeof(IGenerator<,,,,,>).Name}, please check the setup.",
                setupInfo.Origin,
                GetPropertyPath(generatorSetupIndex));
        }

        private static string GetPropertyPath(int generatorSetupIndex)
        {
            return $"GeneratorSetup[{generatorSetupIndex}].Generator";
        }
    }
}