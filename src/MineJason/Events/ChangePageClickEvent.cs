namespace MineJason.Events;

public sealed class ChangePageClickEvent(int value) : ClickEvent, IEquatable<ChangePageClickEvent>
{
    public int Value { get; } = value;

    public bool Equals(ChangePageClickEvent? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Value == other.Value;
    }

    public override bool Equals(object? obj)
    {
        return ReferenceEquals(this, obj) || obj is ChangePageClickEvent other && Equals(other);
    }

    public override int GetHashCode()
    {
        return Value;
    }
}