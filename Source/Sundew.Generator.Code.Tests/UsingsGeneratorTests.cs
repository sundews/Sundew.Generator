// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UsingsGeneratorTests.cs" company="Hukano">
// Copyright (c) Hukano. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sundew.Generator.Code.Tests;

using FluentAssertions;
using Sundew.Generator.Code.CSharp;
using Xunit;

public class UsingsGeneratorTests
{
    [Fact]
    public void GetUsings_Then_ResultShouldBeExpectedResult()
    {
        var usings1 = new[] { "Microsoft.Windows", "System.Collections", "Microsoft.Windows" };
        var usings2 = new[] { "Microsoft.Win32", "System" };
        const string expectedResult = @"using System;
using System.Collections;
using Microsoft.Win32;
using Microsoft.Windows;";

        var result = UsingsHelper.GetUsings(false, 0, true, usings1, usings2);

        result.Should().Be(expectedResult);
    }
}