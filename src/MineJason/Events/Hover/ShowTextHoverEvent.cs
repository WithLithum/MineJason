namespace MineJason.Events.Hover;

public sealed class ShowTextHoverEvent(ChatComponent contents) : HoverEvent, IEquatable<ShowTextHoverEvent>
{
    public ChatComponent Contents { get; } = contents;

    public bool Equals(ShowTextHoverEvent? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Contents.Equals(other.Contents);
    }

    public override bool Equals(object? obj)
    {
        return ReferenceEquals(this, obj) || obj is ShowTextHoverEvent other && Equals(other);
    }

    public override bool Equals(HoverEvent? other)
    {
        return other is ShowTextHoverEvent e && Equals(e);
    }

    public override int GetHashCode()
    {
        return Contents.GetHashCode();
    }
}