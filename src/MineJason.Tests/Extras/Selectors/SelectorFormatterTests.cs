// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

using MineJason.Data;
using MineJason.Extras.Selectors;

namespace MineJason.Tests.Extras.Selectors;

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

    [Theory]
    [InlineData(EntitySelectorKind.AllPlayers, "@a", Label = "All players")]
    [InlineData(EntitySelectorKind.AllEntities, "@e", Label = "All entities")]
    [InlineData(EntitySelectorKind.NearestPlayer, "@p", Label = "Nearest player")]
    [InlineData(EntitySelectorKind.NearestEntity, "@n", Label = "Nearest entity")]
    [InlineData(EntitySelectorKind.RandomPlayer, "@r", Label = "Random player")]
    [InlineData(EntitySelectorKind.Executor, "@s", Label = "Self")]
    public void GetKindString_SpecifiedKind_ResultCorrect(EntitySelectorKind kind, string expected)
    {
        // Act
        var result = EntitySelectorStringFormatter.GetKindString(kind);

        // Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void GetKindString_InvalidKind_Throws()
    {
        // Arrange
        var input = (EntitySelectorKind)(-100);

        // Act
        var exception = Record.Exception(
            () => _ = EntitySelectorStringFormatter.GetKindString(input));

        // Assert
        Assert.IsType<ArgumentException>(exception);
    }

    [Theory]
    [InlineData(EntitySelectorKind.AllPlayers, "@a", Label = "All players")]
    [InlineData(EntitySelectorKind.AllEntities, "@e", Label = "All entities")]
    [InlineData(EntitySelectorKind.NearestPlayer, "@p", Label = "Nearest player")]
    [InlineData(EntitySelectorKind.NearestEntity, "@n", Label = "Nearest entity")]
    [InlineData(EntitySelectorKind.RandomPlayer, "@r", Label = "Random player")]
    [InlineData(EntitySelectorKind.Executor, "@s", Label = "Self")]
    public void ParseSelectorKind_SpecifiedKind_ResultCorrect(EntitySelectorKind expected, string value)
    {
        // Act
        var result = EntitySelectorStringFormatter.ParseSelectorKind(value);

        // Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void ParseSelectorKindInvalidValue_Throws()
    {
        // Arrange
        const string input = "Definitely not a selector kind";

        // Act
        var exception = Record.Exception(
            () => _ = EntitySelectorStringFormatter.ParseSelectorKind(input));

        // Assert
        Assert.IsType<FormatException>(exception);
    }

    [Theory]
    [InlineData(EntitySelectorSortMode.Arbitrary, "arbitrary")]
    [InlineData(EntitySelectorSortMode.Random, "random")]
    [InlineData(EntitySelectorSortMode.Nearest, "nearest")]
    [InlineData(EntitySelectorSortMode.Furthest, "furthest")]
    public void ParseSortMode_Specific_ResultCorrect(EntitySelectorSortMode expected, string value)
    {
        // Act
        var result = EntitySelectorStringFormatter.ParseSortMode(value);

        // Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void ParseSortMode_InvalidValue_Throws()
    {
        // Arrange
        const string input = "Definitely not a sort mode";

        // Act
        var exception = Record.Exception(
            () => _ = EntitySelectorStringFormatter.ParseSortMode(input));

        // Assert
        Assert.IsType<FormatException>(exception);
    }

    [Theory]
    [InlineData(GameMode.Survival, "survival")]
    [InlineData(GameMode.Creative, "creative")]
    [InlineData(GameMode.Adventure, "adventure")]
    [InlineData(GameMode.Spectator, "spectator")]
    public void ParseGameMode_Specific_ResultCorrect(GameMode expected, string value)
    {
        // Act
        var result = EntitySelectorStringFormatter.ParseGameMode(value);

        // Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void ParseGameMode_InvalidValue_Throws()
    {
        // Arrange
        const string input = "Definitely not a game mode";

        // Act
        var exception = Record.Exception(
            () => _ = EntitySelectorStringFormatter.ParseGameMode(input));

        // Assert
        Assert.IsType<FormatException>(exception);
    }

    [Fact]
    public void ParseDistanceRange_TooManyValues_Throws()
    {
        // Arrange
        const string input = "10..20..30";

        // Act
        var exception = Record.Exception(
            () => _ = EntitySelectorStringFormatter.ParseDistanceRange(input));

        // Assert
        Assert.IsType<FormatException>(exception);
    }

    [Fact]
    public void ParseDistanceRange_MinimumOnly_ReturnsCorrect()
    {
        // Arrange
        const string input = "10..";

        // Act
        var result = EntitySelectorStringFormatter.ParseDistanceRange(input);

        // Assert
        Assert.Equal(new DistanceRange(10, null), result);
    }

    [Fact]
    public void ParseDistanceRange_MaximumOnly_ReturnsCorrect()
    {
        // Arrange
        const string input = "..50";

        // Act
        var result = EntitySelectorStringFormatter.ParseDistanceRange(input);

        // Assert
        Assert.Equal(new DistanceRange(null, 50), result);
    }

    [Fact]
    public void ParseDistanceRange_Exact_ReturnsCorrect()
    {
        // Arrange
        const string input = "150";

        // Act
        var result = EntitySelectorStringFormatter.ParseDistanceRange(input);

        // Assert
        Assert.Equal(new DistanceRange(150), result);
    }
}
