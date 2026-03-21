// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

namespace MineJason.Tests.Client;

using MineJason.Data;

public class IntegralRangeTests
{
    [Fact]
    public void TryParse_MinOnly_ParseSuccessfully()
    {
        // Arrange
        const string s = "123..";

        // Act
        var success = IntegralRange.TryParse(s, out var result);

        // Assert
        Assert.True(success);
        Assert.Equal(new IntegralRange(123, null),
            result);
    }

    [Fact]
    public void TryParse_MaxOnly_ParseSuccessfully()
    {
        // Arrange
        const string s = "..321";

        // Act
        var success = IntegralRange.TryParse(s, out var result);

        // Assert
        Assert.True(success);
        Assert.Equal(new IntegralRange(null, 321),
            result);
    }

    [Fact]
    public void TryParse_MinMax_ParseSuccessfully()
    {
        // Arrange
        const string s = "123..321";

        // Act
        var success = IntegralRange.TryParse(s, out var result);

        // Assert
        Assert.True(success);
        Assert.Equal(new IntegralRange(123, 321),
            result);
    }

    [Fact]
    public void TryParse_Exact_ParseSuccessfully()
    {
        // Arrange
        const string s = "123321";

        // Act
        var success = IntegralRange.TryParse(s, out var result);

        // Assert
        Assert.True(success);
        Assert.Equal(new IntegralRange(123321),
            result);
    }

    [Fact]
    public void ToString_MinOnly_ConvertsCorrectly()
    {
        // Arrange
        var value = new IntegralRange(123, null);

        // Act
        var result = value.ToString();

        // Assert
        Assert.Equal("123..", result);
    }

    [Fact]
    public void ToString_MaxOnly_ConvertsCorrectly()
    {
        // Arrange
        var value = new IntegralRange(null, 321);

        // Act
        var result = value.ToString();

        // Assert
        Assert.Equal("..321", result);
    }

    [Fact]
    public void ToString_MinMax_ConvertsCorrectly()
    {
        // Arrange
        var value = new IntegralRange(123, 321);

        // Act
        var result = value.ToString();

        // Assert
        Assert.Equal("123..321", result);
    }

    [Fact]
    public void ToString_Exact_ConvertsCorrectly()
    {
        // Arrange
        var value = new IntegralRange(123321);

        // Act
        var result = value.ToString();

        // Assert
        Assert.Equal("123321", result);
    }
}
