// Copyright (c) WithLithum & contributors 2023-2024. All rights reserved.
// Licensed under the GNU Lesser General Public License, either version 3 or
// (at your opinion) any later version.

namespace MineJason.Tests.Items;

using MineJason.Data;
using MineJason.Items.Blocks;

public class InteractivePredicateTests
{
    [Test]
    public void CanPlaceOn_GetString()
    {
        var component = new CanPlaceOnItemComponent();
        component.Predicates.Add(new InteractBlockPredicate
        {
            Blocks = { new TypeTagEntry(true, new ResourceLocation("minecraft", "test")) }
        });
        component.ShowInTooltip = false;
        
        Assert.That(component.GetString(),
            Is.EqualTo("{predicates:[{blocks:[\"#minecraft:test\"]}],show_in_tooltip:0b}"));
    }
    
    [Test]
    public void CanBreak_GetString()
    {
        var component = new CanBreakItemComponent();
        component.Predicates.Add(new InteractBlockPredicate
        {
            Blocks = { new TypeTagEntry(true, new ResourceLocation("minecraft", "breakable")) }
        });
        component.ShowInTooltip = false;
        
        Assert.That(component.GetString(),
            Is.EqualTo("{predicates:[{blocks:[\"#minecraft:breakable\"]}],show_in_tooltip:0b}"));
    }
}