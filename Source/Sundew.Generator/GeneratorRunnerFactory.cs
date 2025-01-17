// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GeneratorRunnerFactory.cs" company="Sundews">
// Copyright (c) Sundews. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sundew.Generator;

using System.Collections.Generic;
using System.Linq;
using Sundew.Generator.Core;
using Sundew.Generator.Engine;
using Sundew.Generator.Engine.Internal;
using Sundew.Generator.Engine.Internal.Input;
using Sundew.Generator.Engine.Internal.Output;
using Sundew.Generator.Output;

/// <summary>
/// Implementation of <see cref="IGeneratorRunnerFactory"/> for creating a <see cref="GeneratorRunner"/> based in the specified <see cref="IEnumerable{ISetup}"/>.
/// </summary>
/// <seealso cref="IGeneratorRunnerFactory" />
public class GeneratorRunnerFactory : IGeneratorRunnerFactory
{
    private static readonly IWriterSetup[] EmptyWriterSetups = new IWriterSetup[0];

    /// <summary>
    /// Creates an <see cref="IGeneratorRunner" />.
    /// </summary>
    /// <param name="setupInfos">The setupInfos.</param>
    /// <returns>
    /// An <see cref="IGeneratorRunner" />.
    /// </returns>
    public IGeneratorRunner Create(IEnumerable<SetupInfo> setupInfos)
    {
        List<WriterInfo> writerInfos = new();
        foreach (var setupInfo in setupInfos)
        {
            var modelCache = new ModelCache(ModelProviderFactory.CreateModelProvider(setupInfo), setupInfo.Setup);
            AddGlobalWriterSetups(setupInfo, modelCache, writerInfos);
            AddGeneratorSpecificWriterSetups(setupInfo, modelCache, writerInfos);
        }

        return new GeneratorRunner(writerInfos);
    }

    private static void AddGeneratorSpecificWriterSetups(SetupInfo setupInfo, ModelCache modelCache, List<WriterInfo> writerInfos)
    {
        var generatorSetups = setupInfo.Setup.GeneratorSetups;
        IGenerator? previousGenerator = null;
        for (int generatorSetupIndex = 0; generatorSetupIndex < generatorSetups?.Count; generatorSetupIndex++)
        {
            var generatorInfo = CreateGeneratorInfo(setupInfo, modelCache, generatorSetups[generatorSetupIndex], generatorSetupIndex, previousGenerator);
            previousGenerator = generatorInfo.Generator;
            IWriter? previousWriter = null;
            var generatorWriterSetups = GetWriterSetups(generatorInfo.GeneratorSetup.WriterSetups);
            for (int writerSetupIndex = 0; writerSetupIndex < generatorWriterSetups.Count; writerSetupIndex++)
            {
                var writerInfo = CreateWriterInfo(
                    generatorWriterSetups[writerSetupIndex],
                    [generatorInfo],
                    setupInfo,
                    writerSetupIndex,
                    generatorSetupIndex,
                    previousWriter);
                writerInfos.Add(writerInfo);
                previousWriter = writerInfo.Writer;
            }
        }
    }

    private static void AddGlobalWriterSetups(SetupInfo setupInfo, ModelCache modelCache, List<WriterInfo> writerInfos)
    {
        var setup = setupInfo.Setup;
        var globalWriterSetups = GetWriterSetups(setup.WriterSetups);
        IWriter? previousWriter = null;
        for (int writerSetupIndex = 0; writerSetupIndex < globalWriterSetups.Count; writerSetupIndex++)
        {
            var globalWriterSetup = globalWriterSetups[writerSetupIndex];
            var sharingGeneratorInfos = new List<GeneratorInfo>();
            IGenerator? previousGenerator = null;
            for (int generatorSetupIndex = 0;
                 generatorSetupIndex < setup.GeneratorSetups?.Count;
                 generatorSetupIndex++)
            {
                var generatorSetup = setup.GeneratorSetups[generatorSetupIndex];
                if (generatorSetup.SkipGlobalWriterSetups)
                {
                    continue;
                }

                var generatorInfo = CreateGeneratorInfo(setupInfo, modelCache, generatorSetup, generatorSetupIndex, previousGenerator);
                previousGenerator = generatorInfo.Generator;
                if (generatorSetup.ShareGlobalWriters)
                {
                    sharingGeneratorInfos.Add(generatorInfo);
                }
                else
                {
                    var writerInfo = CreateWriterInfo(globalWriterSetup, [generatorInfo], setupInfo, writerSetupIndex, null, previousWriter);
                    writerInfos.Add(writerInfo);
                    previousWriter = writerInfo.Writer;
                }
            }

            if (sharingGeneratorInfos.Any())
            {
                var writerInfo = CreateWriterInfo(globalWriterSetup, sharingGeneratorInfos, setupInfo, writerSetupIndex, null, previousWriter);
                writerInfos.Add(writerInfo);
                previousWriter = writerInfo.Writer;
            }
        }
    }

    private static GeneratorInfo CreateGeneratorInfo(
        SetupInfo setupInfo,
        ModelCache modelCache,
        IGeneratorSetup generatorSetup,
        int generatorSetupIndex,
        IGenerator? previousGenerator)
    {
        var generator = GeneratorFactory.CreateGenerator(generatorSetup.Generator, setupInfo, generatorSetupIndex, previousGenerator);
        return new GeneratorInfo(setupInfo.Setup, modelCache, generatorSetup, generator);
    }

    private static WriterInfo CreateWriterInfo(
        IWriterSetup writerSetup,
        IEnumerable<GeneratorInfo> generatorInfos,
        SetupInfo setupInfo,
        int writerSetupIndex,
        int? generatorSetupIndex,
        IWriter? previousWriter)
    {
        return new(
            writerSetup,
            WriterFactory.CreateWriter(writerSetup.Writer, setupInfo, writerSetupIndex, generatorSetupIndex, previousWriter),
            generatorInfos);
    }

    private static IReadOnlyList<IWriterSetup> GetWriterSetups(IReadOnlyList<IWriterSetup>? writerSetups)
    {
        return writerSetups ?? EmptyWriterSetups;
    }
}