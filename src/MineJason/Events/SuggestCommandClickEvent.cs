namespace MineJason.Events;

public sealed class SuggestCommandClickEvent(string value) : ClickEvent, IEquatable<SuggestCommandClickEvent>
{
    public string Value { get; } = value;

    public bool Equals(SuggestCommandClickEvent? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Value == other.Value;
    }

    public override bool Equals(object? obj)
    {
        return ReferenceEquals(this, obj) || obj is SuggestCommandClickEvent other && Equals(other);
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }
}
