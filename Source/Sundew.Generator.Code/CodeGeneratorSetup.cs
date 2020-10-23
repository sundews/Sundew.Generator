// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CodeGeneratorSetup.cs" company="Hukano">
// Copyright (c) Hukano. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sundew.Generator.Code
{
    using System.Collections.Generic;
    using Sundew.Generator.Core;
    using Sundew.Generator.Output;

    /// <summary>
    /// Implementation of <see cref="IGeneratorSetup"/> for code generation.
    /// </summary>
    /// <seealso cref="IGeneratorSetup" />
    public class CodeGeneratorSetup : GeneratorSetup<IFileWriterSetup>, ICodeGeneratorSetup
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CodeGeneratorSetup"/> class.
        /// </summary>
        public CodeGeneratorSetup()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CodeGeneratorSetup" /> class.
        /// </summary>
        /// <param name="generator">The generator.</param>
        /// <param name="writerSetups">The writer setups.</param>
        /// <param name="skipGlobalWriterSetup">if set to <c>true</c> [skip global writer setup].</param>
        /// <param name="shareGlobalWriters">if set to <c>true</c> [share global writers].</param>
        /// <param name="targetNamespace">The target namespace.</param>
        /// <param name="usings">The usings.</param>
        /// <param name="useGlobalUsings">if set to <c>true</c> [use global usings].</param>
        public CodeGeneratorSetup(TypeOrObject<IGenerator> generator, IReadOnlyList<IFileWriterSetup> writerSetups, bool skipGlobalWriterSetup, bool shareGlobalWriters, string targetNamespace, IReadOnlyList<string> usings, bool useGlobalUsings)
            : base(generator, writerSetups, skipGlobalWriterSetup, shareGlobalWriters)
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
        public string TargetNamespace { get; init; }

        /// <summary>
        /// Gets the usings.
        /// </summary>
        /// <value>
        /// The usings.
        /// </value>
        public IReadOnlyList<string> Usings { get; init; }

        /// <summary>
        /// Gets a value indicating whether [use global usings].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [use global usings]; otherwise, <c>false</c>.
        /// </value>
        public bool UseGlobalUsings { get; init; }
    }
}
