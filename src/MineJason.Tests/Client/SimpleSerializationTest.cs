// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

using System.Text.Json;
using MineJason.Data;
using MineJason.Data.Coordinates;
using MineJason.Tests.Client.Json;
using MineJason.Text;
using MineJason.Text.Colors;

namespace MineJason.Tests.Client;

public class SimpleSerializationTests
{
    [Fact]
    public void TextComponent_Serialize()
    {
        // Arrange
        var component = TextComponent.CreateText("I am text");

        // Act
        var json = JsonSerializer.Serialize(component,
            JsonTestContext.Default.TextComponent);

        // Assert
        Assert.Equal("{\"type\":\"text\",\"text\":\"I am text\"}", json);
    }

    [Fact]
    public void TextComponent_Deserialize()
    {
        // Arrange
        const string json = "{\"text\":\"Hello World!\"}";

        // Act
        var deserialized = JsonSerializer.Deserialize<TextComponent>(json,
            JsonTestContext.Default.TextComponent);

        // Assert
        Assert.Equal(TextComponent.CreateText("Hello World!"), deserialized);
    }

    [Fact]
    public void TextComponent_WithExtras_Serialize()
    {
        // Arrange
        var component = TextComponent.CreateText()
            .Value("My Text")
            .Extras([TextComponent.CreateText("My Extra")])
            .Build();

        // Act
        var json = JsonSerializer.Serialize(component,
            JsonTestContext.Default.TextComponent);

        // Assert
        Assert.Equal("{\"type\":\"text\",\"text\":\"My Text\",\"extra\":[{\"type\":\"text\",\"text\":\"My Extra\"}]}",
            json);
    }

    [Fact]
    public void TextComponent_WithExtras_Deserialize()
    {
        // Arrange
        const string json = "{\"text\":\"Hello World!\",\"extra\":[\"And to you!\"]}";

        // Act
        var deserialized = JsonSerializer.Deserialize(json,
            JsonTestContext.Default.TextComponent);

        // Assert
        Assert.NotNull(deserialized);
        Assert.NotNull(deserialized.Extra);
        Assert.Multiple(() => Assert.Equal("Hello World!",
            Assert.IsType<LiteralTextComponent>(deserialized).Text),
            () => Assert.Equal(TextComponent.CreateText("And to you!"),
            Assert.Single(deserialized.Extra!)));
    }

    [Fact]
    public void TextComponent_Deserialize_RawString()
    {
        // Arrange
        const string json = "\"Hello World!\"";

        // Act
        var deserialized = JsonSerializer.Deserialize(json,
            JsonTestContext.Default.TextComponent);

        // Assert
        Assert.Equal(TextComponent.CreateText("Hello World!"), deserialized);
    }

    [Fact]
    public void TranslatableComponent_Serialize()
    {
        // Arrange
        var component = TextComponent.CreateTranslatable("translatable.key");

        // Act
        var json = JsonSerializer.Serialize(component,
            JsonTestContext.Default.TextComponent);

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
            JsonTestContext.Default.TextComponent);

        // Assert
        var exp = TextComponent.CreateTranslatable("translatable.key");
        Assert.Equal(exp, deserialized);
    }

    [Fact]
    public void KeybindComponent_Serialize()
    {
        // Arrange
        var component = TextComponent.CreateKeybind()
            .Keybind("test.key")
            .Build();

        // Act
        var json = JsonSerializer.Serialize(component,
            JsonTestContext.Default.TextComponent);

        // Assert
        Assert.Equal("{\"type\":\"keybind\",\"keybind\":\"test.key\"}",
            json);
    }

    [Fact]
    public void KeybindComponent_Deserialize()
    {
        // Arrange
        const string json = "{\"keybind\":\"test.key\"}";

        // Act
        var deserialized = JsonSerializer.Deserialize(json,
            JsonTestContext.Default.TextComponent);

        // Assert
        Assert.Equal(TextComponent.CreateKeybind()
            .Keybind("test.key")
            .Build(), deserialized);
    }

    [Fact]
    public void ScoreboardComponent_Serialize()
    {
        // Arrange
        var component = TextComponent.CreateScore("Player", "advancements");

        // Act
        var json = JsonSerializer.Serialize(component,
            JsonTestContext.Default.TextComponent);

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
            JsonTestContext.Default.TextComponent);

        // Assert
        Assert.Equal(TextComponent.CreateScore("Player", "advancements"), deserialized);
    }

    [Fact]
    public void EntityComponent_Serialize()
    {
        // Arrange
        var component = TextComponent.CreateSelector("@a", TextComponent.CreateText(";"));

        // Act
        var json = JsonSerializer.Serialize(component,
            JsonTestContext.Default.TextComponent);

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
            JsonTestContext.Default.TextComponent);

        // Assert
        Assert.Equal(TextComponent.CreateSelector("@a", TextComponent.CreateText(";")),
            deserialized);
    }

    [Fact]
    public void StorageNbtComponent_Serialize()
    {
        // Arrange
        var component = TextComponent.CreateNbt(new ResourceLocation("mine", "storage"), "path.to.NBT");

        // Act
        var json = JsonSerializer.Serialize(component,
            JsonTestContext.Default.TextComponent);

        // Assert
        Assert.Equal("{\"type\":\"nbt\",\"source\":\"storage\",\"nbt\":\"path.to.NBT\",\"storage\":\"mine:storage\"}", json);
    }

    [Fact]
    public void StorageNbt_InterpretEnabled_OutputsPlainTrue()
    {
        // Arrange
        var component = TextComponent.CreateNbt()
            .Storage(new ResourceLocation("mine", "storage"))
            .Path("path.to.NBT")
            .Interpret()
            .Build();

        // Act
        var json = JsonSerializer.Serialize(component,
            JsonTestContext.Default.TextComponent);

        // Assert
        Assert.Equal("{\"type\":\"nbt\",\"source\":\"storage\",\"nbt\":\"path.to.NBT\",\"interpret\":true,\"storage\":\"mine:storage\"}", json);
    }

    [Fact]
    public void StorageNbt_PlainEnabled_OutputsPlainTrue()
    {
        // Arrange
        var component = TextComponent.CreateNbt()
            .Storage(new ResourceLocation("mine", "storage"))
            .Path("path.to.NBT")
            .Plain()
            .Build();

        // Act
        var json = JsonSerializer.Serialize(component,
            JsonTestContext.Default.TextComponent);

        // Assert
        Assert.Equal("{\"type\":\"nbt\",\"source\":\"storage\",\"nbt\":\"path.to.NBT\",\"plain\":true,\"storage\":\"mine:storage\"}", json);
    }

    [Fact]
    public void StorageNbtComponent_Deserialize()
    {
        // Arrange
        const string json = "{\"storage\":\"mine:storage\",\"nbt\":\"path.to.NBT\"}";

        // Act
        var deserialized = JsonSerializer.Deserialize(json,
            JsonTestContext.Default.TextComponent);

        // Assert
        Assert.Equal(TextComponent.CreateNbt(new ResourceLocation("mine", "storage"), "path.to.NBT"),
            deserialized);
    }

    [Fact]
    public void BlockNbtComponent_Serialize()
    {
        // Arrange
        var component = TextComponent.CreateNbt(new BlockPosition(12, 12, 12), "path.to.NBT");

        // Act
        var json = JsonSerializer.Serialize(component,
            JsonTestContext.Default.TextComponent);

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
            JsonTestContext.Default.TextComponent);

        // Assert
        Assert.Equal(TextComponent.CreateNbt(new BlockPosition(12, 12, 12), "path.to.NBT"),
            deserialized);
    }

    [Fact]
    public void EntityNbtComponent_Serialize()
    {
        // Arrange
        var component = TextComponent.CreateNbt("@s", "path.to.NBT");

        // Act
        var json = JsonSerializer.Serialize(component,
            JsonTestContext.Default.TextComponent);

        // Assert
        Assert.Equal("{\"type\":\"nbt\",\"source\":\"entity\",\"nbt\":\"path.to.NBT\",\"entity\":\"@s\"}", json);
    }

    [Fact]
    public void SpriteComponent_Serialize()
    {
        // Arrange
        var component = TextComponent.CreateAtlasObject(
            sprite: new ResourceLocation("foo", "bar"),
            atlas: new ResourceLocation("foo", "atlas"));

        // Act
        var json = JsonSerializer.Serialize(component,
            JsonTestContext.Default.TextComponent);

        // Assert
        Assert.Equal("{\"type\":\"object\",\"object\":\"atlas\",\"atlas\":\"foo:atlas\",\"sprite\":\"foo:bar\"}",
            json);
    }

    [Fact]
    public void SpriteComponent_Serialize_WithFallback()
    {
        // Arrange
        var component = TextComponent.CreateAtlasObject(
            sprite: new ResourceLocation("foo", "bar"),
            atlas: new ResourceLocation("foo", "atlas"),
            fallback: TextComponent.CreateText("Fallback"));

        // Act
        var json = JsonSerializer.Serialize(component,
            JsonTestContext.Default.TextComponent);

        // Assert
        Assert.Equal("{\"type\":\"object\",\"object\":\"atlas\",\"fallback\":{\"type\":\"text\",\"text\":\"Fallback\"},\"atlas\":\"foo:atlas\",\"sprite\":\"foo:bar\"}",
            json);
    }

    [Fact]
    public void PlayerSpriteComponent_Serialize()
    {
        // Arrange
        var component = new PlayerObjectTextComponent()
        {
            Player = new Data.Profile.PlayerProfile
            {
                Name = "PlayerName"
            }
        };

        // Act
        var json = JsonSerializer.Serialize(component,
            JsonTestContext.Default.TextComponent);

        // Assert
        Assert.Equal("{\"type\":\"object\",\"object\":\"player\",\"player\":{\"name\":\"PlayerName\"}}",
            json);
    }

    [Fact]
    public void PlayerSpriteComponent_Deserialize()
    {
        // Arrange
        const string json = "{\"type\":\"object\",\"object\":\"player\",\"player\":{\"name\":\"PlayerName\"}}";

        // Act
        var deserialized = JsonSerializer.Deserialize(json,
            JsonTestContext.Default.TextComponent);

        // Assert
        Assert.Equal(new PlayerObjectTextComponent()
        {
            Player = new Data.Profile.PlayerProfile
            {
                Name = "PlayerName"
            }
        }, deserialized);
    }


    [Fact]
    public void EntityNbtComponent_Deserialize()
    {
        // Arrange
        const string json = "{\"entity\":\"@s\",\"nbt\":\"path.to.NBT\"}";

        // Act
        var deserialized = JsonSerializer.Deserialize(json,
            JsonTestContext.Default.TextComponent);

        // Assert
        Assert.Equal(TextComponent.CreateNbt("@s", "path.to.NBT"),
            deserialized);
    }

    [Fact]
    public void SpriteComponent_Deserialize_WithFallback()
    {
        // Arrange
        const string json = "{\"atlas\":\"foo:atlas\",\"sprite\":\"foo:bar\",\"fallback\":{\"text\":\"Fallback\"}}";

        // Act
        var deserialized = JsonSerializer.Deserialize(json,
            JsonTestContext.Default.TextComponent);

        // Assert
        Assert.Equal(TextComponent.CreateAtlasObject(sprite: new ResourceLocation("foo", "bar"),
            atlas: new ResourceLocation("foo", "atlas"),
            fallback: TextComponent.CreateText("Fallback")),
            deserialized);
    }

    [Fact]
    public void SpriteComponent_Deserialize()
    {
        // Arrange
        const string json = "{\"atlas\":\"foo:atlas\",\"sprite\":\"foo:bar\"}";

        // Act
        var deserialized = JsonSerializer.Deserialize(json,
            JsonTestContext.Default.TextComponent);

        // Assert
        Assert.Equal(TextComponent.CreateAtlasObject(sprite: new ResourceLocation("foo", "bar"),
            atlas: new ResourceLocation("foo", "atlas")),
            deserialized);
    }

    [Fact]
    public void TextComponent_Serialize_WithColor()
    {
        // Arrange
        var component = TextComponent.CreateText("Hello World!")
            .WithColor(NamedTextColor.Aqua);

        // Act
        var json = JsonSerializer.Serialize(component,
            JsonTestContext.Default.TextComponent);

        // Assert
        Assert.Equal("{\"type\":\"text\",\"text\":\"Hello World!\",\"color\":\"aqua\"}", json);
    }

    [Fact]
    public void TranslatableComponent_Serialize_WithColor()
    {
        // Arrange
        var component = TextComponent.CreateTranslatable("translatable.key")
            .WithColor(NamedTextColor.Aqua);

        // Act
        var json = JsonSerializer.Serialize(component,
            JsonTestContext.Default.TextComponent);

        // Assert
        Assert.Equal("{\"type\":\"translatable\",\"translate\":\"translatable.key\",\"color\":\"aqua\"}", json);
    }

    [Fact]
    public void BlockPosition_Serialize()
    {
        // Arrange
        var pos = new BlockPosition(12, 34, 56);

        // Act
        var json = JsonSerializer.Serialize(pos,
            JsonTestContext.Default.BlockPosition);

        // Assert
        Assert.Equal("\"12 34 56\"",
            json);
    }

    [Fact]
    public void BlockPosition_Deserialize()
    {
        // Arrange
        const string json = "\"~2 ~2 ~3\"";

        // Act
        var deserialized = JsonSerializer.Deserialize(json,
            JsonTestContext.Default.BlockPosition);

        // Assert
        Assert.Equal(new BlockPosition(2, 2, 3,
            BlockPositionComponentType.Relative),
            deserialized);
    }

    [Fact]
    public void BlockPosition_Deserialize_NotStringError()
    {
        // Arrange
        const string json = "123";

        // Act
        var exception = Record.Exception(() => JsonSerializer.Deserialize(json,
            JsonTestContext.Default.BlockPosition));

        // Assert
        Assert.IsType<JsonException>(exception, false);
    }
}