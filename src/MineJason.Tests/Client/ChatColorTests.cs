// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

namespace MineJason.Tests.Client;

using System.Text.Json;
using MineJason.Serialization.TextJson;
using MineJason.Text.Colors;

public class ChatColorTests
{
    [Fact]
    public void TextColor_Parse()
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
    public void TextColor_Serialize()
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
    public void TextColor_SerializeAsChatColor()
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
    public void TextColor_Deserialize()
    {
        // Arrange
        // ReSharper disable once StringLiteralTypo   
        const string color = "\"#00ffff\"";
        
        // Act
        var result = JsonSerializer.Deserialize(color,
            MineJasonTextJsonContext.Default.RgbTextColor);

        // Assert
        Assert.Equal(new RgbTextColor(0x00, 0xff, 0xff), (RgbTextColor?)result);
    }
    
    [Fact]
    public void TextColor_DeserializeAsChatColor()
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
}