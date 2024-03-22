// Copyright (c) WithLithum & contributors 2023-2024. All rights reserved.
// Licensed under the GNU Lesser General Public License, either version 3 or
// (at your opinion) any later version.

namespace MineJason.Tests.Models;

using MineJason.Data;

public class TypeTagTests
{
    [Test]
    public void TypeTagEntry_Tag_ToString()
    {
        Assert.That(new TypeTagEntry(true, new ResourceLocation("minecraft", "beta")).ToString(),
            Is.EqualTo("#minecraft:beta"));
    }
    
    [Test]
    public void TypeTagEntry_Type_ToString()
    {
        Assert.That(new TypeTagEntry(false, new ResourceLocation("minecraft", "not_a_tag")).ToString(),
            Is.EqualTo("minecraft:not_a_tag"));
    }
    
    [Test]
    public void TypeTagEntry_Tag_Parse()
    {
        var id = new ResourceLocation("minecraft", "test_a_tag");

        if (!TypeTagEntry.TryParse($"#{id}", out var entry))
        {
            Assert.Fail("Parsing returns false");
        }
        
        Assert.Multiple(() =>
        {
            Assert.That(entry.IsTag);
            Assert.That(entry.Identifier, Is.EqualTo(id));
        });
    }
    
    [Test]
    public void TypeTagEntry_Type_Parse()
    {
        var id = new ResourceLocation("minecraft", "test_not_a_tag");

        if (!TypeTagEntry.TryParse(id.ToString(), out var entry))
        {
            Assert.Fail("Parsing returns false");
        }
        
        Assert.Multiple(() =>
        {
            Assert.That(!entry.IsTag);
            Assert.That(entry.Identifier, Is.EqualTo(id));
        });
    }
}