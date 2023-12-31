namespace MineJason.Events.Hover;

public sealed class ShowTextHoverEvent(ChatComponent contents) : HoverEvent
{
    public ChatComponent Contents { get; set; } = contents;
}