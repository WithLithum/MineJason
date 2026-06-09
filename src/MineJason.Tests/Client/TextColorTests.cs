// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

using System.Drawing;
using System.Text.Json;
using MineJason.Serialization.IO.Json;
using MineJason.Serialization.Schema;
using MineJason.Tests.Client.Json;
using MineJason.Tests.Serialization.Utilities;
using MineJason.Text.Colors;

namespace MineJason.Tests.Client;

public class TextColorTests
{
    [Fact]
    public void FromColorCode_Known_ReturnsCorrect()
    {
        // Arrange
        const char input = '6';

        // Act
        var result = TextColor.FromColorCode(input);

        // Assert
        Assert.Equal(NamedTextColor.Gold, result);
    }

    [Fact]
    public void FromColorCode_Unrecognised_Throws()
    {
        // Arrange
        const char input = 'g';

        // Act
        var exception = Record.Exception(() => TextColor.FromColorCode(input));

        // Assert
        Assert.IsType<ArgumentException>(exception);
    }

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

    [Theory]
    [InlineData("#aaaaaaa", Label = "Invalid length")]
    [InlineData("#INVALD", Label = "Malformed")]
    public void RgbParse_InvalidColour_Fail(string input)
    {
        // Act
        var success = RgbTextColor.TryParse(input, out _);

        // Assert
        Assert.False(success);
    }

    [Fact]
    public void RgbJson_Serialize_MatchingJson()
    {
        // Arrange
        var color = new RgbTextColor(0x00, 0xff, 0xff);

        // Act
        var result = JsonSerializer.Serialize(color,
            JsonTestContext.Default.RgbTextColor);

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
            JsonTestContext.Default.ITextColor);

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
            JsonTestContext.Default.RgbTextColor);

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
            JsonTestContext.Default.ITextColor);

        // Assert
        Assert.Equal(new RgbTextColor(0x00, 0xff, 0xff), result);
    }

    [Fact]
    public void ColorEqualsDrawingColor_SameValue_True()
    {
        // Arrange
        var drawingColor = Color.Red;
        var rgbColor = new RgbTextColor(Color.Red);

        // Act
        var result = rgbColor.Equals(drawingColor);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void RgbEqualsDrawingColor_SameValue_True()
    {
        // Arrange
        var drawingColor = Color.Red;
        var rgbColor = new RgbTextColor(0xFF, 0x00, 0x00);

        // Act
        var result = rgbColor.Equals(drawingColor);

        // Assert
        Assert.True(result);
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

    [Fact]
    public void TextColorSchema_DecodeKnown_Success()
    {
        // Arrange
        const string input = "\"red\"";
        var element = JsonElement.Parse(input);
        var decoder = new JsonElementDecoder();

        // Act
        var result = TextColorSchema.Instance.Decode(element, decoder);

        // Assert
        var color = ResultAssert.Success(result);
        Assert.Equal("red", Assert.IsType<NamedTextColor>(color).GenerateColorText());
    }

    [Fact]
    public void TextColorSchema_Rgb_Success()
    {
        // Arrange
        const string input = "\"#ff0000\"";
        var element = JsonElement.Parse(input);
        var decoder = new JsonElementDecoder();

        // Act
        var result = TextColorSchema.Instance.Decode(element, decoder);

        // Assert
        var color = ResultAssert.Success(result);
        Assert.Equal("#ff0000", Assert.IsType<RgbTextColor>(color).GenerateColorText());
    }

    [Theory]
    [InlineData("123", Label = "Not string")]
    [InlineData("\"\"", Label = "Empty")]
    [InlineData("\"unknown_color\"", Label = "Invalid color name")]
    [InlineData("\"#aaaaaaa\"", Label = "Too long RGB triplet")]
    [InlineData("\"#INVALD\"", Label = "Malformed RGB triplet")]
    public void TextColorSchema_Invalid_Failure(string input)
    {
        // Arrange
        var element = JsonElement.Parse(input);
        var decoder = new JsonElementDecoder();

        // Act
        var result = TextColorSchema.Instance.Decode(element, decoder);

        // Assert
        ResultAssert.Failure(result);
    }
}