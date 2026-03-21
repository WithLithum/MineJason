// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

using MineJason.SNbt.Parsing;

namespace MineJason.Tests.NBT;

public class BasicTokenTests
{
    [Fact]
    public void BasicTokenResolver_SingleQuoted()
    {
        // Arrange
        const string str = "'SingleQuoted'";
        
        // Act
        var result = BasicTokenResolver.Resolve(str[0]);
        
        // Assert
        Assert.Equal(BasicTokenType.SingleQuotedString, result);
    }
    
    [Fact]
    public void BasicTokenResolver_DoubleQuoted()
    {
        // Arrange
        const string str = "\"Quoted\"";
        
        // Act
        var result = BasicTokenResolver.Resolve(str[0]);
        
        // Assert
        Assert.Equal(BasicTokenType.DoubleQuotedString, result);
    }
    
    [Fact]
    public void BasicTokenResolver_Compound()
    {
        // Arrange
        const string str = "{Tag:123}";
        
        // Act
        var result = BasicTokenResolver.Resolve(str[0]);
        
        // Assert
        Assert.Equal(BasicTokenType.Compound, result);
    }
    
    [Fact]
    public void BasicTokenResolver_Unquoted()
    {
        // Arrange
        const string str = "true";
        
        // Act
        var result = BasicTokenResolver.Resolve(str[0]);
        
        // Assert
        Assert.Equal(BasicTokenType.UnquotedWord, result);
    }
}