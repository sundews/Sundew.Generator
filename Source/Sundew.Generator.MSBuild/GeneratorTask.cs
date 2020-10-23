// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GeneratorTask.cs" company="Hukano">
// Copyright (c) Hukano. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sundew.Generator.MSBuild
{
    using System;
    using System.Collections.Concurrent;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using Microsoft.Build.Framework;
    using Microsoft.Build.Utilities;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Scripting;
    using Microsoft.CodeAnalysis.Emit;
    using Microsoft.CodeAnalysis.Scripting;
    using Sundew.Base.Collections;

    /// <summary>
    /// Generator MSBuild task.
    /// </summary>
    /// <seealso cref="Microsoft.Build.Utilities.Task" />
    public class GeneratorTask : Task
    {
        /// <summary>
        /// Gets or sets the project path.
        /// </summary>
        /// <value>
        /// The project path.
        /// </value>
        [Required]
        public string ProjectPath { get; set; }

        /// <summary>
        /// Gets or sets the intermediate output path.
        /// </summary>
        /// <value>
        /// The intermediate output path.
        /// </value>
        [Required]
        public string IntermediateOutputPath { get; set; }

        /// <summary>
        /// Gets or sets the output path.
        /// </summary>
        /// <value>
        /// The output path.
        /// </value>
        [Required]
        public string OutputPath { get; set; }

        /// <summary>
        /// Gets or sets the generation references.
        /// </summary>
        /// <value>
        /// The generation references.
        /// </value>
        [Required]
        public ITaskItem[] GeneratorReferences { get; set; }

        /// <summary>
        /// Gets or sets the additional generation references.
        /// </summary>
        /// <value>
        /// The additional generation references.
        /// </value>
        [Required]
        public ITaskItem[] AdditionalGeneratorReferences { get; set; }

        /// <summary>
        /// Gets or sets the generation setups.
        /// </summary>
        /// <value>
        /// The generation setups.
        /// </value>
        [Required]
        public ITaskItem[] GenerationSetups { get; set; }

        /// <summary>
        /// Gets or sets the compiled generation setups.
        /// </summary>
        /// <value>
        /// The generation setups.
        /// </value>
        [Required]
        public ITaskItem[] CompileGenerationSetups { get; set; }

        /// <summary>
        /// Gets or sets the generation and compiles.
        /// </summary>
        /// <value>
        /// The generation and compiles.
        /// </value>
        [Required]
        public ITaskItem[] GeneratesAndCompiles { get; set; }

        /// <summary>
        /// Gets or sets the generation.
        /// </summary>
        /// <value>
        /// The generation.
        /// </value>
        [Required]
        public ITaskItem[] Generates { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is compiler build.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is compiler build; otherwise, <c>false</c>.
        /// </value>
        [Required]
        public bool IsCompilerBuild { get; set; }

        /// <summary>Gets or sets a value indicating whether this <see cref="GeneratorTask"/> should be debugged.</summary>
        /// <value>
        ///   <c>true</c> if debug; otherwise, <c>false</c>.</value>
        public bool Debug { get; set; }

        /// <summary>
        /// Gets or sets the generated files.
        /// </summary>
        /// <value>
        /// The generated files.
        /// </value>
        [Output]
        public ITaskItem[] GeneratedFiles { get; set; }

        /// <summary>
        /// Gets or sets the generated compiles.
        /// </summary>
        /// <value>
        /// The generated compiles.
        /// </value>
        [Output]
        public ITaskItem[] GeneratedCompiles { get; set; }

        /// <summary>
        /// Must be implemented by derived class.
        /// </summary>
        /// <returns>
        /// true, if successful.
        /// </returns>
        public override bool Execute()
        {
            if (this.Debug)
            {
                System.Diagnostics.Debugger.Launch();
            }

            var currentDirectory = Directory.GetCurrentDirectory();
            var assemblyName = Path.GetFileNameWithoutExtension(this.ProjectPath) + ".gen";
            var sgDirectoryPath = Path.Combine(this.IntermediateOutputPath, "sg");
            var libraryPath = Path.Combine(sgDirectoryPath, $"{assemblyName}.dll");

            this.CleanUpTemporaryDirectory(sgDirectoryPath);

            EmitResult emitResult = null;
            try
            {
                var referenceItems = this.GeneratorReferences.Concat(this.AdditionalGeneratorReferences).ToList();
                var references = AppDomain.CurrentDomain.GetAssemblies()
                    .Select(x => MetadataReference.CreateFromFile(x.Location))
                    .Concat(referenceItems.Select(reference =>
                        MetadataReference.CreateFromFile(Path.GetFullPath(reference.ItemSpec)))).ToList();
                var generatesSyntaxTrees = this.Generates.Concat(this.GeneratesAndCompiles).Select(ParseCompile);
                var compiledGenerationSetupsSyntaxTrees = this.CompileGenerationSetups.Select(ParseCompile).ToArray();
                var compilation =
                    CSharpCompilation.Create(
                        assemblyName,
                        generatesSyntaxTrees.Concat(compiledGenerationSetupsSyntaxTrees),
                        references,
                        new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));
                emitResult = compilation.Emit(libraryPath);
                var scriptOptions = ScriptOptions.Default
                    .AddReferences(references)
                    .AddReferences(compilation.ToMetadataReference())
                    .AddImports(Path.GetFileNameWithoutExtension(this.ProjectPath));
                var interactiveAssemblyLoader =
                    new Microsoft.CodeAnalysis.Scripting.Hosting.InteractiveAssemblyLoader();
                foreach (var reference in referenceItems)
                {
                    interactiveAssemblyLoader.RegisterDependency(
                        Assembly.Load(AssemblyName.GetAssemblyName(Path.GetFullPath(reference.ItemSpec))));
                }

                interactiveAssemblyLoader.RegisterDependency(new AssemblyIdentity(assemblyName), libraryPath);
                var msBuildLogReporter = new MsBuildLogReporter(this.Log);

                var generationScript = CSharpScript.Create<ConcurrentBag<string>>(
                    $@"
using System;
using System.Collections.Concurrent;
using Sundew.Generator;
using Sundew.Generator.Input;
var result = GeneratorFacade.RunAsync(SetupsFactory, new GeneratorOptions {{ ProgressReporter = ProgressReporter, ModelProviderFactory = new EmptyModelProviderFactory() }});
result.Wait();
result.Result
",
                    scriptOptions,
                    typeof(Globals),
                    interactiveAssemblyLoader);
                var setupFactoryProvider = new SetupsFactoryProvider(Directory.GetCurrentDirectory(), this.GenerationSetups, compiledGenerationSetupsSyntaxTrees, compilation);
                var setupsFactory = setupFactoryProvider.GetSetupsFactory();
                Directory.SetCurrentDirectory(Path.Combine(currentDirectory, this.OutputPath));
                var scriptStateTask = generationScript.RunAsync(new Globals
                { ProgressReporter = msBuildLogReporter, SetupsFactory = setupsFactory });
                scriptStateTask.Wait();
                var taskItems = scriptStateTask.Result.ReturnValue.ToArray(x => new TaskItem(x));
                this.GeneratedFiles = taskItems;
                this.GeneratedCompiles = taskItems.Where(x => Path.GetExtension(x.ItemSpec) == ".cs").ToArray();
            }
            catch (BadImageFormatException e)
            {
                if (emitResult != null)
                {
                    var message = emitResult.Diagnostics.Aggregate(
                        new StringBuilder(),
                        (builder, diagnostic) => builder.AppendLine(diagnostic.ToString()),
                        builder => builder.ToString());
                    if (this.IsCompilerBuild)
                    {
                        this.Log.LogWarning(message);
                        return true;
                    }

                    this.Log.LogError(message);
                    return false;
                }

                return this.LogException(e);
            }
            catch (Exception e)
            {
                return this.LogException(e);
            }
            finally
            {
                Directory.SetCurrentDirectory(currentDirectory);
            }

            return true;
        }

        private static SyntaxTree ParseCompile(ITaskItem compile)
        {
            return CSharpSyntaxTree.ParseText(
                File.ReadAllText(compile.ItemSpec),
                new CSharpParseOptions(LanguageVersion.Default, DocumentationMode.None),
                compile.ItemSpec,
                Encoding.Unicode);
        }

        private void CleanUpTemporaryDirectory(string sgDirectoryPath)
        {
            try
            {
                if (Directory.Exists(sgDirectoryPath))
                {
                    Directory.Delete(sgDirectoryPath, true);
                }

                Directory.CreateDirectory(sgDirectoryPath);
            }
            catch (Exception e)
            {
                this.Log.LogWarningFromException(e);
            }
        }

        private bool LogException(Exception e)
        {
            if (this.IsCompilerBuild)
            {
                this.Log.LogWarning(e.ToString());
                return true;
            }

            this.Log.LogErrorFromException(e, true);
            return false;
        }
    }
}
