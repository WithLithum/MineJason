namespace MineJason;

public sealed class TranslatableChatComponent(string translate, string? fallback = null) : ChatComponent("translatable")
{
    public string Translate { get; set; } = translate;

    public string? Fallback { get; set; } = fallback;

    public IList<ChatComponent>? With { get; set; }
}