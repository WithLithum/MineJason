// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

using System.Text.Json;
using MineJason.Tests.Client.Json;
using MineJason.Text;
using MineJason.Text.Behaviour.Hover;

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
    public void ShowEntityHoverEvent_EqualsSameInstance_True()
    {
        // Arrange
        var a = new ShowEntityHoverEvent(
            new ResourceLocation("minecraft", "text"),
            new Guid("34514200-139B-463E-B59F-5D69EDB741E2"));

        // Act
        var result = a.Equals(a);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void ShowEntityHoverEvent_EqualsSameAsBase_True()
    {
        // Arrange
        var a = new ShowEntityHoverEvent(
            new ResourceLocation("minecraft", "text"),
            new Guid("34514200-139B-463E-B59F-5D69EDB741E2"));

        // Act
        var result = a.Equals((HoverEvent)a);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void ShowEntityHoverEvent_EqualsSameAsObject_True()
    {
        // Arrange
        var a = new ShowEntityHoverEvent(
            new ResourceLocation("minecraft", "text"),
            new Guid("34514200-139B-463E-B59F-5D69EDB741E2"));

        // Act
        var result = a.Equals((object)a);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void ShowEntityHoverEvent_EqualsSameContent_True()
    {
        // Arrange
        var a = new ShowEntityHoverEvent(
            new ResourceLocation("minecraft", "text"),
            new Guid("34514200-139B-463E-B59F-5D69EDB741E2"),
            TextComponent.CreateText("I Have A Name!"));
        var b = new ShowEntityHoverEvent(
            new ResourceLocation("minecraft", "text"),
            new Guid("34514200-139B-463E-B59F-5D69EDB741E2"),
            TextComponent.CreateText("I Have A Name!"));

        // Act
        var result = a.Equals(b);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void ShowEntityHoverEvent_EqualsDifferentContent_False()
    {
        // Arrange
        var a = new ShowEntityHoverEvent(
            new ResourceLocation("minecraft", "text"),
            new Guid("34514200-139B-363E-B59F-5D69EDB741E2"),
            TextComponent.CreateText("I Have A Name!"));
        var b = new ShowEntityHoverEvent(
            new ResourceLocation("minecraft", "text"),
            new Guid("34514200-139B-463E-B59F-5D69EDB741E2"),
            TextComponent.CreateText("I Have A Different Name!"));

        // Act
        var result = a.Equals(b);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void ShowEntityHoverEvent_EqualsNull_False()
    {
        // Arrange
        var a = new ShowEntityHoverEvent(
            new ResourceLocation("minecraft", "text"),
            new Guid("34514200-139B-363E-B59F-5D69EDB741E2"),
            TextComponent.CreateText("Compare Me To Null!"));

        // Act
        var result = a.Equals(null);

        // Assert
        Assert.False(result);
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

    [Fact]
    public void ShowTextHoverEvent_EqualsSameInstance_True()
    {
        // Arrange
        var a = new ShowTextHoverEvent(
            TextComponent.CreateText("Same Instance!"));

        // Act
        var result = a.Equals(a);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void ShowTextHoverEvent_EqualsSameAsBase_True()
    {
        // Arrange
        var a = new ShowTextHoverEvent(
            TextComponent.CreateText("Same Instance!"));

        // Act
        var result = a.Equals((HoverEvent)a);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void ShowTextHoverEvent_EqualsSameAsObject_True()
    {
        // Arrange
        var a = new ShowTextHoverEvent(
            TextComponent.CreateText("Same Instance!"));

        // Act
        var result = a.Equals((object)a);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void ShowTextHoverEvent_EqualsSameContent_True()
    {
        // Arrange
        var a = new ShowTextHoverEvent(TextComponent.CreateText("Me!"));
        var b = new ShowTextHoverEvent(TextComponent.CreateText("Me!"));

        // Act
        var result = a.Equals(b);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void ShowTextHoverEvent_EqualsDifferentContent_False()
    {
        // Arrange
        var a = new ShowTextHoverEvent(TextComponent.CreateText("Same Value!"));
        var b = new ShowTextHoverEvent(
            TextComponent.CreateText("Different Value!"));

        // Act
        var result = a.Equals(b);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void ShowTextHoverEvent_EqualsNull_False()
    {
        // Arrange
        var a = new ShowTextHoverEvent(TextComponent.CreateText("Same Value!"));

        // Act
        var result = a.Equals(null);

        // Assert
        Assert.False(result);
    }
}