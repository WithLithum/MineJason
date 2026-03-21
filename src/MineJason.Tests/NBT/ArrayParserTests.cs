// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

using MineJason.SNbt.Parsing;
using MineJason.SNbt;

namespace MineJason.Tests.NBT;

public class ArrayParserTests
{
    [Fact]
    public void ParseArray_Int()
    {
        // Arrange
        const string array = "[I;123,456,789]";
        var token = new SNbtBasicToken(BasicTokenType.List, array);
        var parser = new SNbtArrayParser(new StringReader(array), SNbtTagType.IntArray);
        
        // Act
        var result = parser.ParseArray();
        
        // Assert
        Assert.Equal(array, result.ToSNbtString());
    }
    
    [Fact]
    public void ParseArray_Int_HasNegative()
    {
        // Arrange
        const string array = "[I;-123,456,-789]";
        var token = new SNbtBasicToken(BasicTokenType.List, array);
        var parser = new SNbtArrayParser(new StringReader(array), SNbtTagType.IntArray);
        
        // Act
        var result = parser.ParseArray();
        
        // Assert
        Assert.Equal(array, result.ToSNbtString());
    }
    
    [Fact]
    public void ParseArray_Long()
    {
        // Arrange
        const string array = "[L;123L,456L,789L]";
        var token = new SNbtBasicToken(BasicTokenType.List, array);
        var parser = new SNbtArrayParser(new StringReader(array), SNbtTagType.LongArray);
        
        // Act
        var result = parser.ParseArray();
        
        // Assert
        Assert.Equal(array, result.ToSNbtString());
    }
    
    [Fact]
    public void ParseArray_Long_HasNegative()
    {
        // Arrange
        const string array = "[L;-123L,456L,-789L]";
        var token = new SNbtBasicToken(BasicTokenType.List, array);
        var parser = new SNbtArrayParser(new StringReader(array), SNbtTagType.LongArray);
        
        // Act
        var result = parser.ParseArray();
        
        // Assert
        Assert.Equal(array, result.ToSNbtString());
    }
    
    [Fact]
    public void ParseArray_Long_NoSuffix()
    {
        // Arrange
        const string array = "[L;123,456,789]";
        var token = new SNbtBasicToken(BasicTokenType.List, array);
        var parser = new SNbtArrayParser(new StringReader(array), SNbtTagType.LongArray);
        
        // Act
        var exception = Record.Exception(() => parser.ParseArray());
        
        // Assert
        Assert.IsType<FormatException>(exception);
    }
    
    [Fact]
    public void ParseArray_Byte()
    {
        // Arrange
        const string array = "[B;123b,12b,23b]";
        var token = new SNbtBasicToken(BasicTokenType.List, array);
        var parser = new SNbtArrayParser(new StringReader(array), SNbtTagType.ByteArray);
        
        // Act
        var result = parser.ParseArray();
        
        // Assert
        Assert.Equal(array, result.ToSNbtString());
    }
    
    [Fact]
    public void ParseArray_Byte_HasNegative()
    {
        // Arrange
        const string array = "[B;-123b,12b,23b]";
        var token = new SNbtBasicToken(BasicTokenType.List, array);
        var parser = new SNbtArrayParser(new StringReader(array), SNbtTagType.ByteArray);
        
        // Act
        var result = parser.ParseArray();
        
        // Assert
        Assert.Equal(array, result.ToSNbtString());
    }
    
    [Fact]
    public void ParseArray_Byte_NoSuffix()
    {
        // Arrange
        const string array = "[B:123,12,23]";
        var token = new SNbtBasicToken(BasicTokenType.List, array);
        var parser = new SNbtArrayParser(new StringReader(array), SNbtTagType.ByteArray);
        
        // Act
        var exception = Record.Exception(() => parser.ParseArray());
        
        // Assert
        Assert.IsType<FormatException>(exception);
    }
}