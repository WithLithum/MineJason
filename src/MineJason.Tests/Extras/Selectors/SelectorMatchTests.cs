// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

using MineJason.Extras.Selectors.Matching;

namespace MineJason.Tests.Extras.Selectors;

public class SelectorMatchTests
{
    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void NameInversion_GivenValue_ReturnInversed(bool input)
    {
        // Arrange
        var match = new EntityNameMatch("test", input);

        // Act
        var result = !match;

        // Assert
        Assert.Equal(!input, result.Value);
    }

    [Fact]
    public void ScoreboardExactEquals_Same_ReturnsTrue()
    {
        // Arrange
        var a = new ScoreboardExactMatch("objective", 10);
        var b = new ScoreboardExactMatch("objective", 10);

        // Act
        var result = a.Equals(b);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void ScoreboardExactEquals_DifferentObjectives_ReturnsFalse()
    {
        // Arrange
        var a = new ScoreboardExactMatch("objective1", 10);
        var b = new ScoreboardExactMatch("objective2", 10);

        // Act
        var result = a.Equals(b);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void ScoreboardExactEquals_DifferentValues_ReturnsFalse()
    {
        // Arrange
        var a = new ScoreboardExactMatch("objective", 10);
        var b = new ScoreboardExactMatch("objective", 20);

        // Act
        var result = a.Equals(b);

        // Assert
        Assert.False(result);
    }
}
