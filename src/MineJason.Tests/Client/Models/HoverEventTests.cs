// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

using System.Text.Json;
using MineJason.Events.Hover;
using MineJason.Tests.Client.Json;
using MineJason.Text;

namespace MineJason.Tests.Client.Models;

public class HoverEventTests
{
    [Fact]
    public void ShowEntityHoverEvent_Serialize()
    {
        // Arrange
        var e = new ShowEntityHoverEvent(new ResourceLocation("minecraft", "pig"),
            new Guid("34514200-139B-463E-B59F-5D69EDB741E2"));
        const string expected =
            "{\"action\":\"show_entity\",\"id\":\"minecraft:pig\",\"uuid\":[877740544,328943166,-1247847063,-306757150]}";

        // Act
        var json = JsonSerializer.Serialize(e,
            JsonTestContext.Default.HoverEvent);

        // Assert
        Assert.Equal(expected,
            json);
    }

    [Fact]
    public void ShowEntityHoverEvent_Deserialize()
    {
        // Arrange
        const string json = "{\"action\":\"show_entity\",\"id\":\"minecraft:pig\",\"uuid\":\"34514200-139b-463e-b59f-5d69edb741e2\"}";
        var expected = new ShowEntityHoverEvent(new ResourceLocation("minecraft", "pig"),
            new Guid("34514200-139B-463E-B59F-5D69EDB741E2"));

        // Act
        var result = JsonSerializer.Deserialize(json,
            JsonTestContext.Default.HoverEvent);

        // Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void ShowTextHoverEvent_Serialize()
    {
        // Arrange
        const string json = "{\"action\":\"show_text\",\"value\":{\"type\":\"text\",\"text\":\"Hello World!\"}}";
        var value = new ShowTextHoverEvent(TextComponent.CreateText("Hello World!"));

        // Act
        var result = JsonSerializer.Serialize(value,
            JsonTestContext.Default.HoverEvent);

        // Assert
        Assert.Equal(json, result);
    }

    [Fact]
    public void ShowTextHoverEvent_Deserialize()
    {
        // Arrange
        const string json = "{\"action\":\"show_text\",\"value\":{\"type\":\"text\",\"text\":\"Hello World!\"}}";
        var expected = new ShowTextHoverEvent(TextComponent.CreateText("Hello World!"));

        // Act
        var result = JsonSerializer.Deserialize(json,
            JsonTestContext.Default.HoverEvent);

        // Assert
        Assert.Equal(expected, result);
    }
}