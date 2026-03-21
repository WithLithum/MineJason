// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

namespace MineJason.Tests.Client;

using System.Text.Json;
using MineJason.Data;
using MineJason.Data.Coordinates;
using MineJason.Serialization.TextJson;
using MineJason.Text.Colors;

public class SimpleSerializationTests
{
    [Fact]
    public void TextComponent_Serialize()
    {
        // Arrange
        var component = ChatComponent.CreateText("I am text");

        // Act
        var json = JsonSerializer.Serialize(component,
            MineJasonTextJsonContext.Default.ChatComponent);

        // Assert
        Assert.Equal("{\"type\":\"text\",\"text\":\"I am text\"}", json);
    }

    [Fact]
    public void TextComponent_Deserialize()
    {
        // Arrange
        const string json = "{\"text\":\"Hello World!\"}";

        // Act
        var deserialized = JsonSerializer.Deserialize<ChatComponent>(json,
            MineJasonTextJsonContext.Default.ChatComponent);

        // Assert
        Assert.Equal(ChatComponent.CreateText("Hello World!"), deserialized);
    }

    [Fact]
    public void TextComponent_Deserialize_RawString()
    {
        // Arrange
        const string json = "\"Hello World!\"";

        // Act
        var deserialized = JsonSerializer.Deserialize(json,
            MineJasonTextJsonContext.Default.ChatComponent);

        // Assert
        Assert.Equal(ChatComponent.CreateText("Hello World!"), deserialized);
    }

    [Fact]
    public void TranslatableComponent_Serialize()
    {
        // Arrange
        var component = ChatComponent.CreateTranslatable("translatable.key");

        // Act
        var json = JsonSerializer.Serialize(component,
            MineJasonTextJsonContext.Default.ChatComponent);

        // Assert
        Assert.Equal("{\"type\":\"translatable\",\"translate\":\"translatable.key\"}", json);
    }

    [Fact]
    public void TranslatableComponent_Deserialize()
    {
        // Arrange
        const string json = "{\"translate\":\"translatable.key\"}";

        // Act
        var deserialized = JsonSerializer.Deserialize(json,
            MineJasonTextJsonContext.Default.ChatComponent);

        // Assert
        var exp = ChatComponent.CreateTranslatable("translatable.key");
        Assert.Equal(exp, deserialized);
    }

    [Fact]
    public void ScoreboardComponent_Serialize()
    {
        // Arrange
        var component = ChatComponent.CreateScore("Player", "advancements");

        // Act
        var json = JsonSerializer.Serialize(component,
            MineJasonTextJsonContext.Default.ChatComponent);

        // Assert
        Assert.Equal("{\"type\":\"score\",\"score\":{\"name\":\"Player\",\"objective\":\"advancements\"}}", json);
    }

    [Fact]
    public void ScoreboardComponent_Deserialize()
    {
        // Arrange
        const string json = "{\"score\":{\"name\":\"Player\",\"objective\":\"advancements\"}}";

        // Act
        var deserialized = JsonSerializer.Deserialize(json,
            MineJasonTextJsonContext.Default.ChatComponent);

        // Assert
        Assert.Equal(ChatComponent.CreateScore("Player", "advancements"), deserialized);
    }

    [Fact]
    public void EntityComponent_Serialize()
    {
        // Arrange
        var component = ChatComponent.CreateSelector(new EntitySelector(EntitySelectorKind.AllPlayers), ChatComponent.CreateText(";"));

        // Act
        var json = JsonSerializer.Serialize(component,
            MineJasonTextJsonContext.Default.ChatComponent);

        // Assert
        Assert.Equal("{\"type\":\"selector\",\"selector\":\"@a\",\"separator\":{\"type\":\"text\",\"text\":\";\"}}", json);
    }


    [Fact]
    public void EntityComponent_Deserialize()
    {
        // Arrange
        const string json = "{\"selector\":\"@a\",\"separator\":{\"text\":\";\"}}";

        // Act
        var deserialized = JsonSerializer.Deserialize(json,
            MineJasonTextJsonContext.Default.ChatComponent);

        // Assert
        Assert.Equal(ChatComponent.CreateSelector(new EntitySelector(EntitySelectorKind.AllPlayers), ChatComponent.CreateText(";")),
            deserialized);
    }

    [Fact]
    public void StorageNbtComponent_Serialize()
    {
        // Arrange
        var component = ChatComponent.CreateNbt(new ResourceLocation("mine", "storage"), "path.to.NBT");

        // Act
        var json = JsonSerializer.Serialize(component,
            MineJasonTextJsonContext.Default.ChatComponent);

        // Assert
        Assert.Equal("{\"type\":\"nbt\",\"source\":\"storage\",\"nbt\":\"path.to.NBT\",\"storage\":\"mine:storage\"}", json);
    }

    [Fact]
    public void StorageNbtComponent_Deserialize()
    {
        // Arrange
        const string json = "{\"storage\":\"mine:storage\",\"nbt\":\"path.to.NBT\"}";

        // Act
        var deserialized = JsonSerializer.Deserialize(json,
            MineJasonTextJsonContext.Default.ChatComponent);

        // Assert
        Assert.Equal(ChatComponent.CreateNbt(new ResourceLocation("mine", "storage"), "path.to.NBT"),
            deserialized);
    }

    [Fact]
    public void BlockNbtComponent_Serialize()
    {
        // Arrange
        var component = ChatComponent.CreateNbt(new BlockPosition(12, 12, 12), "path.to.NBT");

        // Act
        var json = JsonSerializer.Serialize(component,
            MineJasonTextJsonContext.Default.ChatComponent);

        // Assert
        Assert.Equal("{\"type\":\"nbt\",\"source\":\"block\",\"nbt\":\"path.to.NBT\",\"block\":\"12 12 12\"}", json);
    }

    [Fact]
    public void BlockNbtComponent_Deserialize()
    {
        // Arrange
        const string json = "{\"block\":\"12 12 12\",\"nbt\":\"path.to.NBT\"}";

        // Act
        var deserialized = JsonSerializer.Deserialize(json,
            MineJasonTextJsonContext.Default.ChatComponent);

        // Assert
        Assert.Equal(ChatComponent.CreateNbt(new BlockPosition(12, 12, 12), "path.to.NBT"),
            deserialized);
    }

    [Fact]
    public void EntityNbtComponent_Serialize()
    {
        // Arrange
        var component = ChatComponent.CreateNbt(new EntitySelector(EntitySelectorKind.Executor), "path.to.NBT");

        // Act
        var json = JsonSerializer.Serialize(component,
            MineJasonTextJsonContext.Default.ChatComponent);

        // Assert
        Assert.Equal("{\"type\":\"nbt\",\"source\":\"entity\",\"nbt\":\"path.to.NBT\",\"entity\":\"@s\"}", json);
    }

    [Fact]
    public void SpriteComponent_Serialize()
    {
        // Arrange
        var component = ChatComponent.CreateAtlasObject(
            sprite: new ResourceLocation("foo", "bar"),
            atlas: new ResourceLocation("foo", "atlas"));

        // Act
        var json = JsonSerializer.Serialize(component,
            MineJasonTextJsonContext.Default.ChatComponent);

        // Assert
        Assert.Equal("{\"type\":\"object\",\"object\":\"atlas\",\"atlas\":\"foo:atlas\",\"sprite\":\"foo:bar\"}",
            json);
    }

    [Fact]
    public void SpriteComponent_Serialize_WithFallback()
    {
        // Arrange
        var component = ChatComponent.CreateAtlasObject(
            sprite: new ResourceLocation("foo", "bar"),
            atlas: new ResourceLocation("foo", "atlas"),
            fallback: ChatComponent.CreateText("Fallback"));

        // Act
        var json = JsonSerializer.Serialize(component,
            MineJasonTextJsonContext.Default.ChatComponent);

        // Assert
        Assert.Equal("{\"type\":\"object\",\"object\":\"atlas\",\"fallback\":{\"type\":\"text\",\"text\":\"Fallback\"},\"atlas\":\"foo:atlas\",\"sprite\":\"foo:bar\"}",
            json);
    }

    [Fact]
    public void EntityNbtComponent_Deserialize()
    {
        // Arrange
        const string json = "{\"entity\":\"@s\",\"nbt\":\"path.to.NBT\"}";

        // Act
        var deserialized = JsonSerializer.Deserialize(json,
            MineJasonTextJsonContext.Default.ChatComponent);

        // Assert
        Assert.Equal(ChatComponent.CreateNbt(new EntitySelector(EntitySelectorKind.Executor), "path.to.NBT"),
            deserialized);
    }

    [Fact]
    public void SpriteComponent_Deserialize_WithFallback()
    {
        // Arrange
        const string json = "{\"atlas\":\"foo:atlas\",\"sprite\":\"foo:bar\",\"fallback\":{\"text\":\"Fallback\"}}";

        // Act
        var deserialized = JsonSerializer.Deserialize(json,
            MineJasonTextJsonContext.Default.ChatComponent);

        // Assert
        Assert.Equal(ChatComponent.CreateAtlasObject(sprite: new ResourceLocation("foo", "bar"),
            atlas: new ResourceLocation("foo", "atlas"),
            fallback: ChatComponent.CreateText("Fallback")),
            deserialized);
    }

    [Fact]
    public void SpriteComponent_Deserialize()
    {
        // Arrange
        const string json = "{\"atlas\":\"foo:atlas\",\"sprite\":\"foo:bar\"}";

        // Act
        var deserialized = JsonSerializer.Deserialize(json,
            MineJasonTextJsonContext.Default.ChatComponent);

        // Assert
        Assert.Equal(ChatComponent.CreateAtlasObject(sprite: new ResourceLocation("foo", "bar"),
            atlas: new ResourceLocation("foo", "atlas")),
            deserialized);
    }

    [Fact]
    public void TextComponent_Serialize_WithColor()
    {
        // Arrange
        var component = ChatComponent.CreateText("Hello World!")
            .WithColor(NamedTextColor.Aqua);

        // Act
        var json = JsonSerializer.Serialize(component,
            MineJasonTextJsonContext.Default.ChatComponent);

        // Assert
        Assert.Equal("{\"type\":\"text\",\"text\":\"Hello World!\",\"color\":\"aqua\"}", json);
    }

    [Fact]
    public void TranslatableComponent_Serialize_WithColor()
    {
        // Arrange
        var component = ChatComponent.CreateTranslatable("translatable.key")
            .WithColor(NamedTextColor.Aqua);

        // Act
        var json = JsonSerializer.Serialize(component,
            MineJasonTextJsonContext.Default.ChatComponent);

        // Assert
        Assert.Equal("{\"type\":\"translatable\",\"translate\":\"translatable.key\",\"color\":\"aqua\"}", json);
    }
}