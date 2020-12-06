// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SetupsFactoryProvider.cs" company="Hukano">
// Copyright (c) Hukano. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sundew.Generator.MSBuild
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Microsoft.Build.Framework;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;
    using Sundew.Generator.Discovery;

    /// <summary>
    /// Creates an <see cref="ISetupsFactory"/> from <see cref="ITaskItem"/>s and syntax trees.
    /// </summary>
    public class SetupsFactoryProvider
    {
        private readonly string currentDirectory;
        private readonly ITaskItem[]? generationSetupTaskItems;
        private readonly SetupsFactoryTypesVisitor typeNameAndAssemblyVisitor;

        /// <summary>
        /// Initializes a new instance of the <see cref="SetupsFactoryProvider"/> class.
        /// </summary>
        public SetupsFactoryProvider(string currentDirectory, ITaskItem[]? generationSetupTaskItems, IEnumerable<SyntaxTree> compiledGenerationSetupsSyntaxTrees, Compilation compilation)
        {
            this.currentDirectory = currentDirectory;
            this.generationSetupTaskItems = generationSetupTaskItems;
            this.typeNameAndAssemblyVisitor = new SetupsFactoryTypesVisitor(compiledGenerationSetupsSyntaxTrees, compilation);
        }

        /// <summary>
        /// Gets the setups factory.
        /// </summary>
        /// <returns>A setups factory.</returns>
        public ISetupsFactory? GetSetupsFactory()
        {
            ISetupsFactory? setupsFactory = null;
            if (this.generationSetupTaskItems?.Any() == true)
            {
                setupsFactory = new JsonSetupsFactory(this.generationSetupTaskItems.Select(x => Path.Combine(this.currentDirectory, x.ItemSpec)));
            }

            var setupsFactoryTypes = this.typeNameAndAssemblyVisitor.GetTypes();
            if (setupsFactoryTypes.Any())
            {
                setupsFactory = setupsFactory != null
                    ? (ISetupsFactory)new CompositeSetupsFactory(setupsFactory, new SetupsFactoryTypeSetupsFactory(setupsFactoryTypes))
                    : new SetupsFactoryTypeSetupsFactory(setupsFactoryTypes);
            }

            return setupsFactory;
        }

        private class SetupsFactoryTypesVisitor : CSharpSyntaxWalker
        {
            private readonly IEnumerable<SyntaxTree> syntaxTrees;
            private readonly Compilation compilation;
            private List<string>? types;
            private SemanticModel? semanticModel;

            public SetupsFactoryTypesVisitor(IEnumerable<SyntaxTree> syntaxTrees, Compilation compilation)
            {
                this.syntaxTrees = syntaxTrees;
                this.compilation = compilation;
            }

            public IReadOnlyList<string> GetTypes()
            {
                this.types = new List<string>();
                foreach (var syntaxTree in this.syntaxTrees)
                {
                    this.semanticModel = this.compilation.GetSemanticModel(syntaxTree);
                    this.Visit(syntaxTree.GetRoot());
                }

                var actualTypes = this.types;
                this.types = null;
                return actualTypes;
            }

            public override void VisitClassDeclaration(ClassDeclarationSyntax node)
            {
                base.VisitClassDeclaration(node);
                var declaredSymbol = this.semanticModel.GetDeclaredSymbol(node);
                if (declaredSymbol != null)
                {
                    this.types?.Add($"{declaredSymbol.ToDisplayString()}, {declaredSymbol.ContainingAssembly}");
                }
            }
        }
    }
}