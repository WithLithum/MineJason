// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

namespace MineJason.Tests.Client;

using System.Text.Json;

public class ResourceLocationTest
{
    [Fact]
    public void Constructor_InvalidNamespace_Throws()
    {
        // Arrange
        const string nameSpace = "a/b";
        const string path = "some_path";
        
        // Act
        var exception = Record.Exception(() => new ResourceLocation(nameSpace, path));
        
        // Assert
        Assert.IsType<ArgumentException>(exception);
    }
    
    [Fact]
    public void Constructor_InvalidPath_Throws()
    {
        // Arrange
        const string nameSpace = "minecraft";
        const string path = "a//b";
        
        // Act
        var exception = Record.Exception(() => new ResourceLocation(nameSpace, path));
        
        // Assert
        Assert.IsType<ArgumentException>(exception);
    }

    [Fact]
    public void IsValid_ValidInstance_ReturnsTrue()
    {
        // Arrange
        var subject = new ResourceLocation("minecraft", "air");
        
        // Act
        var result = subject.IsValid();
        
        // Assert
        Assert.True(result);
    }
    
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
    
    [Fact]
    public void TryParse_InvalidNamespace_Fail()
    {
        // Arrange
        const string input = "a/b:c";
        
        // Act
        var success = ResourceLocation.TryParse(input, out _);
        
        // Assert
        Assert.False(success);
    }
    
    [Fact]
    public void TryParse_InvalidPath_Fail()
    {
        // Arrange
        const string input = "a:b_囧_c";
        
        // Act
        var success = ResourceLocation.TryParse(input, out _);
        
        // Assert
        Assert.False(success);
    }
    
    [Fact]
    public void TryParse_TooManySegments_Fail()
    {
        // Arrange
        const string input = "a:b:c";
        
        // Act
        var success = ResourceLocation.TryParse(input, out _);
        
        // Assert
        Assert.False(success);
    }

    [Fact]
    public void Equals_SameContent_ReturnsTrue()
    {
        // Arrange
        var a = new ResourceLocation("minecraft", "air");
        var b = new ResourceLocation("minecraft", "air");
        
        // Act
        var result = a.Equals(b);
        
        // Assert
        Assert.True(result);
    }
    
    [Fact]
    public void Equals_DifferentContent_ReturnsFalse()
    {
        // Arrange
        var a = new ResourceLocation("minecraft", "air");
        var b = new ResourceLocation("minecraft", "water");
        
        // Act
        var result = a.Equals(b);
        
        // Assert
        Assert.False(result);
    }
    
    [Fact]
    public void EqualsObject_SameContent_ReturnsTrue()
    {
        // Arrange
        var a = new ResourceLocation("minecraft", "air");
        var b = new ResourceLocation("minecraft", "air");
        
        // Act
        var result = a.Equals((object)b);
        
        // Assert
        Assert.True(result);
    }
    
    [Fact]
    public void EqualsObject_DifferentContent_ReturnsTrue()
    {
        // Arrange
        var a = new ResourceLocation("minecraft", "air");
        var b = new ResourceLocation("minecraft", "stone");
        
        // Act
        var result = a.Equals((object)b);
        
        // Assert
        Assert.False(result);
    }
    
    [Fact]
    public void EqualOperator_SameContent_ReturnsTrue()
    {
        // Arrange
        var a = new ResourceLocation("minecraft", "air");
        var b = new ResourceLocation("minecraft", "air");
        
        // Act
        var result = a == b;
        
        // Assert
        Assert.True(result);
    }
    
    [Fact]
    public void EqualOperator_DifferentContent_ReturnsFalse()
    {
        // Arrange
        var a = new ResourceLocation("minecraft", "air");
        var b = new ResourceLocation("minecraft", "stone");
        
        // Act
        var result = a == b;
        
        // Assert
        Assert.False(result);
    }
    
    [Fact]
    public void InequalOperator_DifferentContent_ReturnsTrue()
    {
        // Arrange
        var a = new ResourceLocation("minecraft", "air");
        var b = new ResourceLocation("minecraft", "stone");
        
        // Act
        var result = a != b;
        
        // Assert
        Assert.True(result);
    }
    
    [Fact]
    public void InequalOperator_SameContent_ReturnsFalse()
    {
        // Arrange
        var a = new ResourceLocation("minecraft", "air");
        var b = new ResourceLocation("minecraft", "air");
        
        // Act
        var result = a != b;
        
        // Assert
        Assert.False(result);
    }
}