namespace MineJason.Events;

public sealed class OpenUrlClickEvent(Uri value) : ClickEvent, IEquatable<OpenUrlClickEvent>
{
    public Uri Value { get; } = value;

    public bool Equals(OpenUrlClickEvent? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Value.Equals(other.Value);
    }

    public override bool Equals(object? obj)
    {
        return ReferenceEquals(this, obj) || obj is OpenUrlClickEvent other && Equals(other);
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }
}