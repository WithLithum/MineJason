namespace MineJason.Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

public class ArrayComponentTests
{
    [Test]
    public void Extra_Deserialize()
    {
        var sample = ChatComponent.CreateText("Hallo")
            .Append(ChatComponent.CreateText("Again!"));

        const string json = "{\"text\":\"Hallo\",\"extra\":[{\"text\":\"Again!\"}]}";

        Assert.That(JsonSerializer.Deserialize<ChatComponent>(json),
            Is.EqualTo(sample));
    }

    [Test]
    public void Array_Deserialize()
    {
        var sample = ChatComponent.CreateText("Hallo")
            .Append(ChatComponent.CreateText("Again!"));

        const string json = "[{\"text\":\"Hallo\"},{\"text\":\"Again!\"}]";

        Assert.That(JsonSerializer.Deserialize<ChatComponent>(json),
            Is.EqualTo(sample));
    }
}
