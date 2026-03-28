// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

namespace MineJason.Tests.Client;

using MineJason.Serialization.IO.Json;
using MineJason.Serialization.Schema;
using MineJason.Serialization.TextJson;
using MineJason.Text;
using System.Text.Json;

public class ComponentTests
{
    [Fact]
    public void Json_IncorrectType()
    {
        // Arrange
        const string json = "{\"type\":\"nbt\",\"text\":\"Hello World!\"}";

        // Act
        var component = JsonSerializer.Deserialize(json, MineJasonTextJsonContext.Default.ChatComponent);

        // Assert
        Assert.Equal(TextComponent.CreateText("Hello World!"),
            (TextComponent?)component);
    }

    [Fact]
    public void Schema_ArrayOfComponents_ConvertsCorrectly()
    {
        // Arrange
        var json = JsonElement.Parse("[{\"text\":\"A\"},{\"text\":\"B\"}]");

        // Act
        var result = TextComponentSchema.Instance.Decode(json,
            new JsonElementDecoder());

        // Assert
        Assert.NotNull(result.Value);

        var literal = Assert.IsType<LiteralTextComponent>(result.Value);
        Assert.NotNull(literal.Extra);
        Assert.Multiple(() => Assert.Equal("A", literal.Text),
            () => Assert.Equal(TextComponent.CreateText("B"), Assert.Single(literal.Extra))
            );
    }

    [Fact]
    public void Schema_ArrayOfStringComponents_ConvertsCorrectly()
    {
        // Arrange
        var json = JsonElement.Parse("[\"A\",\"B\"]");

        // Act
        var result = TextComponentSchema.Instance.Decode(json,
            new JsonElementDecoder());

        // Assert
        Assert.NotNull(result.Value);

        var literal = Assert.IsType<LiteralTextComponent>(result.Value);
        Assert.NotNull(literal.Extra);
        Assert.Multiple(() => Assert.Equal("A", literal.Text),
            () => Assert.Equal(TextComponent.CreateText("B"), Assert.Single(literal.Extra))
            );
    }

    [Fact]
    public void Schema_ArrayOfVariousKindsOfStringComponents_ConvertsCorrectly()
    {
        // Arrange
        var json = JsonElement.Parse("[\"A\",\"B\",{\"text\":\"C\"}]");

        // Act
        var result = TextComponentSchema.Instance.Decode(json,
            new JsonElementDecoder());

        // Assert
        Assert.NotNull(result.Value);

        var literal = Assert.IsType<LiteralTextComponent>(result.Value);
        Assert.NotNull(literal.Extra);
        Assert.Multiple(() => Assert.Equal("A", literal.Text),
            () => Assert.Collection(literal.Extra,
                x1 => Assert.Equal(TextComponent.CreateText("B"), x1),
                x2 => Assert.Equal(TextComponent.CreateText("C"), x2))
        );
    }
}