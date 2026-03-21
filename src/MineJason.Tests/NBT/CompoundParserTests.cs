// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

using MineJason.SNbt.Parsing;
using MineJason.SNbt.Values;
using MineJason.SNbt;

namespace MineJason.Tests.NBT;

public class CompoundReaderTests
{
    [Fact]
    public void Parser_ReadUntil()
    {
        // Arrange
        var parser = new SNbtCompoundReader(new StringReader("text_text_text:"));
        
        // Act
        var result = parser.ReadUntil(':');
        
        // Assert
        Assert.Equal("text_text_text", result);
    }

    [Fact]
    public void Parser_ReadUnquotedTagKey()
    {
        // Arrange
        var parser = new SNbtCompoundReader(new StringReader("tag_key:"));
        
        // Act
        var result = parser.ReadUnquotedTagKey();
        
        // Assert
        Assert.Equal("tag_key", result);
    }
    
    [Fact]
    public void Parser_ReadUnquotedTagKey_DisallowWhitespace()
    {
        // Arrange
        var parser = new SNbtCompoundReader(new StringReader("tag key:"));
        
        // Act
        var exception = Record.Exception(parser.ReadUnquotedTagKey);
        
        // Assert
        Assert.IsType<FormatException>(exception);
    }
    
    [Fact]
    public void Parser_ReadQuotedTagKey()
    {
        // Arrange
        var parser = new SNbtCompoundReader(new StringReader("\"minecraft:lore\":"));
        
        // Act
        var result = parser.ReadQuotedTagKey();
        
        // Assert
        Assert.Equal("minecraft:lore", result);
    }

    [Fact]
    public void Parser_ReadCompound_SingleProperty()
    {
        // Arrange
        const string nbt = "{test:123}";
        var parser = new SNbtCompoundParser(new SNbtCompoundReader(new StringReader(nbt)));

        // Act
        var result = parser.ReadCompound();
        
        // Assert
        Assert.Equal(nbt, result.ToSNbtString());
    }
    
    [Fact]
    public void Parser_ReadCompound()
    {
        // Arrange
        const string nbt = "{test:123,test2:345}";
        var parser = new SNbtCompoundParser(new SNbtCompoundReader(new StringReader(nbt)));

        // Act
        var result = parser.ReadCompound();
        
        // Assert
        Assert.Equal(nbt, result.ToSNbtString());
    }

    [Fact]
    public void Parser_ReadCompound_Nested()
    {
        // Arrange
        const string nbt = "{test:123,test2:345,nest:{test3:456}}";
        var parser = new SNbtCompoundParser(new SNbtCompoundReader(new StringReader(nbt)));

        // Act
        var result = parser.ReadCompound();
        
        // Assert
        Assert.Equal(nbt, result.ToSNbtString());
    }

    [Fact]
    public void OrHListValue_SingleEmptyKeyValue_HListValue()
    {
        // Arrange
        const string nbt = "{\"\":100b}";
        var parser = new SNbtCompoundParser(new SNbtCompoundReader(new StringReader(nbt)));

        // Act
        var result = parser.ReadCompoundOrHListValue();

        // Assert
        Assert.Null(result.Item1);
    }

    [Fact]
    public void OrHListValue_HListValue_ReadValueCorrectly()
    {
        // Arrange
        const string nbt = "{\"\":100b}";
        var parser = new SNbtCompoundParser(new SNbtCompoundReader(new StringReader(nbt)));

        // Act
        var result = parser.ReadCompoundOrHListValue();

        // Assert
        Assert.Equal((sbyte)100, Assert.IsType<SNbtByteValue>(result.Item2.Value).Value);
    }

    [Fact]
    public void OrHListValue_SingleValueWithNamedKey_FullCompound()
    {
        // Arrange
        const string nbt = "{\"value1\":100b}";
        var parser = new SNbtCompoundParser(new SNbtCompoundReader(new StringReader(nbt)));

        // Act
        var result = parser.ReadCompoundOrHListValue();

        // Assert
        Assert.NotNull(result.Item1);
    }

    [Fact]
    public void OrHListValue_SingleValueWithUnquotedNamedKey_FullCompound()
    {
        // Arrange
        const string nbt = "{value2:123b}";
        var parser = new SNbtCompoundParser(new SNbtCompoundReader(new StringReader(nbt)));

        // Act
        var result = parser.ReadCompoundOrHListValue();

        // Assert
        Assert.NotNull(result.Item1);
    }

    [Fact]
    public void OrHListValue_BeginWithEmptyKeyButHasMoreValues_FullCompound()
    {
        // Arrange
        const string nbt = "{\"\":\"value\",\"test2\":123}";
        var parser = new SNbtCompoundParser(new SNbtCompoundReader(new StringReader(nbt)));

        // Act
        var result = parser.ReadCompoundOrHListValue();

        // Assert
        Assert.NotNull(result.Item1);
    }
}