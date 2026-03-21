// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

using MineJason.SNbt.Parsing;

namespace MineJason.Tests.NBT;

public class TagResolverTest
{
    [Fact]
    public void ResolvePrimitive_Int()
    {
        // Arrange
        var token = new SNbtBasicToken(BasicTokenType.UnquotedWord, "12345678");

        // Act
        var result = SNbtTagResolver.ResolvePrimitive(token);

        // Assert
        Assert.Equal(SNbtTagType.Int, result);
    }

    [Fact]
    public void ResolvePrimitive_Double()
    {
        // Arrange
        var token = new SNbtBasicToken(BasicTokenType.UnquotedWord, "1234.5678");

        // Act
        var result = SNbtTagResolver.ResolvePrimitive(token);

        // Assert
        Assert.Equal(SNbtTagType.Double, result);
    }

    [Fact]
    public void ResolvePrimitive_Float()
    {
        // Arrange
        var token = new SNbtBasicToken(BasicTokenType.UnquotedWord, "1234.5f");

        // Act
        var result = SNbtTagResolver.ResolvePrimitive(token);

        // Assert
        Assert.Equal(SNbtTagType.Float, result);
    }

    [Fact]
    public void ResolvePrimitive_Byte()
    {
        // Arrange
        var token = new SNbtBasicToken(BasicTokenType.UnquotedWord, "12b");

        // Act
        var result = SNbtTagResolver.ResolvePrimitive(token);

        // Assert
        Assert.Equal(SNbtTagType.Byte, result);
    }

    [Fact]
    public void ResolvePrimitive_Long()
    {
        // Arrange
        var token = new SNbtBasicToken(BasicTokenType.UnquotedWord, "123456789012345678l");

        // Act
        var result = SNbtTagResolver.ResolvePrimitive(token);

        // Assert
        Assert.Equal(SNbtTagType.Long, result);
    }

    [Fact]
    public void ResolvePrimitive_IntArray()
    {
        // Arrange
        var token = new SNbtBasicToken(BasicTokenType.List, "[I;123,456]");

        // Act
        var result = SNbtTagResolver.ResolvePrimitive(token);

        // Assert
        Assert.Equal(SNbtTagType.IntArray, result);
    }

    [Fact]
    public void ResolvePrimitive_LongArray()
    {
        // Arrange
        var token = new SNbtBasicToken(BasicTokenType.List, "[L;123l,456l]");

        // Act
        var result = SNbtTagResolver.ResolvePrimitive(token);

        // Assert
        Assert.Equal(SNbtTagType.LongArray, result);
    }

    [Fact]
    public void ResolvePrimitive_ByteArray()
    {
        // Arrange
        var token = new SNbtBasicToken(BasicTokenType.List, "[B;12b,34b]");

        // Act
        var result = SNbtTagResolver.ResolvePrimitive(token);

        // Assert
        Assert.Equal(SNbtTagType.ByteArray, result);
    }
}
