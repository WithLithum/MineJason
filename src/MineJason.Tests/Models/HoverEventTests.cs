// Copyright (c) WithLithum & contributors 2023-2024. All rights reserved.
// Licensed under the GNU Lesser General Public License, either version 3 or
// (at your opinion) any later version.

namespace MineJason.Tests.Models;

using System.Text.Json;
using MineJason.Events.Hover;

public class HoverEventTests
{
    [Test]
    public void ShowEntityHoverEvent_Serialize()
    {
        var e = new ShowEntityHoverEvent(new ResourceLocation("minecraft", "pig"),
            new Guid("34514200-139B-463E-B59F-5D69EDB741E2"));
        
        Assert.That(JsonSerializer.Serialize<HoverEvent>(e),
            Is.EqualTo("{\"action\":\"show_entity\",\"type\":\"minecraft:pig\",\"id\":\"34514200-139b-463e-b59f-5d69edb741e2\"}"));
    }
    
    [Test]
    public void ShowEntityHoverEvent_Deserialize()
    {
        var e = new ShowEntityHoverEvent(new ResourceLocation("minecraft", "pig"),
            new Guid("34514200-139B-463E-B59F-5D69EDB741E2"));
        
        Assert.That(JsonSerializer.Deserialize<HoverEvent>("{\"action\":\"show_entity\",\"type\":\"minecraft:pig\",\"id\":\"34514200-139b-463e-b59f-5d69edb741e2\"}"),
            Is.EqualTo(e));
    }
    
    [Test]
    public void ShowItemHoverEvent_Serialize()
    {
        var e = new ShowItemHoverEvent(new ResourceLocation("minecraft", "stone"),
            12);
        
        Assert.That(JsonSerializer.Serialize<HoverEvent>(e),
            Is.EqualTo("{\"action\":\"show_item\",\"id\":\"minecraft:stone\",\"count\":12}"));
    }
    
    [Test]
    public void ShowItemHoverEvent_Deserialize()
    {
        var e = new ShowItemHoverEvent(new ResourceLocation("minecraft", "stone"),
            12);
        
        Assert.That(JsonSerializer.Deserialize<HoverEvent>("{\"action\":\"show_item\",\"id\":\"minecraft:stone\",\"count\":12}"),
            Is.EqualTo(e));
    }
    
    [Test]
    public void ShowTextHoverEvent_Serialize()
    {
        var e = new ShowTextHoverEvent(ChatComponent.CreateText("Hello World!"));
        
        Assert.That(JsonSerializer.Serialize<HoverEvent>(e),
            Is.EqualTo("{\"action\":\"show_text\",\"contents\":{\"text\":\"Hello World!\"}}"));
    }
    
    [Test]
    public void ShowTextHoverEvent_Deserialize()
    {
        var e = new ShowTextHoverEvent(ChatComponent.CreateText("Hello World!"));
        
        Assert.That(JsonSerializer.Deserialize<HoverEvent>("{\"action\":\"show_text\",\"contents\":{\"text\":\"Hello World!\"}}"),
            Is.EqualTo(e));
    }
}