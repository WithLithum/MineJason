namespace MineJason;

public sealed class TextChatComponent(string text) : ChatComponent("text")
{
    public string Text { get; set; } = text;
}