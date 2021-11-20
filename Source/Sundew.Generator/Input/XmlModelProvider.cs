// --------------------------------------------------------------------------------------------------------------------
// <copyright file="XmlModelProvider.cs" company="Hukano">
// Copyright (c) Hukano. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sundew.Generator.Input;

using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Sundew.Generator.Core;

/// <summary>
/// Model provider for Xml files.
/// </summary>
/// <typeparam name="TModel">The type of the model.</typeparam>
public class XmlModelProvider<TModel> : IModelProvider<ISetup, IModelSetup, TModel>
    where TModel : class
{
    private const string FileSearchPattern = "*.xml";
    private const string Folder = ".";

    private readonly XmlSerializer xmlSerializer = new(typeof(TModel));

    /// <summary>
    /// Gets the models.
    /// </summary>
    /// <param name="setup">The setup.</param>
    /// <param name="modelSetup">The model setup.</param>
    /// <returns>
    /// The models.
    /// </returns>
    public async Task<IReadOnlyList<IModelInfo<TModel>>> GetModelsAsync(ISetup setup, IModelSetup? modelSetup)
    {
        var folder = Folder;
        var filesSearchPattern = FileSearchPattern;
        if (modelSetup is IFolderModelSetup folderModelSetup)
        {
            folder = folderModelSetup.Folder;
            filesSearchPattern = folderModelSetup.FilesSearchPattern;
        }

        var files = await Sundew.IO.Directory.EnumerateFilesAsync(folder, filesSearchPattern).ConfigureAwait(false);
        return files.Select(
            filePath =>
                new ModelInfo<TModel>(
                    (TModel)this.xmlSerializer.Deserialize(File.OpenRead(filePath)), filePath)).ToList();
    }
}