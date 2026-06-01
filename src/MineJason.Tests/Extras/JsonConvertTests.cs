// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

using System.Text.Json;
using MineJason.Extras.Selectors;

namespace MineJason.Tests.Extras;

public class JsonConvertTests
{
    [Fact]
    public void AnySelectorConverter_ReadNotString_Error()
    {
        // Arrange
        var element = JsonElement.Parse("123");

        // Act
        var exception = Record.Exception(
            () => _ = JsonSerializer.Deserialize<IEntitySelector>(element));

        // Assert
        Assert.IsType<JsonException>(exception, exactMatch: false);
    }

    [Fact]
    public void AnySelectorConverter_ReadEmptyString_Error()
    {
        // Arrange
        var element = JsonElement.Parse("\"\"");

        // Act
        var exception = Record.Exception(
            () => _ = JsonSerializer.Deserialize<IEntitySelector>(element));

        // Assert
        Assert.IsType<JsonException>(exception, exactMatch: false);
    }

    [Fact]
    public void AnySelectorConverter_Read_Guid()
    {
        // Arrange
        const string input = "\"659D3AC7-41F3-4C7B-B0FC-9A4F0F3137A9\"";

        // Act
        var result = JsonSerializer.Deserialize<IEntitySelector>(input);

        // Assert
        Assert.Equal(new Guid("659D3AC7-41F3-4C7B-B0FC-9A4F0F3137A9"),
            Assert.IsType<EntityGuidSelector>(result).Value);
    }

    [Fact]
    public void AnySelectorConverter_Write_Guid()
    {
        // Arrange
        const string guid = "659D3AC7-41F3-4C7B-B0FC-9A4F0F3137A9";
        var selector = new EntityGuidSelector(Guid.Parse(guid));

        // Act
        var result = JsonSerializer.Serialize<IEntitySelector>(selector);

        // Assert
        Assert.Equal($"\"{guid}\"",
            result,
            StringComparer.OrdinalIgnoreCase);
    }

    [Fact]
    public void AnySelectorConverter_Read_Selector()
    {
        // Arrange
        const string selector = "@r[tag=!this_tag]";
        var element = JsonElement.Parse($"\"{selector}\"");

        // Act
        var result = JsonSerializer.Deserialize<IEntitySelector>(element);

        // Assert
        Assert.Equal(selector, result?.ToString());
    }

    [Fact]
    public void AnySelectorConverter_Write_Selector()
    {
        // Arrange
        var selector = EntitySelector.RandomPlayer()
            .Build();

        // Act
        var result = JsonSerializer.Serialize<IEntitySelector>(selector);

        // Assert
        Assert.Equal("\"@r\"", result);
    }

    [Fact]
    public void EntitySelectorConverter_Read_Selector()
    {
        // Arrange
        const string selector = "@r[tag=!this_tag]";
        var element = JsonElement.Parse($"\"{selector}\"");

        // Act
        var result = JsonSerializer.Deserialize<EntitySelector>(element);

        // Assert
        Assert.Equal(selector, result?.ToString());
    }

    [Fact]
    public void EntitySelectorConverter_ReadNotString_Error()
    {
        // Arrange
        var element = JsonElement.Parse("123");

        // Act
        var exception = Record.Exception(
            () => _ = JsonSerializer.Deserialize<EntitySelector>(element));

        // Assert
        Assert.IsType<JsonException>(exception, exactMatch: false);
    }

    [Fact]
    public void EntitySelectorConverter_ReadEmptyString_Error()
    {
        // Arrange
        var element = JsonElement.Parse("\"\"");

        // Act
        var exception = Record.Exception(
            () => _ = JsonSerializer.Deserialize<EntitySelector>(element));

        // Assert
        Assert.IsType<JsonException>(exception, exactMatch: false);
    }

    [Fact]
    public void EntitySelectorConverter_Write_Selector()
    {
        // Arrange
        var selector = EntitySelector.RandomPlayer()
            .ExcludeName("some_name")
            .Build();

        // Act
        var result = JsonSerializer.Serialize(selector);

        // Assert
        Assert.Equal("\"@r[name=!some_name]\"", result);
    }

    [Fact]
    public void GuidSelectorConverter_ReadNotString_Error()
    {
        // Arrange
        var element = JsonElement.Parse("123");

        // Act
        var exception = Record.Exception(
            () => _ = JsonSerializer.Deserialize<EntityGuidSelector>(element));

        // Assert
        Assert.IsType<JsonException>(exception, exactMatch: false);
    }

    [Fact]
    public void GuidSelectorConverter_ReadEmptyString_Error()
    {
        // Arrange
        var element = JsonElement.Parse("\"\"");

        // Act
        var exception = Record.Exception(
            () => _ = JsonSerializer.Deserialize<EntityGuidSelector>(element));

        // Assert
        Assert.IsType<JsonException>(exception, exactMatch: false);
    }

    [Fact]
    public void GuidSelectorConverter_ReadInvalidString_Error()
    {
        // Arrange
        var element = JsonElement.Parse("\"This is by no means a valid GUID\"");

        // Act
        var exception = Record.Exception(
            () => _ = JsonSerializer.Deserialize<EntityGuidSelector>(element));

        // Assert
        Assert.IsType<JsonException>(exception, exactMatch: false);
    }

    [Fact]
    public void GuidSelectorConverter_Read_Selector()
    {
        // Arrange
        var guid = new Guid("659D3AC7-41F3-4C7B-B0FC-9A4F0F3137A9");
        var element = JsonElement.Parse($"\"{guid}\"");

        // Act
        var result = JsonSerializer.Deserialize<EntityGuidSelector>(element);

        // Assert
        Assert.Equal(guid, result?.Value);
    }

    [Fact]
    public void GuidSelectorConverter_Write_Selector()
    {
        // Arrange
        var guid = new Guid("659D3AC7-41F3-4C7B-B0FC-9A4F0F3137A9");
        var selector = new EntityGuidSelector(guid);

        // Act
        var result = JsonSerializer.Serialize(selector);

        // Assert
        Assert.Equal($"\"{guid}\"", result, ignoreCase: true);
    }
}