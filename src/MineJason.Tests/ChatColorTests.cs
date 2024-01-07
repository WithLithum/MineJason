namespace MineJason.Tests;

using System.Text.Json;
using MineJason.Colors;

public class ChatColorTests
{
    [Test]
    public void RgbChatColor_Parse()
    {
        Assert.Multiple(() =>
        {
            Assert.That(RgbChatColor.TryParse("#00ff00", out var result));
            Assert.That(result, Is.EqualTo(new RgbChatColor(0x00, 0xff, 0x00)));
        });
    }

    [Test]
    public void RgbChatColor_Serialize()
    {
        Assert.That(JsonSerializer.Serialize(new RgbChatColor(0x00, 0xff, 0xff)),
            // ReSharper disable once StringLiteralTypo
            Is.EqualTo("\"#00ffff\""));
    }
    
    [Test]
    public void RgbChatColor_SerializeAsChatColor()
    {
        Assert.That(JsonSerializer.Serialize((IChatColor)new RgbChatColor(0x00, 0xff, 0xff)),
            // ReSharper disable once StringLiteralTypo
            Is.EqualTo("\"#00ffff\""));
    }
    
    [Test]
    public void RgbChatColor_Deserialize()
    {
        // ReSharper disable once StringLiteralTypo   
        Assert.That(JsonSerializer.Deserialize<RgbChatColor>("\"#00ffff\""),
            Is.EqualTo(new RgbChatColor(0x00, 0xff, 0xff)));
    }
    
    [Test]
    public void RgbChatColor_DeserializeAsChatColor()
    {
        // ReSharper disable once StringLiteralTypo   
        Assert.That(JsonSerializer.Deserialize<IChatColor>("\"#00ffff\""),
            Is.EqualTo(new RgbChatColor(0x00, 0xff, 0xff)));
    }
}