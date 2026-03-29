// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

using System.Text.Json;
using MineJason.Events;
using MineJason.Tests.Client.Json;
using MineJason.Text.Behaviour.Click;

namespace MineJason.Tests.Client.Models;

public class ClickEventTests
{
    [Fact]
    public void ChangePageClickEvent_Serialize()
    {
        // Arrange
        var clickEvent = new ChangePageClickEvent(12);
        
        // Act
        var json = JsonSerializer.Serialize(clickEvent, JsonTestContext.Default.ClickEvent);
        
        // Assert
        Assert.Equal("{\"action\":\"change_page\",\"page\":12}", json);
    }
    
    [Fact]
    public void ChangePageClickEvent_Deserialize()
    {
        // Arrange
        const string json = "{\"action\":\"change_page\",\"page\":12}";
        
        // Act
        var result = JsonSerializer.Deserialize(json,
            JsonTestContext.Default.ClickEvent);
        
        // Assert
        Assert.Equal(new ChangePageClickEvent(12), result);
    }
    
    [Fact]
    public void CopyToClipboardClickEvent_Serialize()
    {
        // Arrange
        var clickEvent = new CopyToClipboardClickEvent("clipboard copy");
        
        // Act
        var json = JsonSerializer.Serialize(clickEvent, JsonTestContext.Default.ClickEvent);
        
        // Assert
        Assert.Equal("{\"action\":\"copy_to_clipboard\",\"value\":\"clipboard copy\"}", json);
    }
    
    [Fact]
    public void CopyToClipboardClickEvent_Deserialize()
    {
        // Arrange
        const string json = "{\"action\":\"copy_to_clipboard\",\"value\":\"clipboard copy\"}";
        
        // Act
        var result = JsonSerializer.Deserialize(json,
            JsonTestContext.Default.ClickEvent);
        
        // Assert
        Assert.Equal(new CopyToClipboardClickEvent("clipboard copy"), result);
    }
    
    [Fact]
    public void OpenUrlClickEvent_Serialize()
    {
        // Arrange
        var clickEvent = new OpenUrlClickEvent(new Uri("https://minecraft.net"));
        
        // Act
        var json = JsonSerializer.Serialize(clickEvent, JsonTestContext.Default.ClickEvent);
        
        // Assert
        Assert.Equal("{\"action\":\"open_url\",\"url\":\"https://minecraft.net/\"}", json);
    }
    
    [Fact]
    public void OpenUrlClickEvent_Deserialize()
    {
        // Arrange
        const string json = "{\"action\":\"open_url\",\"url\":\"https://minecraft.net\"}";
        
        // Act
        var result = JsonSerializer.Deserialize(json,
            JsonTestContext.Default.ClickEvent);
        
        // Assert
        Assert.Equal(new OpenUrlClickEvent(new Uri("https://minecraft.net")), result);
    }

    [Fact]
    public void RunCommandClickEvent_Serialize()
    {
        // Arrange
        var clickEvent = new RunCommandClickEvent("effect clear");
        
        // Act
        var json = JsonSerializer.Serialize(clickEvent, JsonTestContext.Default.ClickEvent);
        
        // Assert
        Assert.Equal("{\"action\":\"run_command\",\"command\":\"effect clear\"}", json);
    }
    
    [Fact]
    public void RunCommandClickEvent_Deserialize()
    {
        // Arrange
        const string json = "{\"action\":\"run_command\",\"command\":\"effect clear\"}";
        
        // Act
        var result = JsonSerializer.Deserialize(json,
            JsonTestContext.Default.ClickEvent);
        
        // Assert
        Assert.Equal(new RunCommandClickEvent("effect clear"), result);
    }
    
    [Fact]
    public void SuggestCommandClickEvent_Serialize()
    {
        // Arrange
        var clickEvent = new SuggestCommandClickEvent("effect clear");
        
        // Act
        var json = JsonSerializer.Serialize(clickEvent, JsonTestContext.Default.ClickEvent);
        
        // Assert
        Assert.Equal("{\"action\":\"suggest_command\",\"command\":\"effect clear\"}", json);
    }
    
    [Fact]
    public void SuggestCommandClickEvent_Deserialize()
    {
        // Arrange
        const string json = "{\"action\":\"suggest_command\",\"command\":\"effect clear\"}";
        
        // Act
        var result = JsonSerializer.Deserialize(json,
            JsonTestContext.Default.ClickEvent);
        
        // Assert
        Assert.Equal(new SuggestCommandClickEvent("effect clear"), result);
    }

    [Fact]
    public void CustomClickEvent_NoPayload_Serialize()
    {
        // Arrange
        var clickEvent = new CustomClickEvent(
            new ResourceLocation("foo", "bar"));

        // Act
        var json = JsonSerializer.Serialize(clickEvent, JsonTestContext.Default.ClickEvent);

        // Assert
        Assert.Equal("{\"action\":\"custom\",\"id\":\"foo:bar\"}", json);
    }


    [Fact]
    public void CustomClickEvent_NoPayload_Deserialize()
    {
        // Arrange
        const string json = "{\"action\":\"custom\",\"id\":\"foo:bar\"}";

        // Act
        var result = JsonSerializer.Deserialize(json,
            JsonTestContext.Default.ClickEvent);

        // Assert
        Assert.Equal(new CustomClickEvent(new ResourceLocation("foo", "bar")), result);
    }

    [Fact]
    public void CustomClickEvent_WithPayload_Serialize()
    {
        // Arrange
        var clickEvent = new CustomClickEvent(
            new ResourceLocation("foo", "data"),
            "bar");

        // Act
        var json = JsonSerializer.Serialize(clickEvent, JsonTestContext.Default.ClickEvent);

        // Assert
        Assert.Equal("{\"action\":\"custom\",\"id\":\"foo:data\",\"payload\":\"bar\"}", json);
    }


    [Fact]
    public void CustomClickEvent_WithPayload_Deserialize()
    {
        // Arrange
        const string json = "{\"action\":\"custom\",\"id\":\"foo:bar\",\"payload\":\"bar\"}";

        // Act
        var result = JsonSerializer.Deserialize(json,
            JsonTestContext.Default.ClickEvent);

        // Assert
        Assert.Equal(new CustomClickEvent(
            new ResourceLocation("foo", "bar"), "bar"), result);
    }
}