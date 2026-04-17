// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

using MineJason.Data;
using MineJason.Data.Selectors;

namespace MineJason.Tests.Client.Models;

public class EqualityTests
{
    [Fact]
    public void EntityNameMatchEquals_SameValues_True()
    {
        // Arrange
        var a = new EntityNameMatch("Sample", true);
        var b = new EntityNameMatch("Sample", true);
        
        // Act
        var result = a.Equals(b);
        
        // Assert
        Assert.True(result);
    }
    
    [Fact]
    public void EntityNameMatchEquals_DifferentValues_True()
    {
        // Arrange
        var a = new EntityNameMatch("Sample", true);
        var b = new EntityNameMatch("Something", false);
        
        // Act
        var result = a.Equals(b);
        
        // Assert
        Assert.False(result);
    }
    
    [Fact]
    public void GameModeMatchEquals_SameValues_True()
    {
        // Arrange
        var a = new GameModeMatch(GameMode.Creative);
        var b = new GameModeMatch(GameMode.Creative);
        
        // Act
        var result = a.Equals(b);
        
        // Assert
        Assert.True(result);
    }
    
    [Fact]
    public void GameModeMatchEquals_DifferentValues_True()
    {
        // Arrange
        var a = new GameModeMatch(GameMode.Creative);
        var b = new GameModeMatch(GameMode.Adventure);
        
        // Act
        var result = a.Equals(b);
        
        // Assert
        Assert.False(result);
    }
}