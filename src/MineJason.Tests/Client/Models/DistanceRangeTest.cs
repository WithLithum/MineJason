// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

using MineJason.Data;

namespace MineJason.Tests.Client.Models;

public class DistanceRangeTest
{
    [Fact]
    public void ToString_Ranged_WritesRanged()
    {
        // Arrange
        var range = new DistanceRange(20, 30);

        // Act
        var result = range.ToString();

        // Assert
        Assert.Equal("20..30", result);
    }

    [Fact]
    public void ToString_Exact_WritesExact()
    {
        // Arrange
        var range = new DistanceRange(10);

        // Act
        var result = range.ToString();

        // Assert
        Assert.Equal("10", result);
    }
}
