// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

using MineJason.SNbt;
using MineJason.SNbt.Parsing;

namespace MineJason.Tests.NBT;

public class ListParserTests
{
    [Fact]
    public void ParsePrimitive_SimpleList()
    {
        // Arrange
        const string list = "[123b,12b]";
        var parser = new SNbtListParser(new StringReader(list));

        // Act
        var result = parser.ReadPrimitive();

        // Assert
        Assert.Equal(list, result?.ToSNbtString());
    }

    [Fact]
    public void ParsePrimitive_NestedList()
    {
        // Arrange
        const string list = "[[1b,2b,3b,4b,5b],[10b,20b,30b,40b,50b]]";
        var parser = new SNbtListParser(new StringReader(list));

        // Act
        var result = parser.ReadPrimitive();

        // Assert
        Assert.Equal(list, result?.ToSNbtString());
    }

    [Fact]
    public void ParsePrimitive_QuotedStrings()
    {
        // Arrange
        const string list = "['Not A list','Again not a list']";
        var parser = new SNbtListParser(new StringReader(list));

        // Act
        var result = parser.ReadPrimitive();

        // Assert
        Assert.Equal(list, result?.ToSNbtString());
    }

    [Fact]
    public void ParsePrimitive_Heterogeneous()
    {
        // Arrange
        const string list = "['Not A list',123b,'A list']";
        var parser = new SNbtListParser(new StringReader(list));

        // Act
        var result = parser.ReadPrimitive();

        // Assert
        Assert.Equal("[{\"\":'Not A list'},{\"\":123b},{\"\":'A list'}]", result?.ToSNbtString());
    }

    [Fact]
    public void ParseHeterogeneous_Homogeneous_AlwaysHeterogeneous()
    {
        // Arrange
        const string list = "[121b,122b,123b]";
        var parser = new SNbtListParser(new StringReader(list));

        // Act
        var result = parser.ReadHeterogeneous();

        // Assert
        Assert.Equal("[{\"\":121b},{\"\":122b},{\"\":123b}]", result?.ToSNbtString());
    }
}
