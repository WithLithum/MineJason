namespace MineJason.Events.Hover;

public sealed class ShowEntityHoverEvent(string type, Guid id) : HoverEvent
{
    public ChatComponent? Name { get; set; }

    public string Type { get; set; } = type;

    public Guid Id { get; set; } = id;
}