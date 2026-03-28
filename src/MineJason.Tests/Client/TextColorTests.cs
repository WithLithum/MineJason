// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

using System.Text.Json;
using MineJason.Serialization.TextJson;
using MineJason.Text.Colors;

namespace MineJason.Tests.Client;

public class TextColorTests
{
    [Fact]
    public void RgbParse_ValidColour_MatchingResult()
    {
        // Arrange
        const string color = "#00ff00";

        // Act
        var success = RgbTextColor.TryParse(color, out var result);

        // Assert
        Assert.True(success);
        Assert.Equal(new RgbTextColor(0x00, 0xff, 0x00), result);
    }

    [Fact]
    public void RgbJson_Serialize_MatchingJson()
    {
        // Arrange
        var color = new RgbTextColor(0x00, 0xff, 0xff);

        // Act
        var result = JsonSerializer.Serialize(color,
            MineJasonTextJsonContext.Default.RgbTextColor);

        // Result
        // ReSharper disable once StringLiteralTypo   
        Assert.Equal("\"#00ffff\"", result);
    }

    [Fact]
    public void RgbJson_SerializeAsInterface_MatchingJson()
    {
        // Arrange
        var color = new RgbTextColor(0x00, 0xff, 0xff);

        // Act
        var result = JsonSerializer.Serialize(color,
            MineJasonTextJsonContext.Default.ITextColor);

        // Result
        Assert.Equal("\"#00ffff\"", result);
    }

    [Fact]
    public void RgbJson_Deserialize_MatchingJson()
    {
        // Arrange
        // ReSharper disable once StringLiteralTypo   
        const string color = "\"#00ffff\"";

        // Act
        var result = JsonSerializer.Deserialize(color,
            MineJasonTextJsonContext.Default.RgbTextColor);

        // Assert
        Assert.Equal(new RgbTextColor(0x00, 0xff, 0xff), result);
    }

    [Fact]
    public void RgbJson_DeserializeAsInterface_MatchingJson()
    {
        // Arrange
        // ReSharper disable once StringLiteralTypo   
        const string color = "\"#00ffff\"";

        // Act
        var result = JsonSerializer.Deserialize(color,
            MineJasonTextJsonContext.Default.ITextColor);

        // Assert
        Assert.Equal(new RgbTextColor(0x00, 0xff, 0xff), result);
    }

    [Fact]
    public void FromRgb_ExceedsMaximum_Throws()
    {
        // Arrange
        const int value = 0xFFFFFFF;

        // Act
        var exception = Record.Exception(() => RgbTextColor.FromRgb(value));

        // Assert
        Assert.IsType<ArgumentOutOfRangeException>(exception);
    }

    [Fact]
    public void FromRgb_BelowMinimum_Throws()
    {
        // Arrange
        const int value = -123;

        // Act
        var exception = Record.Exception(() => RgbTextColor.FromRgb(value));

        // Assert
        Assert.IsType<ArgumentOutOfRangeException>(exception);
    }

    [Fact]
    public void FromRgb_ConformantValue_ReturnsMatching()
    {
        // Arrange
        const int value = 0xFE30A2;

        // Act
        var result = RgbTextColor.FromRgb(value);

        // Assert
        Assert.Multiple(() => Assert.Equal(0xFE, result.Color.R),
            () => Assert.Equal(0x30, result.Color.G),
            () => Assert.Equal(0xA2, result.Color.B));
    }
}