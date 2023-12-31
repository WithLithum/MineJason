namespace MineJason.Events;

public sealed class RunCommandClickEvent(string value) : ClickEvent, IEquatable<RunCommandClickEvent>
{
    public string Value { get; } = value;

    public bool Equals(RunCommandClickEvent? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Value == other.Value;
    }

    public override bool Equals(object? obj)
    {
        return ReferenceEquals(this, obj) || obj is RunCommandClickEvent other && Equals(other);
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }
}
