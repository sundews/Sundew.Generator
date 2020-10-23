# Sundew.Generator

Sundew.Generator is code generator aiming to provide an alternative or replacement for T4 templates.

Generators are simple C# console applications that execute themselves after being built.

The concept separates generators from models and output, meaning it is possible to have several generators operate on the same input (models) and have the result placed in one or more outputs (writers).

## **Getting started**

1. Create a new project.
1. Add the [Sundew.Generator](https://www.nuget.org/packages/Sundew.Generator/) or [Sundew.Generator.Code](https://www.nuget.org/packages/Sundew.Generator.Code/) NuGet package.
1. [Implement a generator](#implement_a_generator)
1. [Setup](#setup)
1. Implement the main method, something like this:

```csharp
public static Task Main()
{
    return GeneratorFacade.RunAsync(new MySetupsFactory());
}
```

1. Build (and it will run)

## <a id="implement_a_generator"></a>**Implementing a generator**

A generator can be added by implementing the generic ```Sundew.Generator.IGenerator<in TSetup, in TGeneratorSetup, in TTarget, in TModel, TRun, out TOutput>``` interface.
The interface take quite a few generic parameters, which the developer can decide and requires the implementation of two methods.

* **Prepare** must return a number IRuns, which will cause the Generate method to be called for each run. Typically prepare will return a single run.

* **Generate** can output anything as long as the writer accepts the output, but typically this would be text, such as TextOutput.

Sundew.Generator does not support any form of preprocessed text templates like T4. For readability string interpolation combined with methods may be used.

```csharp
public interface IGenerator<in TSetup, in TGeneratorSetup, in TTarget, in TModel, TRun, out TOutput> : IGenerator
    where TTarget : ITarget
    where TRun : IRun
{
    IReadOnlyList<TRun> Prepare(TSetup setup, TGeneratorSetup generatorSetup, TTarget target, TModel model, string modelPath);

    TOutput Generate(TSetup setup, TGeneratorSetup generatorSetup, TTarget target, TModel model, TRun run, long index);
}
```

### **Example**

## <a id="setup"></a>**Setup**

The setup is used to configure models, generators and writers.
```The Sundew.Generator.Setup``` allows setting a ModelSetup, one of more GeneratorSetups and one or more WriterSetups.

### **ModelSetup**

A model setup allows to specify a ModelProvider and the ModelType with the ```Sundew.Generator.Input.ModelSetup``` class. If neither a ModelProvider or ModelType is specified an ```Sundew.Generator.ModelProviders.EmptyModelProvider<object>``` will be used.
If only a model type is specified ```Sundew.Generator.ModelProviders.JsonModelProvider<TModel>``` will be used.

#### **Model Providers**

Sundew.Generator ships with three model providers out the box:

```csharp
Sundew.Generator.ModelProviders.XmlModelProvider<TModel>
Sundew.Generator.ModelProviders.JsonModelProvider<TModel>
Sundew.Generator.ModelProviders.EmptyModelProvider<TModel>
```

```XmlModelProvider``` and ```JsonModelProvider``` can use an ```Sundew.Generator.Input.FolderModelSetup``` to specify folder and file pattern to search for model files.

```EmptyModelProvider``` provides a single model through its default constructor.

#### **Implementing a model provider**

To implement a model provider use the  ```Sundew.Generator.Input.IModelProvider<in TSetup, in TModelSetup, TModel>``` interface.

The interface consists of a single async method that results a list of models.
GetModelsAsync takes two parameters, the setup with which the generation was setup and a model setup.

```csharp
public interface IModelProvider<in TSetup, in TModelSetup, TModel> : IModelProvider
    where TModelSetup : class
    where TModel : class
{
    Task<IReadOnlyList<IModelInfo<TModel>>> GetModelsAsync(TSetup setup, TModelSetup modelSetup);
}
```

##### **Generator Example**

[QuantityGenerator](https://github.com/hugener/Sundew.Quantities/tree/master/Sources/Sundew.Quantities.Generator/Quantities/QuantityGenerator.cs)

### **GeneratorSetups**

```Sundew.Generator.GeneratorSetup``` specify which generators to run and also allow to add generator specific settings.
Additionally, they may also specify any number of WriterSetups that will only be used for this particular generator and a boolean indicating whether the global WriterSetups should skip the generator (default false).

### **WriterSetups**

Writer setups help configure what happens with a generator's output. ```Sundew.Generator.Output.WriterSetup``` provides a target (any string) and a writer.

There is also the ```Sundew.Generator.Output.FileWriterSetup``` which can be used to specify a filename suffix and extension.

#### **Writers**

* ```Sundew.Generator.Writers.TextFileWriter``` writes text output of each run to a file and uses the ```WriterSetup.Target``` to specify the folder to write to.
* ```Sundew.Generator.Code.ProjectTextFileWriter``` is available in the [Sundew.Generator.Code](https://www.nuget.org/packages/Sundew.Generator.CodeAnalysis) NuGet package and can be used with the provided ```Sundew.Generator.Code.CodeSetup``` to generate C# code. It expects the ```WriterSetup.Target``` to be csproj or vbproj to determine the default namespace and root folder to store generated files.

#### **Implementing a writer**

Similar to generators and model providers, a custom writer can be added be implementing the ```Sundew.Generator.Output.IWriter<in TWriterSetup, TTarget, in TRun, in TOutput>``` interface.

This is by far the most complex interface because it can manage the output of a generator.

* **GetTargetAsync** runs in the very beginning of the generation process and provides a target for the generators prepare method. For example the ```Sundew.Generator.Writers.TextFileWriter``` builds the target folder path.
* **PrepareTargetAsync** runs after the generation process.
* **ApplyContentToTargetAsync** runs for each of the generated outputs of the generator and allows to gather the output or persist it.
* **CompleteTargetAsync** runs once as a last step in the generation process and may be used to persist gathered output if necessary.

```csharp
public interface IWriter<in TWriterSetup, TTarget, in TRun, in TOutput> : IWriter
    where TWriterSetup : IWriterSetup
    where TTarget : ITarget
    where TRun : IRun
{
    Task<TTarget> GetTargetAsync(TWriterSetup writerSetup);

    Task PrepareTargetAsync(TTarget target, TWriterSetup writerSetup);

    Task<string> ApplyContentToTargetAsync(TTarget target, TRun run, TWriterSetup writerSetup, TOutput output);

    Task CompleteTargetAsync(ITargetCompletionTracker targetCompletionTracker);
}
```

##### **Writer Example**

[TextFileWriter](Sources/Sundew.Generator/Output/TextFileWriter.cs)

## **Additional info**

### **Including generated code in a build**

With SDK-style projects files can be included with a wildcard an ItemGroup, while the WriterSetup can be configured to output the generated files to a .generated folder.

```xml
<ItemGroup>
  <Compile Include=".generated\**\*.cs" />
</ItemGroup>
```

It might also be suitable to add the .generated folder to .gitignore to avoid checking in generated files.

```
.generated/
```

### **Configuring build order**

In Visual Studio the Build Dependencies feature can be used to ensure that the generator runs before any consuming project without creating a project reference.

Right click the solution in Solution Explorer - Build Dependencies - Build Dependencies to set up the build order.

### **Number of runs**

The number times each generator runs is determined the following way:
```#WriterSetups * #Models * #Runs```

### **Json Serialized Setups**

Sundew.Generator support using json files to setup up the generator.
Use the RunAsync overload on the GeneratorFacade and specify the directory and file pattern to search for setup files.
```GeneratorFacade.RunAsync(string directory, string pattern, GeneratorOptions generatorOptions = null)```

In a json setup file a ```"Type"``` property may be specified with the fully qualified name to tell the deserializer, which type to use for Setup, GeneratorSetup, ModelSetup and WriterSetup.

## **License:**

MIT
