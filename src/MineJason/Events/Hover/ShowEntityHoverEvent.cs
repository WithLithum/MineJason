namespace MineJason.Events.Hover;

public sealed class ShowEntityHoverEvent(string type, Guid id, ChatComponent? name = null) : HoverEvent, IEquatable<ShowEntityHoverEvent>
{
    public ChatComponent? Name { get; } = name;

    public string Type { get; } = type;

    public Guid Id { get; } = id;

    public bool Equals(ShowEntityHoverEvent? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Equals(Name, other.Name) && Type == other.Type && Id.Equals(other.Id);
    }

    public override bool Equals(object? obj)
    {
        return ReferenceEquals(this, obj) || obj is ShowEntityHoverEvent other && Equals(other);
    }

    public override bool Equals(HoverEvent? other)
    {
        return other is ShowEntityHoverEvent e && Equals(e);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Name, Type, Id);
    }
}