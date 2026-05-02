// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

using MineJason.Text;

namespace MineJason.Tests.Client;

public class ComponentBuilderTests
{
    [Fact]
    public void Common_Bold_AffectsComponent()
    {
        // Arrange
        var builder = TextComponent.CreateText();

        // Act
        var result = builder.Value("Text")
            .Bold()
            .Build();

        // Assert
        Assert.True(result.Bold);
    }

    [Fact]
    public void Common_Italic_AffectsComponent()
    {
        // Arrange
        var builder = TextComponent.CreateText();

        // Act
        var result = builder.Value("Text")
            .Italic()
            .Build();

        // Assert
        Assert.True(result.Italic);
    }

    [Fact]
    public void Common_Underline_AffectsComponent()
    {
        // Arrange
        var builder = TextComponent.CreateText();

        // Act
        var result = builder.Value("Text")
            .Underline()
            .Build();

        // Assert
        Assert.True(result.Underline);
    }

    [Fact]
    public void Common_Strikethrough_AffectsComponent()
    {
        // Arrange
        var builder = TextComponent.CreateText();

        // Act
        var result = builder.Value("Text")
            .Strikethrough()
            .Build();

        // Assert
        Assert.True(result.Strikethrough);
    }

    [Fact]
    public void Common_Obfuscate_AffectsComponent()
    {
        // Arrange
        var builder = TextComponent.CreateText();

        // Act
        var result = builder.Value("Text")
            .Obfuscate()
            .Build();

        // Assert
        Assert.True(result.Obfuscated);
    }

    [Fact]
    public void Common_Insertion_AffectsComponent()
    {
        // Arrange
        var builder = TextComponent.CreateText();

        // Act
        var result = builder.Value("Text")
            .Insertion("Abc")
            .Build();

        // Assert
        Assert.Equal("Abc", result.Insertion);
    }

    [Fact]
    public void Keybind_Value_AffectsComponent()
    {
        // Arrange
        var builder = TextComponent.CreateKeybind();

        // Act
        var result = builder.Keybind("key.id")
            .Build();

        // Assert
        Assert.Equal("key.id", result.Keybind);
    }

    [Fact]
    public void Score_NameValue_AffectsComponent()
    {
        // Arrange
        var builder = TextComponent.CreateScore();

        // Act
        var result = builder.Name("foo")
            .Objective("bar")
            .Build();

        // Assert
        Assert.Multiple(() => Assert.Equal("foo", result.Score.Name),
            () => Assert.Equal("bar", result.Score.Objective));
    }

    [Fact]
    public void Translate_Fallback_AffectsComponent()
    {
        // Arrange
        var builder = TextComponent.CreateTranslatable();

        // Act
        var result = builder.Value("foo.bar")
            .Fallback("Fallback Text")
            .Build();

        // Assert
        Assert.Equal("Fallback Text", result.Fallback);
    }
}
