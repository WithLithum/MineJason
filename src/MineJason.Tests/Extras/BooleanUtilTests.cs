// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

using MineJason.Extras.Helpers;

namespace MineJason.Tests.Extras;

public class BooleanUtilTests
{
    [Fact]
    public void TryParseLowerBooleanString_LowercaseTrue_ReturnsTrue()
    {
        // Arrange
        const string input = "true";

        // Act
        var success = BooleanUtil.TryParseLowerBoolean(input, out var value);

        // Assert
        Assert.True(success);
        Assert.True(value);
    }

    [Fact]
    public void TryParseLowerBooleanString_LowercaseFalse_ReturnsFalse()
    {
        // Arrange
        const string input = "false";

        // Act
        var success = BooleanUtil.TryParseLowerBoolean(input, out var value);

        // Assert
        Assert.True(success);
        Assert.False(value);
    }

    [Fact]
    public void TryParseLowerBooleanString_SomethingElse_ReturnsFalse()
    {
        // Arrange
        const string input = "notabool";

        // Act
        var success = BooleanUtil.TryParseLowerBoolean(input, out _);

        // Assert
        Assert.False(success);
    }

    [Theory]
    [InlineData(true, "true")]
    [InlineData(false, "false")]
    public void ToLowerBooleanString_GivenInput_CorrespondingResult(bool input, string expected)
    {
        // Act
        var result = BooleanUtil.ToLowerBooleanString(input);

        // Assert
        Assert.Equal(expected, result);
    }
}
