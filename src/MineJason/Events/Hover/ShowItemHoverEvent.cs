namespace MineJason.Events.Hover;

public sealed class ShowItemHoverEvent(string id, int count, string nbt) : HoverEvent
{
    public string Id { get; set; } = id;
    public int Count { get; set; } = count;
    public string Nbt { get; set; } = nbt;
}
