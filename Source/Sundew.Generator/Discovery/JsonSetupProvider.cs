// --------------------------------------------------------------------------------------------------------------------
// <copyright file="JsonSetupProvider.cs" company="Hukano">
// Copyright (c) Hukano. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sundew.Generator.Discovery
{
    using System.Threading.Tasks;
    using Newtonsoft.Json;
    using Sundew.Generator.Converters.Json;
    using Sundew.Generator.Core;
    using Sundew.IO;

    /// <summary>
    /// Json setup provider.
    /// </summary>
    /// <seealso cref="ISetupProvider" />
    public class JsonSetupProvider : ISetupProvider
    {
        /// <summary>
        /// Gets the setup.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns>
        /// An <see cref="ISetup" />.
        /// </returns>
        public async Task<ISetup> GetSetupAsync(string path)
        {
            var text = await File.ReadAllTextAsync(path);
            return JsonConvert.DeserializeObject<ISetup>(
                text,
                new SetupJsonConverter(),
                new WriterSetupJsonConverter(),
                new GeneratorSetupJsonConverter(),
                new TypeOrObjectJsonConverter());
        }
    }
}