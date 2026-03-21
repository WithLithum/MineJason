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
        var success = TextColor.TryParse(color, out var result);
        
        // Assert
        Assert.True(success);
        Assert.Equal(new TextColor(0x00, 0xff, 0x00), result);
    }

    [Fact]
    public void TextColor_Serialize()
    {
        // Arrange
        var color = new TextColor(0x00, 0xff, 0xff);
        
        // Act
        var result = JsonSerializer.Serialize(color,
            MineJasonTextJsonContext.Default.TextColor);
        
        // Result
        // ReSharper disable once StringLiteralTypo   
        Assert.Equal("\"#00ffff\"", result);
    }
    
    [Fact]
    public void TextColor_SerializeAsChatColor()
    {
        // Arrange
        var color = new TextColor(0x00, 0xff, 0xff);
        
        // Act
        var result = JsonSerializer.Serialize(color,
            MineJasonTextJsonContext.Default.IChatColor);
        
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
            MineJasonTextJsonContext.Default.TextColor);
        
        // Assert
        Assert.Equal(new TextColor(0x00, 0xff, 0xff), result);
    }
    
    [Fact]
    public void TextColor_DeserializeAsChatColor()
    {
        // Arrange
        // ReSharper disable once StringLiteralTypo   
        const string color = "\"#00ffff\"";
        
        // Act
        var result = JsonSerializer.Deserialize(color,
            MineJasonTextJsonContext.Default.IChatColor);
        
        // Assert
        Assert.Equal(new TextColor(0x00, 0xff, 0xff), result);
    }
}