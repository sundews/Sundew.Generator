// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MSBuildWorkspaceFactory.cs" company="Sundews">
// Copyright (c) Sundews. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sundew.Generator.CodeAnalysis.MSBuildWorkspace;

using System.Threading.Tasks;

internal static class MSBuildWorkspaceFactory
{
    private static bool wasWorkspaceCreatedOnce;

    public static Task<Microsoft.CodeAnalysis.MSBuild.MSBuildWorkspace> CreateAsync()
    {
        if (wasWorkspaceCreatedOnce)
        {
            return Task.FromResult(Microsoft.CodeAnalysis.MSBuild.MSBuildWorkspace.Create());
        }

        var createWorkspaceTask = Task.Run(() => Microsoft.CodeAnalysis.MSBuild.MSBuildWorkspace.Create());
        wasWorkspaceCreatedOnce = true;
        return createWorkspaceTask;
    }
}