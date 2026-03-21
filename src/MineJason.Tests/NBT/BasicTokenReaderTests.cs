// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

using MineJason.SNbt.Parsing;

namespace MineJason.Tests.NBT;

public class BasicTokenReaderTests
{
    [Fact]
    public void ReadToken_UnquotedString()
    {
        // Arrange
        var str = new StringReader("unquoted_string next");
        var reader = new SNbtBasicTokenReader(str);
        
        // Act
        var token = reader.ReadToken();
        
        // Assert
        Assert.Equal(new SNbtBasicToken(BasicTokenType.UnquotedWord, "unquoted_string"),
            token);
    }
    
    [Fact]
    public void ReadToken_QuotedString_Single()
    {
        // Arrange
        var str = new StringReader("'Single Quoted String'");
        var reader = new SNbtBasicTokenReader(str);
        
        // Act
        var token = reader.ReadToken();
        
        // Assert
        Assert.Equal(new SNbtBasicToken(BasicTokenType.SingleQuotedString, "Single Quoted String"),
            token);
    }
    
    [Fact]
    public void ReadToken_QuotedString_Double()
    {
        // Arrange
        var str = new StringReader("\"Double Quoted String\"");
        var reader = new SNbtBasicTokenReader(str);
        
        // Act
        var token = reader.ReadToken();
        
        // Assert
        Assert.Equal(new SNbtBasicToken(BasicTokenType.DoubleQuotedString, "Double Quoted String"),
            token);
    }
    
    [Fact]
    public void ReadToken_Compound()
    {
        // Arrange
        const string compound = "{tagA:true,tagB:true}";
        var str = new StringReader(compound);
        var reader = new SNbtBasicTokenReader(str);
        
        // Act
        var token = reader.ReadToken();
        
        // Assert
        Assert.Equal(new SNbtBasicToken(BasicTokenType.Compound, compound),
            token);
    }
    
    [Fact]
    public void ReadToken_Compound_Nested()
    {
        // Arrange
        const string compound = "{tagA:true,tagB:{tagC:true}}";
        var str = new StringReader(compound);
        var reader = new SNbtBasicTokenReader(str);
        
        // Act
        var token = reader.ReadToken();
        
        // Assert
        Assert.Equal(new SNbtBasicToken(BasicTokenType.Compound, compound),
            token);
    }

    [Fact]
    public void ReadToken_List_Flat()
    {
        // Arrange
        const string list = "[1b,2b,3b,4b,5b]";
        var str = new StringReader(list);
        var reader = new SNbtBasicTokenReader(str);
        
        // Act
        var token = reader.ReadToken();
        
        // Assert
        Assert.Equal(new SNbtBasicToken(BasicTokenType.List, list),
            token);
    }

    [Fact]
    public void ReadToken_List_Flat_QuotedStrings()
    {
        // Arrange
        const string list = "[\"[]\",\"[this is not a list]\"]";
        var str = new StringReader(list);
        var reader = new SNbtBasicTokenReader(str);
        
        // Act
        var token = reader.ReadToken();
        
        // Assert
        Assert.Equal(new SNbtBasicToken(BasicTokenType.List, list),
            token);
    }


    [Fact]
    public void ReadToken_List_Nested()
    {
        // Arrange
        const string list = "[[12b,123b],[1b,2b,3b]]";
        var str = new StringReader(list);
        var reader = new SNbtBasicTokenReader(str);
        
        // Act
        var token = reader.ReadToken();
        
        // Assert
        Assert.Equal(new SNbtBasicToken(BasicTokenType.List, list),
            token);
    }
    
    [Fact]
    public void ReadQuotedString_DoubleQuoted()
    {
        // Arrange
        var str = new StringReader("\"Double Quoted String\"");
        var reader = new SNbtBasicTokenReader(str);
        
        // Act
        var result = reader.ReadQuotedString();
        
        // Assert
        Assert.Equal("Double Quoted String", result);
    }
    
    [Fact]
    public void ReadQuotedString_SingleQuoted()
    {
        // Arrange
        var str = new StringReader("'Single Quoted String'");
        var reader = new SNbtBasicTokenReader(str);
        
        // Act
        var result = reader.ReadQuotedString();
        
        // Assert
        Assert.Equal("Single Quoted String", result);
    }
    
    [Fact]
    public void ReadQuotedString_Invalid()
    {
        // Arrange
        var str = new StringReader("blah");
        var reader = new SNbtBasicTokenReader(str);
        
        // Act
        var exception = Record.Exception(reader.ReadQuotedString);
        
        // Assert
        Assert.IsType<FormatException>(exception);
    }
}