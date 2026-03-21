// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

namespace MineJason.Tests.Client;

using System.Text.Json;
using MineJason.Data;
using MineJason.Data.Selectors;

public class JsonConvertTests
{
    [Fact]
    public void AnySelectorConverter_Write_Guid()
    {
        // Arrange
        const string guid = "659D3AC7-41F3-4C7B-B0FC-9A4F0F3137A9";
        var selector = new EntityGuidSelector(Guid.Parse(guid));
        
        // Act
        var result = JsonSerializer.Serialize(selector);
        
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
        var result = JsonSerializer.Serialize(selector);
        
        // Assert
        Assert.Equal("\"@r\"", result);
    }
}