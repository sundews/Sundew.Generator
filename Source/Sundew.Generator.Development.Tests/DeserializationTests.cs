// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DeserializationTests.cs" company="Sundews">
// Copyright (c) Sundews. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sundew.Generator.Development.Tests;

using AwesomeAssertions;
using Newtonsoft.Json;
using Sundew.Generator.Converters.Json;
using Xunit;

public class DeserializationTests
{
    [Fact]
    public void DeserializeObject_When_TextContainsWriterSetup_Then_ResultWriterSetupShouldBeEmpty()
    {
        const string input = @"
{
    ""Type"": ""Sundew.Generator.Development.Tests.Setup, Sundew.Generator.Development.Tests"",
    ""WriterSetups"": [
        { ""Target"": ""c:\\temp\\hmm.txt"", ""Writer"": ""Sundew.Generator.Development.Tests.TextWriter, Sundew.Generator.Development.Tests"", ""AddFilesToProject"": false },
        { ""Target"": ""c:\\temp\\hmm2.txt"", ""AddFilesToProject"": true }
    ],
    ""GeneratorSetups"": [
        {
            ""Generator"": ""Sundew.Generator.Development.Tests.EmptyGenerator, Sundew.Generator.Development.Tests"",
            ""WriterSetups"": [
                { ""Target"": ""c:\\temp\\hmm3.txt"", ""AddFilesToProject"": false },
                { ""Target"": ""c:\\temp\\hmm4.txt"", ""AddFilesToProject"": true }
            ]
        }
    ],
    ""ModelType"": ""Sundew.Generator.Development.Tests.TextWriter, Sundew.Generator.Development.Tests""
}";
        var result = JsonConvert.DeserializeObject<ISetup>(
            input,
            new SetupJsonConverter(),
            new WriterSetupJsonConverter(),
            new GeneratorSetupJsonConverter(),
            new TypeOrObjectJsonConverter(),
            new ModelSetupJsonConverter());

        result!.WriterSetups.Should().NotBeEmpty();
    }
}