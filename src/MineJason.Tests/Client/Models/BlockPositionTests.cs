// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

namespace MineJason.Tests.Client.Models;

using MineJason.Data.Coordinates;
using Xunit;

public class BlockPositionTests
{
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
}
