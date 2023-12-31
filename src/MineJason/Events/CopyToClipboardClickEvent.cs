namespace MineJason.Events;

public sealed class CopyToClipboardClickEvent(string value) : ClickEvent, IEquatable<CopyToClipboardClickEvent>
{
    public string Value { get; } = value;

    public bool Equals(CopyToClipboardClickEvent? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Value == other.Value;
    }

    public override bool Equals(object? obj)
    {
        return ReferenceEquals(this, obj) || obj is CopyToClipboardClickEvent other && Equals(other);
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }
}
