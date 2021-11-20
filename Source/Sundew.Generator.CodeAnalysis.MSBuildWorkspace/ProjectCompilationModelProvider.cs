// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProjectCompilationModelProvider.cs" company="Hukano">
// Copyright (c) Hukano. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sundew.Generator.CodeAnalysis.MSBuildWorkspace;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Sundew.Base.Collections;
using Sundew.Generator.Code;
using Sundew.Generator.Input;

/// <summary>
/// Model provider for providing access to MSBuild project <see cref="Compilation"/>s.
/// </summary>
/// <seealso cref="IModelProvider{ICompilationsSetup, IModelSetup, Compilation}" />
public class ProjectCompilationModelProvider : IModelProvider<ICompilationsSetup, IModelSetup, Compilation?>
{
    /// <summary>
    /// Gets the models.
    /// </summary>
    /// <param name="setup">The setup.</param>
    /// <param name="modelSetup">The model setup.</param>
    /// <returns>
    /// The models.
    /// </returns>
    public async Task<IReadOnlyList<IModelInfo<Compilation?>>> GetModelsAsync(ICompilationsSetup setup, IModelSetup? modelSetup)
    {
        var msBuildWorkspace = await MSBuildWorkspaceFactory.CreateAsync().ConfigureAwait(false);
        if (setup.CompilationPaths == null)
        {
            return Array.Empty<IModelInfo<Compilation?>>();
        }

        return await setup.CompilationPaths.SelectAsync(async filePath =>
        {
            var project = await msBuildWorkspace.OpenProjectAsync(filePath).ConfigureAwait(false);
            return new ModelInfo<Compilation?>(await project.GetCompilationAsync().ConfigureAwait(false), filePath);
        }).ConfigureAwait(false);
    }
}