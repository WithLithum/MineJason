// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

namespace MineJason.Tests.Client.Selectors;

using MineJason.Data;
using MineJason.Data.Selectors;

public class SelectorFormatterTests
{
    [Fact]
    public void ParseSelectorKind_AllPlayers()
    {
        // Arrange
        var parse = "@a";

        // Act
        var result = EntitySelectorStringFormatter.ParseSelectorKind(parse);

        // Assert
        Assert.Equal(EntitySelectorKind.AllPlayers, result);
    }

    [Fact]
    public void ParseSelectorKind_NearestPlayer()
    {
        // Arrange
        var parse = "@p";

        // Act
        var result = EntitySelectorStringFormatter.ParseSelectorKind(parse);

        // Assert
        Assert.Equal(EntitySelectorKind.NearestPlayer, result);
    }

    [Fact]
    public void ParseSelectorKind_RandomPlayer()
    {
        // Arrange
        var parse = "@r";

        // Act
        var result = EntitySelectorStringFormatter.ParseSelectorKind(parse);

        // Assert
        Assert.Equal(EntitySelectorKind.RandomPlayer, result);
    }
    
    [Fact]
    public void ParseSelectorKind_AllEntities()
    {
        // Arrange
        var parse = "@e";

        // Act
        var result = EntitySelectorStringFormatter.ParseSelectorKind(parse);

        // Assert
        Assert.Equal(EntitySelectorKind.AllEntities, result);
    }

    [Fact]
    public void ParseSelectorKind_Executor()
    {
        // Arrange
        var parse = "@s";

        // Act
        var result = EntitySelectorStringFormatter.ParseSelectorKind(parse);

        // Assert
        Assert.Equal(EntitySelectorKind.Executor, result);
    }
    
    [Fact]
    public void ParseSelectorKind_NearestEntity()
    {
        // Arrange
        var parse = "@n";

        // Act
        var result = EntitySelectorStringFormatter.ParseSelectorKind(parse);

        // Assert
        Assert.Equal(EntitySelectorKind.NearestEntity, result);
    }
}
