// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

using MineJason.SNbt.Parsing;

namespace MineJason.Tests.NBT;

public class SNbtParseTests
{
    [Fact]
    public void ParseToken_Int()
    {
        // Arrange
        var token = new SNbtBasicToken(BasicTokenType.UnquotedWord, "12345678");

        // Act
        var result = SNbtToken.ParseIntTag(token);

        // Assert
        Assert.Equal(12345678, result.Value);
    }

    [Fact]
    public void ParseToken_Long()
    {
        // Arrange
        var token = new SNbtBasicToken(BasicTokenType.UnquotedWord, "123456789012345678L");

        // Act
        var result = SNbtToken.ParseLongTag(token);

        // Assert
        Assert.Equal(123456789012345678L, result.Value);
    }

    [Fact]
    public void ParseToken_Double_Decimal()
    {
        // Arrange
        var token = new SNbtBasicToken(BasicTokenType.UnquotedWord, "1234.5678");

        // Act
        var result = SNbtToken.ParseDoubleTag(token);

        // Assert
        Assert.Equal(1234.5678, result.Value);
    }
    
    [Fact]
    public void ParseToken_Double_Suffix()
    {
        // Arrange
        var token = new SNbtBasicToken(BasicTokenType.UnquotedWord, "123d");

        // Act
        var result = SNbtToken.ParseDoubleTag(token);

        // Assert
        Assert.Equal(123.0, result.Value);
    }

    [Fact]
    public void ParseToken_Float()
    {
        // Arrange
        var token = new SNbtBasicToken(BasicTokenType.UnquotedWord, "1234.56f");

        // Act
        var result = SNbtToken.ParseFloatTag(token);

        // Assert
        Assert.Equal(1234.56f, result.Value);
    }

    [Fact]
    public void ParseToken_Byte_True()
    {
        // Arrange
        var token = new SNbtBasicToken(BasicTokenType.UnquotedWord, "true");

        // Act
        var result = SNbtToken.ParseByteTag(token);

        // Assert
        Assert.Equal(1, result.Value);
    }

    [Fact]
    public void ParseToken_Byte_False()
    {
        // Arrange
        var token = new SNbtBasicToken(BasicTokenType.UnquotedWord, "false");

        // Act
        var result = SNbtToken.ParseByteTag(token);

        // Assert
        Assert.Equal(0, result.Value);
    }

    [Fact]
    public void ParseToken_Byte_Number()
    {
        // Arrange
        var token = new SNbtBasicToken(BasicTokenType.UnquotedWord, "127b");

        // Act
        var result = SNbtToken.ParseByteTag(token);

        // Assert
        Assert.Equal(127, result.Value);
    }
}
