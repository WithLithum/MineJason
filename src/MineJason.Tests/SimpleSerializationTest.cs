namespace MineJason.Tests;
using System.Text.Json;
using MineJason.Data;
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
    public void ShowTextEvent_Serialize()
    {
        Assert.That(JsonSerializer.Serialize((HoverEvent)(new ShowTextHoverEvent(ChatComponent.CreateText("text this")))),
            Is.EqualTo("{\"action\":\"show_text\",\"contents\":{\"text\":\"text this\"}}"));
    }
    
    [Test]
    public void ShowItemEvent_Serialize()
    {
        Assert.That(JsonSerializer.Serialize((HoverEvent)(new ShowItemHoverEvent(new ResourceLocation("minecraft", "stone")))),
            Is.EqualTo("{\"action\":\"show_item\",\"id\":\"minecraft:stone\"}"));
    }
    
    [Test]
    public void ShowEntityHoverEvent_Serialize()
    {
        const string strGuid = "A389ECDC-6621-425D-B9B3-FB94E8C514DF";
        var guid = new Guid(strGuid);
        
        Assert.That(JsonSerializer.Serialize((HoverEvent)(new ShowEntityHoverEvent(new ResourceLocation("minecraft", "pig"),
                guid))),
            Is.EqualTo("{\"action\":\"show_entity\",\"type\":\"minecraft:pig\",\"id\":\"a389ecdc-6621-425d-b9b3-fb94e8c514df\"}"));
    }

    [Test]
    public void ShowTextEvent_Deserialize()
    {
        Assert.That(JsonSerializer.Deserialize<HoverEvent>("{\"action\":\"show_text\",\"contents\":{\"text\":\"text this\"}}"),
            Is.EqualTo(new ShowTextHoverEvent(ChatComponent.CreateText("text this"))));
    }
    
    [Test]
    public void ChangePageClickEvent_Serialize()
    {
        Assert.That(JsonSerializer.Serialize((ClickEvent)(new ChangePageClickEvent(12))),
            Is.EqualTo("{\"action\":\"change_page\",\"value\":12}"));
    }
    
    [Test]
    public void CopyToClipboardClickEvent_Serialize()
    {
        Assert.That(JsonSerializer.Serialize((ClickEvent)(new CopyToClipboardClickEvent("clipboard copy"))),
            Is.EqualTo("{\"action\":\"copy_to_clipboard\",\"value\":\"clipboard copy\"}"));
    }
    
    [Test]
    public void OpenUrlClickEvent_Serialize()
    {
        Assert.That(JsonSerializer.Serialize((ClickEvent)(new OpenUrlClickEvent(new Uri("https://minecraft.net")))),
            Is.EqualTo("{\"action\":\"open_url\",\"value\":\"https://minecraft.net\"}"));
    }

    [Test]
    public void RunCommandClickEvent_Serialize()
    {
        Assert.That(JsonSerializer.Serialize((ClickEvent)(new RunCommandClickEvent("effect clear"))),
            Is.EqualTo("{\"action\":\"run_command\",\"value\":\"effect clear\"}"));
    }
    
    [Test]
    public void SuggestCommandClickEvent_Serialize()
    {
        Assert.That(JsonSerializer.Serialize((ClickEvent)(new SuggestCommandClickEvent("effect clear"))),
            Is.EqualTo("{\"action\":\"suggest_command\",\"value\":\"effect clear\"}"));
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
    public void TranslatableComponent_SerializeWithColor()
    {
        var component = ChatComponent.CreateTranslatable("translatable.key");
        component.Color = KnownColor.Aqua;

        Assert.That(JsonSerializer.Serialize(component),
            Is.EqualTo("{\"translate\":\"translatable.key\",\"color\":\"aqua\"}"));
    }
}