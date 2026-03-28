// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

namespace MineJason.Tests.Client;

using System.Text.Json;
using MineJason.Data;
using MineJason.Serialization.TextJson;
using MineJason.Text;

public class ContextSerializationTests
{
    [Fact]
    public void TextComponent_ContextSerialize()
    {
        // Arrange
        var component = TextComponent.CreateText("text");

        // Act
        var json = JsonSerializer.Serialize(component,
            MineJasonTextJsonContext.Default.ChatComponent);

        // Assert
        Assert.Equal("{\"type\":\"text\",\"text\":\"text\"}", json);
    }

    [Fact]
    public void TranslatableComponent_ContextSerialize()
    {
        // Arrange
        var component = TextComponent.CreateTranslatable("its_me");

        // Act
        var json = JsonSerializer.Serialize(component,
            MineJasonTextJsonContext.Default.ChatComponent);

        // Assert
        Assert.Equal("{\"type\":\"translatable\",\"translate\":\"its_me\"}", json);
    }

    [Fact]
    public void EntityComponent_ContextSerialize()
    {
        // Arrange
        var component = TextComponent.CreateSelector(new EntitySelector(EntitySelectorKind.Executor));

        // Act
        var json = JsonSerializer.Serialize(component,
            MineJasonTextJsonContext.Default.ChatComponent);

        // Assert
        Assert.Equal("{\"type\":\"selector\",\"selector\":\"@s\"}", json);
    }

    [Fact]
    public void AtlasObjectComponent_ContextSerialize()
    {
        // Arrange
        var component = TextComponent.CreateAtlasObject(new ResourceLocation("foo", "bar"));

        // Act
        var json = JsonSerializer.Serialize(component,
            MineJasonTextJsonContext.Default.ChatComponent);

        // Assert
        Assert.Equal("{\"type\":\"object\",\"object\":\"atlas\",\"sprite\":\"foo:bar\"}", json);
    }
}