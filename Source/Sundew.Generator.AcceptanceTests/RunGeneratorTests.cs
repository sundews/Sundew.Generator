// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RunGeneratorTests.cs" company="Hukano">
// Copyright (c) Hukano. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sundew.Generator.AcceptanceTests
{
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Moq;
    using Sundew.Base.Collections;
    using Sundew.Generator.Core;
    using Sundew.Generator.Input;
    using Sundew.Generator.Output;
    using Sundew.Generator.Reporting;
    using Xunit;
    using ISetup = Sundew.Generator.ISetup;

    public class RunGeneratorTests
    {
        private readonly IWriter<IWriterSetup, ITarget, IRun, object> writer = New.Mock<IWriter<IWriterSetup, ITarget, IRun, object>>();
        private readonly IGenerator<ISetup, IGeneratorSetup, ITarget, object, IRun, object> generator = New.Mock<IGenerator<ISetup, IGeneratorSetup, ITarget, object, IRun, object>>();
        private readonly IModelProvider<ISetup, IModelSetup, object> modelProvider = New.Mock<IModelProvider<ISetup, IModelSetup, object>>();
        private readonly ITarget target = New.Mock<ITarget>();
        private readonly IRun run1 = New.Mock<IRun>();
        private readonly IProgressReporter progressReporter = New.Mock<IProgressReporter>();

        public RunGeneratorTests()
        {
            this.writer.Setup(x => x.GetTargetAsync(It.IsAny<IWriterSetup>())).Returns(Task.FromResult(this.target));
            this.modelProvider.Setup(x => x.GetModelsAsync(It.IsAny<ISetup>(), It.IsAny<IModelSetup>())).Returns(Task.FromResult<IReadOnlyList<IModelInfo<object>>>(new[] { new ModelInfo<object>(new object(), this.GetType().ToString()) }));
            this.generator.Setup(x => x.Prepare(It.IsAny<ISetup>(), It.IsAny<IGeneratorSetup>(), this.target, It.IsAny<object>(), It.IsAny<string>())).Returns(() => new List<IRun> { this.run1 });
            this.generator.Setup(x => x.Generate(It.IsAny<ISetup>(), It.IsAny<IGeneratorSetup>(), this.target, It.IsAny<object>(), this.run1, It.IsAny<long>())).Returns(() => new object());
        }

        [Fact]
        public async Task Given_ASetupWithOneGeneratorAndRunAndModelAndWriter_When_GeneratorRuns_Then_GeneratorGenerateShouldBeCalledOnce()
        {
            await this.RunGenerator().ConfigureAwait(false);

            this.generator.Verify(x => x.Generate(It.IsAny<ISetup>(), It.IsAny<IGeneratorSetup>(), this.target, It.IsAny<object>(), this.run1, It.IsAny<long>()), Times.Once());
        }

        [Fact]
        public async Task Given_ASetupWithOneGeneratorAndRunAndModelAndWriter_When_GeneratorRuns_Then_WriterCompleteTargetAsyncShouldBeCalledOnce()
        {
            await this.RunGenerator().ConfigureAwait(false);

            this.writer.Verify(x => x.CompleteTargetAsync(It.IsAny<ITargetCompletionTracker>()), Times.Once());
        }

        [Fact]
        public async Task Given_ASetupWithOneGeneratorAndTwoRunsAndOneModelAndWriter_When_GeneratorRuns_Then_GeneratorGenerateShouldBeCalledShouldBeCalledTwice()
        {
            var run2 = New.Mock<IRun>();
            this.generator.Setup(x => x.Prepare(It.IsAny<ISetup>(), It.IsAny<IGeneratorSetup>(), this.target, It.IsAny<object>(), It.IsAny<string>())).Returns(() => new List<IRun> { this.run1, run2 });
            this.generator.Setup(x => x.Generate(It.IsAny<ISetup>(), It.IsAny<IGeneratorSetup>(), this.target, It.IsAny<object>(), run2, It.IsAny<long>())).Returns(() => new object());

            await this.RunGenerator().ConfigureAwait(false);

            this.generator.Verify(x => x.Generate(It.IsAny<ISetup>(), It.IsAny<IGeneratorSetup>(), this.target, It.IsAny<object>(), It.IsAny<IRun>(), It.IsAny<long>()), Times.Exactly(2));
        }

        [Fact]
        public async Task Given_ASetupWithOneGeneratorAndTwoRunsAndOneModelAndWriter_When_GeneratorRuns_Then_WriterGetTargetAsyncShouldBeCalledOnce()
        {
            var run2 = New.Mock<IRun>();
            this.generator.Setup(x => x.Prepare(It.IsAny<ISetup>(), It.IsAny<IGeneratorSetup>(), this.target, It.IsAny<object>(), It.IsAny<string>())).Returns(() => new List<IRun> { this.run1, run2 });
            this.generator.Setup(x => x.Generate(It.IsAny<ISetup>(), It.IsAny<IGeneratorSetup>(), this.target, It.IsAny<object>(), run2, It.IsAny<long>())).Returns(() => new object());

            await this.RunGenerator().ConfigureAwait(false);

            this.writer.Verify(x => x.GetTargetAsync(It.IsAny<IWriterSetup>()), Times.Once);
        }

        [Fact]
        public async Task Given_ASetupWithOneGeneratorAndTwoRunsAndOneModelAndWriter_When_GeneratorRuns_Then_WriterPrepareTargetAsyncShouldBeCalledOnce()
        {
            var run2 = New.Mock<IRun>();
            this.generator.Setup(x => x.Prepare(It.IsAny<ISetup>(), It.IsAny<IGeneratorSetup>(), this.target, It.IsAny<object>(), It.IsAny<string>())).Returns(() => new List<IRun> { this.run1, run2 });
            this.generator.Setup(x => x.Generate(It.IsAny<ISetup>(), It.IsAny<IGeneratorSetup>(), this.target, It.IsAny<object>(), run2, It.IsAny<long>())).Returns(() => new object());

            await this.RunGenerator().ConfigureAwait(false);

            this.writer.Verify(x => x.PrepareTargetAsync(It.IsAny<ITarget>(), It.IsAny<IWriterSetup>()), Times.Once);
        }

        [Fact]
        public async Task Given_ASetupWithOneGeneratorAndTwoRunsAndOneModelAndWriter_When_GeneratorRuns_Then_WriterApplyContentToTargetAsyncShouldBeCalledTwice()
        {
            var run2 = New.Mock<IRun>();
            this.generator.Setup(x => x.Prepare(It.IsAny<ISetup>(), It.IsAny<IGeneratorSetup>(), this.target, It.IsAny<object>(), It.IsAny<string>())).Returns(() => new List<IRun> { this.run1, run2 });
            this.generator.Setup(x => x.Generate(It.IsAny<ISetup>(), It.IsAny<IGeneratorSetup>(), this.target, It.IsAny<object>(), run2, It.IsAny<long>())).Returns(() => new object());

            await this.RunGenerator().ConfigureAwait(false);

            this.writer.Verify(x => x.ApplyContentToTargetAsync(It.IsAny<ITarget>(), It.IsAny<IRun>(), It.IsAny<IWriterSetup>(), It.IsAny<object>()), Times.Exactly(2));
        }

        [Fact]
        public async Task Given_ASetupWithOneGeneratorAndTwoRunsAndOneModelAndWriter_When_GeneratorRuns_Then_WriterCompleteTargetAsyncShouldBeCalledOnce()
        {
            var run2 = New.Mock<IRun>();
            this.generator.Setup(x => x.Prepare(It.IsAny<ISetup>(), It.IsAny<IGeneratorSetup>(), this.target, It.IsAny<object>(), It.IsAny<string>())).Returns(() => new List<IRun> { this.run1, run2 });
            this.generator.Setup(x => x.Generate(It.IsAny<ISetup>(), It.IsAny<IGeneratorSetup>(), this.target, It.IsAny<object>(), run2, It.IsAny<long>())).Returns(() => new object());

            await this.RunGenerator().ConfigureAwait(false);

            this.writer.Verify(x => x.CompleteTargetAsync(It.IsAny<ITargetCompletionTracker>()), Times.Once);
        }

        [Theory]
        [InlineData(true, 1)]
        [InlineData(false, 2)]
        public async Task Given_ASetupWithTwoGeneratorsAndOneModelAndWriter_When_GeneratorRuns_Then_WriterCompleteTargetAsyncShouldBeCalledExpectedNumberOfTimes(bool shareGlobalWriters, int expectedNumberOfCalls)
        {
            var generator2 = New.Mock<IGenerator<ISetup, IGeneratorSetup, ITarget, object, IRun, object>>();
            generator2.Setup(x => x.Prepare(It.IsAny<ISetup>(), It.IsAny<IGeneratorSetup>(), this.target, It.IsAny<object>(), It.IsAny<string>())).Returns(() => new List<IRun> { this.run1 });
            generator2.Setup(x => x.Generate(It.IsAny<ISetup>(), It.IsAny<IGeneratorSetup>(), this.target, It.IsAny<object>(), this.run1, It.IsAny<long>())).Returns(() => new object());

            await this.RunGenerator(new[] { this.generator, generator2 }, shareGlobalWriters).ConfigureAwait(false);

            this.writer.Verify(x => x.CompleteTargetAsync(It.IsAny<ITargetCompletionTracker>()), Times.Exactly(expectedNumberOfCalls));
        }

        private Task<ConcurrentBag<string>> RunGenerator(IGenerator[]? generators = null, bool shareGlobalWriters = false)
        {
            generators ??= new[] { this.generator };
            var generatorSetups = generators.ConvertAll(x =>
                new GeneratorSetup(new TypeOrObject<IGenerator>(x), null, false, shareGlobalWriters));
            return GeneratorFacade.RunAsync(
                new GeneratorOptions { ProgressReporter = this.progressReporter },
                new Setup(
                    new ModelSetup(new TypeOrObject<IModelProvider>(this.modelProvider), typeof(object)),
                    new[] { new WriterSetup("AnyTarget", new TypeOrObject<IWriter>(this.writer)) },
                    generatorSetups!));
        }
    }
}
