namespace MineJason.Tests;
using System.Text.Json;
using MineJason.Data;
using MineJason.Data.Coordinates;
using MineJason.Events;
using MineJason.Events.Hover;

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
    public void TextComponent_Deserialize()
    {
        var deserialized = JsonSerializer.Deserialize<ChatComponent>("{\"text\":\"Hello World!\"}");

        Assert.That(deserialized,
            Is.EqualTo(ChatComponent.CreateText("Hello World!")));
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
    public void ScoreboardComponent_Serialize()
    {
        Assert.That(JsonSerializer.Serialize(ChatComponent.CreateScore("Player", "advancements")),
            Is.EqualTo("{\"score\":{\"name\":\"Player\",\"objective\":\"advancements\"}}"));
    }

    [Test]
    public void ScoreboardComponent_Deserialize()
    {
        var deserialized = JsonSerializer.Deserialize<ChatComponent>("{\"score\":{\"name\":\"Player\",\"objective\":\"kills\"}}");

        Assert.That(deserialized,
            Is.EqualTo(ChatComponent.CreateScore("Player", "kills")));
    }

    [Test]
    public void EntityComponent_Serialize()
    {
        Assert.That(JsonSerializer.Serialize(ChatComponent.CreateSelector(new EntitySelector(EntitySelectorKind.AllPlayers), ChatComponent.CreateText(";"))),
            Is.EqualTo("{\"selector\":\"@a\",\"separator\":{\"text\":\";\"}}"));
    }


    [Test]
    public void EntityComponent_Deserialize()
    {
        var deserialized = JsonSerializer.Deserialize<ChatComponent>("{\"selector\":\"@a\",\"separator\":{\"text\":\";\"}}");

        Assert.That(deserialized,
            Is.EqualTo(ChatComponent.CreateSelector(new EntitySelector(EntitySelectorKind.AllPlayers), ChatComponent.CreateText(";"))));
    }

    [Test]
    public void StorageNbtComponent_Serialize()
    {
        Assert.That(JsonSerializer.Serialize(ChatComponent.CreateNbt(new ResourceLocation("mine", "storage"), "path.to.NBT")),
            Is.EqualTo("{\"storage\":\"mine:storage\",\"nbt\":\"path.to.NBT\"}"));
    }


    [Test]
    public void StorageNbtComponent_Deserialize()
    {
        var deserialized = JsonSerializer.Deserialize<ChatComponent>("{\"storage\":\"mine:storage\",\"nbt\":\"path.to.NBT\"}");

        Assert.That(deserialized,
            Is.EqualTo(ChatComponent.CreateNbt(new ResourceLocation("mine", "storage"), "path.to.NBT")));
    }

    [Test]
    public void BlockNbtComponent_Serialize()
    {
        Assert.That(JsonSerializer.Serialize(ChatComponent.CreateNbt(new AnyBlockPosition(new BlockPosition(12, 12, 12)), "path.to.NBT")),
            Is.EqualTo("{\"block\":\"12 12 12\",\"nbt\":\"path.to.NBT\"}"));
    }


    [Test]
    public void BlockNbtComponent_Deserialize()
    {
        var deserialized = JsonSerializer.Deserialize<ChatComponent>("{\"block\":\"12 12 12\",\"nbt\":\"path.to.NBT\"}");

        Assert.That(deserialized,
            Is.EqualTo(ChatComponent.CreateNbt(new AnyBlockPosition(new BlockPosition(12, 12, 12)), "path.to.NBT")));
    }

    [Test]
    public void EntityNbtComponent_Serialize()
    {
        Assert.That(JsonSerializer.Serialize(ChatComponent.CreateNbt(new EntitySelector(EntitySelectorKind.Executor), "path.to.NBT")),
            Is.EqualTo("{\"entity\":\"@s\",\"nbt\":\"path.to.NBT\"}"));
    }


    [Test]
    public void EntityNbtComponent_Deserialize()
    {
        var deserialized = JsonSerializer.Deserialize<ChatComponent>("{\"entity\":\"@s\",\"nbt\":\"path.to.NBT\"}");

        Assert.That(deserialized,
            Is.EqualTo(ChatComponent.CreateNbt(new EntitySelector(EntitySelectorKind.Executor), "path.to.NBT")));
    }

    [Test]
    public void TextComponent_SerializeWithColor()
    {
        var component = ChatComponent.CreateText("Hello World!");
        component.Color = KnownColor.Aqua;

        Assert.That(JsonSerializer.Serialize(component),
            Is.EqualTo("{\"text\":\"Hello World!\",\"color\":\"aqua\"}"));
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