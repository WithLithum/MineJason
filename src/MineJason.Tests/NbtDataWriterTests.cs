// Copyright (c) WithLithum & contributors 2023-2024. All rights reserved.
// Licensed under the GNU Lesser General Public License, either version 3 or
// (at your opinion) any later version.

namespace MineJason.Tests;

using MineJason.Data.Compiler;

public class NbtDataWriterTests
{
    [Test]
    public void Writer_TwoProperties()
    {
        var sw = new StringWriter();

        using (var writer = new NbtDataWriter(sw))
        {
            writer.WriteBeginCompound();
            writer.WriteRawProperty("A", "123");
            writer.WriteRawProperty("B", "456");
            writer.WriteEndCompound();
        }

        Assert.That(sw.ToString(),
            Is.EqualTo("{A:123,B:456}"));
    }
    
    [Test]
    public void Writer_ChatComponent()
    {
        var sw = new StringWriter();

        using (var writer = new NbtDataWriter(sw))
        {
            writer.WriteBeginCompound();
            writer.WriteProperty("Name", ChatComponent.CreateText("Hello World!"));
            writer.WriteEndCompound();
        }

        Assert.That(sw.ToString(),
            Is.EqualTo("{Name:'{\"text\":\"Hello World!\"}'}"));
    }
    
    [Test]
    public void Writer_CompoundInsideCompound()
    {
        var sw = new StringWriter();

        using (var writer = new NbtDataWriter(sw))
        {
            writer.WriteBeginCompound();
            writer.WritePropertyCompoundBegin("Property");
            writer.WriteRawProperty("SubA", "100");
            writer.WriteRawProperty("SubB", "300");
            writer.WritePropertyCompoundEnd();
            writer.WriteRawProperty("Inner", "500");
            writer.WriteEndCompound();
        }

        Assert.That(sw.ToString(),
            Is.EqualTo("{Property:{SubA:100,SubB:300},Inner:500}"));
    }
}