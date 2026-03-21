// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

namespace MineJason.Tests.Client.Models;

using System.Text.Json;
using MineJason.Data;
using MineJason.Data.Nbt;
using MineJason.Serialization.TextJson;

public class NbtProviderTests
{
    [Fact]
    public void Interface_Json_Deserialize()
    {
        // Arrange
        const string json = "\"{Value:123}\"";
        
        // Act
        var result = JsonSerializer.Deserialize(json, 
            MineJasonTextJsonContext.Default.INbtDataProvider);
        
        // Assert
        Assert.Equal(new RawNbtDataProvider("{Value:123}"), result);
    }
    
    [Fact]
    public void Interface_Json_Serialize()
    {
        // Arrange
        var provider = new RawNbtDataProvider("{Value:123}");
        
        // Act
        var json = JsonSerializer.Serialize(provider,
            MineJasonTextJsonContext.Default.INbtDataProvider);
        
        // Assert
        Assert.Equal("\"{Value:123}\"", json);
    }
    
    [Fact]
    public void RawProvider_Json_Deserialize()
    {
        // Arrange
        const string json = "\"{Value:123}\"";
        
        // Act
        var result = JsonSerializer.Deserialize(json, 
            MineJasonTextJsonContext.Default.RawNbtDataProvider);
        
        // Assert
        Assert.Equal(new RawNbtDataProvider("{Value:123}"), result);
    }
    
    [Fact]
    public void RawProvider_Json_Serialize()
    {
        // Arrange
        var provider = new RawNbtDataProvider("{Value:123}");
        
        // Act
        var json = JsonSerializer.Serialize(provider,
            MineJasonTextJsonContext.Default.RawNbtDataProvider);
        
        // Assert
        Assert.Equal("\"{Value:123}\"", json);
    }
}