// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

namespace MineJason.Tests.Client.Models;

using System.Text.Json;
using MineJason.Data.Loot;
using MineJason.Data.Loot.Numbers;
using MineJason.Serialization.TextJson;

public class LootNumberProviderTests
{
    [Fact]
    public void ExactProvider_IntMaxValue()
    {
        // Arrange
        const int value = int.MaxValue;
        
        // Act
        var provider = new LootExactNumberProvider(value);
        
        // Assert
        Assert.Equal(value, provider.Value);
    }

    [Fact]
    public void ExactProvider_Read_Shorthand()
    {
        // Arrange
        const string json = "25";
        
        // Act
        var provider = JsonSerializer.Deserialize(json,
            MineJasonTextJsonContext.Default.ILootNumberProvider);
        
        // Assert
        Assert.Equal(new LootExactNumberProvider(25f), provider);
    }
    
    [Fact]
    public void ExactProvider_Read_Typed()
    {
        // Arrange
        const string json = "{\"type\":\"constant\",\"value\":25}";
        
        // Act
        var provider = JsonSerializer.Deserialize(json,
            MineJasonTextJsonContext.Default.ILootNumberProvider);
        
        // Assert
        Assert.Equal(new LootExactNumberProvider(25f), provider);
    }
    
    [Fact]
    public void UniformProvider_Read_Shorthand()
    {
        // Arrange
        const string json = "{\"min\":10,\"max\":20}";
        
        // Act
        var provider = JsonSerializer.Deserialize(json,
            MineJasonTextJsonContext.Default.ILootNumberProvider);
        
        // Assert
        Assert.Equal(new LootUniformNumberProvider(10f, 20f), provider);
    }
    
    [Fact]
    public void UniformProvider_Read_Typed()
    {
        // Arrange
        const string json = "{\"type\":\"minecraft:uniform\",\"min\":10,\"max\":20}";
        
        // Act
        var provider = JsonSerializer.Deserialize(json,
            MineJasonTextJsonContext.Default.ILootNumberProvider);
        
        // Assert
        Assert.Equal(new LootUniformNumberProvider(10f, 20f), provider);
    }
    
    [Fact]
    public void BinomialProvider_Read_Typed()
    {
        // Arrange
        const string json = "{\"type\":\"minecraft:binomial\",\"min\":10,\"max\":20}";
        
        // Act
        var provider = JsonSerializer.Deserialize(json,
            MineJasonTextJsonContext.Default.ILootNumberProvider);
        
        // Assert
        Assert.Equal(new LootBinomialNumberProvider(10f, 20f), provider);
    }

    [Fact]
    public void ScoreProvider_Write_Fixed_NoScale()
    {
        // Arrange
        var provider = new LootScoreNumberProvider(new LootScoreFixedTarget("test_target"),
            score: "objective",
            scale: null);
        
        // Act
        var json = JsonSerializer.Serialize(provider,
            MineJasonTextJsonContext.Default.ILootNumberProvider);
        
        // Assert
        Assert.Equal("{\"type\":\"minecraft:score\",\"target\":{\"type\":\"fixed\",\"name\":\"test_target\"},\"score\":\"objective\"}", 
            json);
    }
    
    [Fact]
    public void ScoreProvider_Write_Fixed_Scale()
    {
        // Arrange
        var provider = new LootScoreNumberProvider(new LootScoreFixedTarget("test_target"),
            score: "objective",
            scale: 2.5f);
        
        // Act
        var json = JsonSerializer.Serialize(provider,
            MineJasonTextJsonContext.Default.ILootNumberProvider);
        
        // Assert
        Assert.Equal("{\"type\":\"minecraft:score\",\"target\":{\"type\":\"fixed\",\"name\":\"test_target\"},\"score\":\"objective\",\"scale\":2.5}",
            json);
    }
    
    [Fact]
    public void ScoreProvider_Write_Context_NoScale()
    {
        // Arrange
        var provider = new LootScoreNumberProvider(new LootScoreContextTarget(LootContextTarget.This),
            score: "objective",
            scale: null);
        
        // Act
        var json = JsonSerializer.Serialize(provider,
            MineJasonTextJsonContext.Default.ILootNumberProvider);
        
        // Assert
        Assert.Equal("{\"type\":\"minecraft:score\",\"target\":{\"type\":\"context\",\"target\":\"this\"},\"score\":\"objective\"}",
            json);
    }
    
    [Fact]
    public void ScoreProvider_Write_Context_Scale()
    {
        // Arrange
        var provider = new LootScoreNumberProvider(new LootScoreContextTarget(LootContextTarget.This),
            score: "objective",
            scale: 2.5f);
        
        // Act
        var json = JsonSerializer.Serialize(provider,
            MineJasonTextJsonContext.Default.ILootNumberProvider);
        
        // Assert
        Assert.Equal("{\"type\":\"minecraft:score\",\"target\":{\"type\":\"context\",\"target\":\"this\"},\"score\":\"objective\",\"scale\":2.5}", json);
    }

    [Fact]
    public void ScoreProvider_Read_Context_Scale()
    {
        // Arrange
        var matchProvider = new LootScoreNumberProvider(new LootScoreContextTarget(LootContextTarget.This),
            score: "objective",
            scale: 2.5f);
        const string json = "{\"type\":\"minecraft:score\",\"target\":{\"type\":\"context\",\"target\":\"this\"},\"score\":\"objective\",\"scale\":2.5}";
        
        // Act
        var result = JsonSerializer.Deserialize(json, MineJasonTextJsonContext.Default.ILootNumberProvider);
        
        // Assert
        Assert.Equal(matchProvider, result);
    }
    
    [Fact]
    public void ScoreProvider_Read_Context_NoScale()
    {
        // Arrange
        var matchProvider = new LootScoreNumberProvider(new LootScoreContextTarget(LootContextTarget.This),
            score: "objective",
            scale: null);
        const string json = "{\"type\":\"minecraft:score\",\"target\":{\"type\":\"context\",\"target\":\"this\"},\"score\":\"objective\"}";
        
        // Act
        var result = JsonSerializer.Deserialize(json, MineJasonTextJsonContext.Default.ILootNumberProvider);
        
        // Assert
        Assert.Equal(matchProvider, result);
    }
    
    [Fact]
    public void ScoreProvider_Read_Fixed_Scale()
    {
        // Arrange
        var matchProvider = new LootScoreNumberProvider(new LootScoreFixedTarget("test"),
            score: "objective",
            scale: 2.5f);
        const string json = "{\"type\":\"minecraft:score\",\"target\":{\"type\":\"fixed\",\"name\":\"test\"},\"score\":\"objective\",\"scale\":2.5}";
        
        // Act
        var result = JsonSerializer.Deserialize(json, MineJasonTextJsonContext.Default.ILootNumberProvider);
        
        // Assert
        Assert.Equal(matchProvider, result);
    }
    
    [Fact]
    public void ScoreProvider_Read_Fixed_NoScale()
    {
        // Arrange
        var matchProvider = new LootScoreNumberProvider(new LootScoreFixedTarget("test"),
            score: "objective",
            scale: null);
        const string json = "{\"type\":\"minecraft:score\",\"target\":{\"type\":\"fixed\",\"name\":\"test\"},\"score\":\"objective\"}";
        
        // Act
        var result = JsonSerializer.Deserialize(json, MineJasonTextJsonContext.Default.ILootNumberProvider);
        
        // Assert
        Assert.Equal(matchProvider, result);
    }

    [Fact]
    public void StorageProvider_Read()
    {
        // Arrange
        var provider = new LootStorageNumberProvider(new ResourceLocation("test", "storage"),
            "path.to.number");
        const string json = "{\"type\":\"minecraft:storage\",\"storage\":\"test:storage\",\"path\":\"path.to.number\"}";
        
        // Act
        var result = JsonSerializer.Deserialize(json, MineJasonTextJsonContext.Default.ILootNumberProvider);
        
        // Assert
        Assert.Equal(provider, result);
    }
    
    [Fact]
    public void StorageProvider_Write()
    {
        // Arrange
        var provider = new LootStorageNumberProvider(new ResourceLocation("test", "storage"),
            "path.to.number");
        
        // Act
        var result = JsonSerializer.Serialize(provider, MineJasonTextJsonContext.Default.ILootNumberProvider);
        
        // Assert
        Assert.Equal("{\"type\":\"minecraft:storage\",\"storage\":\"test:storage\",\"path\":\"path.to.number\"}",
            result);
    }
}