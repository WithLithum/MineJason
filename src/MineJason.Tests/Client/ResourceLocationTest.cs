// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

namespace MineJason.Tests.Client;

using System.Text.Json;

public class ResourceLocationTest
{
    [Fact]
    public void IsPathValid_Invalid_EmptyDirectoryName()
    {
        // Arrange
        const string path = "a//b";
        
        // Act
        var valid = ResourceLocation.IsPathValid(path);
        
        // Assert
        Assert.False(valid);
    }

    [Fact]
    public void IsPathValid_Valid_OkayDirectory()
    {
        // Arrange
        const string path = "aa/bb/cc";
        
        // Act
        var valid = ResourceLocation.IsPathValid(path);
        
        // Assert
        Assert.True(valid);
    }

    [Fact]
    public void IsPathValid_Invalid_UpperCaseLetterInLowerCase()
    {
        // Arrange
        const string path = "aa/Bb/cc";
        
        // Act
        var valid = ResourceLocation.IsPathValid(path);
        
        // Assert
        Assert.False(valid);
    }

    [Fact]
    public void IsPathValid_Invalid_AllUpperCase()
    {
        // Arrange
        const string path = "AA/BB/CC";
        
        // Act
        var valid = ResourceLocation.IsPathValid(path);
        
        // Assert
        Assert.False(valid);
    }

    [Fact]
    public void IsPathValid_Invalid_UnlawfulSymbol()
    {
        // Arrange
        const string path = "AA/!!/CC";
        
        // Act
        var valid = ResourceLocation.IsPathValid(path);
        
        // Assert
        Assert.False(valid);
    }

    [Fact]
    public void IsPathValid_Invalid_FullId()
    {
        // Arrange
        const string path = "minecraft:short_grass";
        
        // Act
        var valid = ResourceLocation.IsPathValid(path);
        
        // Assert
        Assert.False(valid);
    }

    [Fact]
    public void IsPathValid_Valid_RegularOkayPath()
    {
        // Arrange
        const string path = "gameplay/features_set/oxygen/enable_oxygen";
        
        // Act
        var valid = ResourceLocation.IsPathValid(path);
        
        // Assert
        Assert.True(valid);
    }

    [Fact]
    public void IsPathValid_False_Cyrillic()
    {
        // Arrange
        const string path = "a/кириллица/b";
        
        // Act
        var valid = ResourceLocation.IsPathValid(path);
        
        // Assert
        Assert.False(valid);
    }

    [Fact]
    public void IsPathValid_Chinese()
    {
        // Arrange
        const string path = "aa/囧/bb";
        
        // Act
        var valid = ResourceLocation.IsPathValid(path);
        
        // Assert
        // ResourceLocation任何部分都不应该接受除英文字母和部分半角符号以外的一切内容
        Assert.False(valid);
    }

    [Fact]
    public void Deserialize_DictionaryKey()
    {
        // Arrange
        const string testJson = """
            {"minecraft:test":"key value"}
            """;

        // Act
        var dictionary = JsonSerializer.Deserialize<Dictionary<ResourceLocation, string>>(testJson);
        
        // Assert
        Assert.Equal(new ResourceLocation("minecraft", "test"),
            dictionary!.First().Key);
    }
}