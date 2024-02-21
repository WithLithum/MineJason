// Copyright (c) WithLithum & contributors 2023-2024. All rights reserved.
// Licensed under the GNU Lesser General Public License, either version 3 or
// (at your opinion) any later version.

namespace MineJason.Tests;

using System.Text.Json;
using MineJason.Data;
using MineJason.Serialization.TextJson;

public class ContextSerializationTests
{
    [Test]
    public void TextComponent_ContextSerialize()
    {
        var json = JsonSerializer.Serialize(ChatComponent.CreateText("text"),
            typeof(ChatComponent),
            MineJasonTextJsonContext.Default);
        
        Assert.That(json, Is.EqualTo("{\"text\":\"text\"}"));
    }
    
    [Test]
    public void TranslatableComponent_ContextSerialize()
    {
        var json = JsonSerializer.Serialize(ChatComponent.CreateTranslatable("its_me"),
            typeof(ChatComponent),
            MineJasonTextJsonContext.Default);
        
        Assert.That(json, Is.EqualTo("{\"translate\":\"its_me\"}"));
    }
    
    [Test]
    public void EntityComponent_ContextSerialize()
    {
        var json = JsonSerializer.Serialize(ChatComponent.CreateSelector(new EntitySelector(EntitySelectorKind.Executor)),
            typeof(ChatComponent),
            MineJasonTextJsonContext.Default);
        
        Assert.That(json, Is.EqualTo("{\"selector\":\"@s\"}"));
    }
}