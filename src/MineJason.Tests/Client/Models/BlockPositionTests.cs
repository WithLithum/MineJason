// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

namespace MineJason.Tests.Client.Models;

using MineJason.Data.Coordinates;
using Xunit;

public class BlockPositionTests
{
    [Fact]
    public void PositionParse_TooFewComponents_Throw()
    {
        // Arrange
        const string input = "12 34";
        
        // Act
        var exception = Record.Exception(() => BlockPosition.Parse(input));
        
        // Assert
        Assert.IsType<ArgumentException>(exception);
    }
    
    [Fact]
    public void PositionParse_TooManyComponents_Throw()
    {
        // Arrange
        const string input = "12 34 56 78";
        
        // Act
        var exception = Record.Exception(() => BlockPosition.Parse(input));
        
        // Assert
        Assert.IsType<ArgumentException>(exception);
    }
    
    [Fact]
    public void PositionParse_WellFormed_ReturnsCorrect()
    {
        // Arrange
        const string input = "12 34 56";
        
        // Act
        var result = BlockPosition.Parse(input);
        
        // Assert
        Assert.Equal(new BlockPosition(12, 34, 56), result);
    }
    
    [Fact]
    public void ComponentParse_Absolute_ReturnsCorrect()
    {
        // Arrange
        const string input = "127";
        
        // Act
        var result = BlockPositionComponent.Parse(input);
        
        // Assert
        Assert.Equal(new BlockPositionComponent(127), result);
    }
    
    [Fact]
    public void ComponentParse_Relative_ReturnsCorrect()
    {
        // Arrange
        const string input = "~320";
        
        // Act
        var result = BlockPositionComponent.Parse(input);
        
        // Assert
        Assert.Equal(new BlockPositionComponent(320, BlockPositionComponentType.Relative),
            result);
    }
    
    [Fact]
    public void ComponentParse_Local_ReturnsCorrect()
    {
        // Arrange
        const string input = "^256";
        
        // Act
        var result = BlockPositionComponent.Parse(input);
        
        // Assert
        Assert.Equal(new BlockPositionComponent(256, BlockPositionComponentType.Local),
            result);
    }
    
    [Fact]
    public void BlockPosition_IsValid_AllAbsolute()
    {
        // Arrange
        var pos = new BlockPosition(123, 456, 789);
        
        // Act
        var isValid = pos.IsValid();
        
        // Assert
        Assert.True(isValid);
    }

    [Fact]
    public void BlockPosition_IsValid_AllRelative()
    {
        // Arrange
        var pos = new BlockPosition(123, 456, 789, BlockPositionComponentType.Relative);
        
        // Act
        var isValid = pos.IsValid();
        
        // Assert
        Assert.True(isValid);
    }

    [Fact]
    public void BlockPosition_IsValid_AllLocal()
    {
        // Arrange
        var pos = new BlockPosition(123, 456, 789, BlockPositionComponentType.Local);
        
        // Act
        var isValid = pos.IsValid();
        
        // Assert
        Assert.True(isValid);
    }

    [Fact]
    public void BlockPosition_IsValid_MixedAbsoluteAndRelative()
    {
        // Arrange
        // The below coordinate is "331 ~ 115"
        var pos = new BlockPosition(new BlockPositionComponent(331),
            new BlockPositionComponent(0, BlockPositionComponentType.Relative),
            new BlockPositionComponent(115, BlockPositionComponentType.Relative));
        
        // Act
        var isValid = pos.IsValid();
        
        // Assert
        Assert.True(isValid);
    }

    [Fact]
    public void BlockPosition_IsValid_MixedAbsoluteRelativeAndLocal()
    {
        // Arrange
        // The below coordinate is "123 ~ ^2"
        var pos = new BlockPosition(new BlockPositionComponent(123),
            new BlockPositionComponent(0, BlockPositionComponentType.Relative),
            new BlockPositionComponent(2, BlockPositionComponentType.Local));
        
        // Act
        var isValid = pos.IsValid();
        
        // Assert
        Assert.False(isValid);
    }

    [Fact]
    public void BlockPositionComponent_ToString_Absolute()
    {
        // Arrange
        var component = new BlockPositionComponent(255,
            type: BlockPositionComponentType.Absolute);
        
        // Act
        var result = component.ToString();
        
        // Assert
        Assert.Equal("255", result);
    }

    [Fact]
    public void BlockPositionComponent_ToString_Local()
    {
        // Arrange
        var component = new BlockPositionComponent(233,
            type: BlockPositionComponentType.Local);
        
        // Act
        var result = component.ToString();
        
        // Assert
        Assert.Equal("^233", result);
    }

    [Fact]
    public void BlockPositionComponent_ToString_Relative()
    {
        // Arrange
        var component = new BlockPositionComponent(123,
            type: BlockPositionComponentType.Relative);
        
        // Act
        var result = component.ToString();
        
        // Assert
        Assert.Equal("~123", result);
    }
    
    [Fact]
    public void Component_AddComponent_Correct()
    {
        // Arrange
        var left = new BlockPositionComponent(100);
        var right = new BlockPositionComponent(23);
        
        // Act
        var result = left + right;
        
        // Assert
        Assert.Equal(new BlockPositionComponent(123), result);
    }
    
    [Fact]
    public void Component_AddNumber_Correct()
    {
        // Arrange
        var left = new BlockPositionComponent(100);
        const int right = 23;
        
        // Act
        var result = left + right;
        
        // Assert
        Assert.Equal(new BlockPositionComponent(123), result);
    }
    
    [Fact]
    public void Component_SubtractComponent_Correct()
    {
        // Arrange
        var left = new BlockPositionComponent(123);
        var right = new BlockPositionComponent(23);
        
        // Act
        var result = left - right;
        
        // Assert
        Assert.Equal(new BlockPositionComponent(100), result);
    }
    
    [Fact]
    public void Component_SubtractNumber_Correct()
    {
        // Arrange
        var left = new BlockPositionComponent(123);
        const int right = 23;
        
        // Act
        var result = left - right;
        
        // Assert
        Assert.Equal(new BlockPositionComponent(100), result);
    }
    
    [Fact]
    public void Component_MultiplyComponent_Correct()
    {
        // Arrange
        var left = new BlockPositionComponent(10);
        var right = new BlockPositionComponent(10);
        
        // Act
        var result = left * right;
        
        // Assert
        Assert.Equal(new BlockPositionComponent(100), result);
    }
    
    [Fact]
    public void Component_MultiplyNumber_Correct()
    {
        // Arrange
        var left = new BlockPositionComponent(10);
        const int right = 10;
        
        // Act
        var result = left * right;
        
        // Assert
        Assert.Equal(new BlockPositionComponent(100), result);
    }

    [Fact]
    public void Component_DivideComponent_Correct()
    {
        // Arrange
        var left = new BlockPositionComponent(1000);
        var right = new BlockPositionComponent(2);
        
        // Act
        var result = left / right;
        
        // Assert
        Assert.Equal(new BlockPositionComponent(500), result);
    }
    
    [Fact]
    public void Component_DivideNumber_Correct()
    {
        // Arrange
        var left = new BlockPositionComponent(1000);
        const int right = 2;
        
        // Act
        var result = left / right;
        
        // Assert
        Assert.Equal(new BlockPositionComponent(500), result);
    }
    
    [Fact]
    public void ComponentEquality_Same_True()
    {
        // Arrange
        var left = new BlockPositionComponent(1000);
        var right = new BlockPositionComponent(1000);
        
        // Act
        var result = left == right;
        
        // Assert
        Assert.True(result);
    }
    
    [Fact]
    public void ComponentEquality_Different_False()
    {
        // Arrange
        var left = new BlockPositionComponent(1000);
        var right = new BlockPositionComponent(555);
        
        // Act
        var result = left == right;
        
        // Assert
        Assert.False(result);
    }
    
    [Fact]
    public void ComponentEquality_DifferentKind_False()
    {
        // Arrange
        var left = new BlockPositionComponent(1000);
        var right = new BlockPositionComponent(1000, BlockPositionComponentType.Local);
        
        // Act
        var result = left == right;
        
        // Assert
        Assert.False(result);
    }
    
    [Fact]
    public void ComponentInequality_Same_Correct()
    {
        // Arrange
        var left = new BlockPositionComponent(1000);
        var right = new BlockPositionComponent(1000);
        
        // Act
        var result = left != right;
        
        // Assert
        Assert.False(result);
    }
    
    [Fact]
    public void ComponentInequality_Different_True()
    {
        // Arrange
        var left = new BlockPositionComponent(1000);
        var right = new BlockPositionComponent(555);
        
        // Act
        var result = left != right;
        
        // Assert
        Assert.True(result);
    }
    
    [Fact]
    public void ComponentInequality_DifferentKind_True()
    {
        // Arrange
        var left = new BlockPositionComponent(1000);
        var right = new BlockPositionComponent(1000, BlockPositionComponentType.Relative);
        
        // Act
        var result = left != right;
        
        // Assert
        Assert.True(result);
    }

    [Fact]
    public void ComponentHashCode_Same_Equals()
    {
        // Arrange
        var c1 = new BlockPositionComponent(1000);
        var c2 = new BlockPositionComponent(1000);
        
        // Act
        var hash1 = c1.GetHashCode();
        var hash2 = c2.GetHashCode();
        
        // Assert
        Assert.Equal(hash1, hash2);
    }
    
    [Fact]
    public void ComponentHashCode_Different_NotEquals()
    {
        // Arrange
        var c1 = new BlockPositionComponent(1000);
        var c2 = new BlockPositionComponent(2000);
        
        // Act
        var hash1 = c1.GetHashCode();
        var hash2 = c2.GetHashCode();
        
        // Assert
        Assert.NotEqual(hash1, hash2);
    }
}
