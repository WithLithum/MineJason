using System.Text.Json;

namespace MineJason.Tests;

public class SimpleSerializationTests
{
    [Test]
    public void KnownChatColor_Serialize()
    {
        Assert.That(JsonSerializer.Serialize(KnownColor.Aqua),
            Is.EqualTo("\"aqua\""));
    }

    [Test]
    public void TextComponent_Serialize()
    {
        Assert.That(JsonSerializer.Serialize(ChatComponent.CreateText("I am text")),
            Is.EqualTo("{\"text\":\"I am text\"}"));
    }

    [Test]
    public void TranslatableComponent_Serialize()
    {
        Assert.That(JsonSerializer.Serialize(ChatComponent.CreateTranslatable("translatable.key")),
            Is.EqualTo("{\"translate\":\"translatable.key\"}"));
    }

    [Test]
    public void TranslatableComponent_Deserialize()
    {
        var deserialized = JsonSerializer.Deserialize<ChatComponent>("{\"translate\":\"translatable.key\"}");

        Assert.That(deserialized,
            Is.EqualTo(ChatComponent.CreateTranslatable("translatable.key")));
    }

    [Test]
    public void TranslatableComponent_SerializeWithColor()
    {
        var component = ChatComponent.CreateTranslatable("translatable.key");
        component.Color = KnownColor.Aqua;

        Assert.That(JsonSerializer.Serialize(component),
            Is.EqualTo("{\"translate\":\"translatable.key\",\"color\":\"aqua\"}"));
    }
}