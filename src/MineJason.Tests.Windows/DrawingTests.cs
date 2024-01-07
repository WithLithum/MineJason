namespace MineJason.Tests.Windows;

using System.Drawing;
using MineJason.Extensions.Drawing;

public class DrawingTests
{
    [Test]
    public void DrawingChatColor_Parse()
    {
        Assert.Multiple(() =>
        {
            Assert.That(DrawingChatColor.TryParse("#00ff00", out var result));
            Assert.That(result, Is.EqualTo(new DrawingChatColor(0x00, 0xff, 0x00)));
        });
    }
}