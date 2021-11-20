// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SetupInfoFactoryTests.cs" company="Hukano">
// Copyright (c) Hukano. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sundew.Generator.Tests;

using FluentAssertions;
using Sundew.Base.Collections;
using Sundew.Generator.Discovery;
using Sundew.Generator.Output;
using Xunit;

public class SetupInfoFactoryTests
{
    [Fact]
    public void CreateSetupInfo_Then_ResultContainOneItem()
    {
        var setup =
            new Setup
            {
                WriterSetups = new IWriterSetup[]
                {
                    new WriterSetup(@"c:\temp")
                    {
                        Writer = new TextFileWriter(),
                    },
                },
                GeneratorSetups = new[] { new GeneratorSetup() },
            };

        var result = SetupInfoFactory.CreateSetupInfos(setup.ToEnumerable());

        result.Should().HaveCount(1);
    }
}