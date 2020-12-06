// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CodeSetup.cs" company="Hukano">
// Copyright (c) Hukano. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sundew.Generator.Code
{
    using System.Collections.Generic;
    using Sundew.Generator.Core;
    using Sundew.Generator.Input;
    using Sundew.Generator.Output;

    /// <summary>
    /// Implementation of <see cref="ISetup"/> for code generation.
    /// </summary>
    /// <seealso cref="ISetup" />
    public class CodeSetup : Setup<IModelSetup, IFileWriterSetup, ICodeGeneratorSetup>, ICodeSetup
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CodeSetup" /> class.
        /// </summary>
        public CodeSetup()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CodeSetup" /> class.
        /// </summary>
        /// <param name="modelSetup">The model setup.</param>
        /// <param name="writerSetups">The target setups.</param>
        /// <param name="generatorSetups">The generator setups.</param>
        /// <param name="targetNamespace">The target namespace.</param>
        /// <param name="usings">The usings.</param>
        /// <param name="useGlobalUsings">if set to <c>true</c> [use global usings].</param>
        public CodeSetup(
            IModelSetup modelSetup,
            IReadOnlyList<IFileWriterSetup> writerSetups,
            IReadOnlyList<ICodeGeneratorSetup> generatorSetups,
            string targetNamespace,
            IReadOnlyList<string> usings,
            bool useGlobalUsings)
            : base(modelSetup, writerSetups, generatorSetups)
        {
            this.TargetNamespace = targetNamespace;
            this.Usings = usings;
            this.UseGlobalUsings = useGlobalUsings;
        }

        /// <summary>
        /// Gets the target namespace.
        /// </summary>
        /// <value>
        /// The target namespace.
        /// </value>
        public string? TargetNamespace { get; init; }

        /// <summary>
        /// Gets the usings.
        /// </summary>
        /// <value>
        /// The usings.
        /// </value>
        public IReadOnlyList<string>? Usings { get; init; }

        /// <summary>
        /// Gets a value indicating whether [use global usings].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [use global usings]; otherwise, <c>false</c>.
        /// </value>
        public bool UseGlobalUsings { get; init; }
    }
}
