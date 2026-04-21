// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

using System.Text.Json;
using MineJason.Extras.Selectors;

namespace MineJason.Tests.Extras;

public class JsonConvertTests
{
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
}