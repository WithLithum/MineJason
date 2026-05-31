// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

using MineJason.Data;
using MineJason.Extras.Selectors;
using MineJason.Extras.Selectors.Matching;

namespace MineJason.Tests.Extras.Selectors;

public class EqualityTests
{
    [Fact]
    public void EntitySelectorEquals_SameValues_True()
    {
        // Arrange
        var a = EntitySelectorStringFormatter.ParseSelector(
            "@a[x=100,y=50,z=-100,dx=15,dy=2,dz=12]");
        var b = EntitySelectorStringFormatter.ParseSelector(
            "@a[x=100,y=50,z=-100,dx=15,dy=2,dz=12]");

        // Act
        var result = a.Equals(b);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void EntitySelectorEquals_SameValueDifferentForm_True()
    {
        // Arrange
        var a = EntitySelectorStringFormatter.ParseSelector(
            "@a[x=100,y=50,z=-100,dx=15,dy=2,dz=12]");
        var b = EntitySelectorStringFormatter.ParseSelector(
            "@a[x=100,z=-100,y=50,dy=2,dx=15,dz=12]");

        // Act
        var result = a.Equals(b);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void EntitySelectorEquals_DifferentValues_False()
    {
        // Arrange
        var a = EntitySelectorStringFormatter.ParseSelector(
            "@a[x=100,y=50,z=-100,dx=15,dy=2,dz=12]");
        var b = EntitySelectorStringFormatter.ParseSelector(
            "@p");

        // Act
        var result = a.Equals(b);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void EntitySelectorEquals_Null_False()
    {
        // Arrange
        var a = EntitySelectorStringFormatter.ParseSelector(
            "@a[x=100,y=50,z=-100,dx=15,dy=2,dz=12]");

        // Act
        var result = a.Equals(null);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void EntitySelectorEqualsObject_DifferentType_False()
    {
        // Arrange
        var a = EntitySelectorStringFormatter.ParseSelector(
            "@a[x=100,y=50,z=-100,dx=15,dy=2,dz=12]");
        var b = "string";

        // Act
        var result = a.Equals(b);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void EntitySelectorEqualsObject_SameValues_True()
    {
        // Arrange
        var a = EntitySelectorStringFormatter.ParseSelector(
            "@a[x=100,y=50,z=-100,dx=15,dy=2,dz=12]");

        // Act
        var result = a.Equals((object)a);

        // Assert
        Assert.True(result);
    }

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