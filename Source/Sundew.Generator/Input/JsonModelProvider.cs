// --------------------------------------------------------------------------------------------------------------------
// <copyright file="JsonModelProvider.cs" company="Hukano">
// Copyright (c) Hukano. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sundew.Generator.Input;

using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Sundew.Base.Collections;
using Sundew.Generator.Core;
using Sundew.IO;

/// <summary>
/// An implementation of <see cref="IModelProvider{ISetup, IFolderModelSetup, TModel}"/>, which supports json.
/// </summary>
/// <typeparam name="TModel">The type of the model definition.</typeparam>
public class JsonModelProvider<TModel> : IModelProvider<ISetup, IModelSetup, TModel>
    where TModel : class
{
    private const string FilesSearchPattern = "*.json";
    private const string Folder = ".";

    /// <summary>
    /// Gets the models.
    /// </summary>
    /// <param name="setup">The setup.</param>
    /// <param name="modelSetup">The file model setup.</param>
    /// <returns>
    /// The models.
    /// </returns>
    public async Task<IReadOnlyList<IModelInfo<TModel>>> GetModelsAsync(ISetup setup, IModelSetup? modelSetup)
    {
        var folder = Folder;
        var filesSearchPattern = FilesSearchPattern;
        if (modelSetup is IFolderModelSetup folderModelSetup)
        {
            folder = folderModelSetup.Folder;
            filesSearchPattern = folderModelSetup.FilesSearchPattern;
        }

        var files = await Directory.EnumerateFilesAsync(folder, filesSearchPattern).ConfigureAwait(false);
        return await files.SelectAsync(
            async filePath =>
                new ModelInfo<TModel>(
                    JsonConvert.DeserializeObject<TModel>(await File.ReadAllTextAsync(filePath).ConfigureAwait(false))!, filePath)).ConfigureAwait(false);
    }
}