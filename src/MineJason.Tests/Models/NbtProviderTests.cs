namespace MineJason.Tests.Models;

using System.Text.Json;
using MineJason.Data;

public class NbtProviderTests
{
    [Test]
    public void Serialize()
    {
        Assert.That(JsonSerializer.Serialize(new NbtProvider("{Value:0}")),
            Is.EqualTo("\"{Value:0}\""));
    }
}