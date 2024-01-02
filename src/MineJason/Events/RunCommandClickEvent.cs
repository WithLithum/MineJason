namespace MineJason.Events;

/// <summary>
/// Represents a click event that executes a command.
/// </summary>
/// <param name="value">The command to execute.</param>
public sealed class RunCommandClickEvent(string value) : ClickEvent, IEquatable<RunCommandClickEvent>
{
    /// <summary>
    /// Gets the command to execute.
    /// </summary>
    public string Value { get; } = value;

    /// <inheritdoc />
    public bool Equals(RunCommandClickEvent? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Value == other.Value;
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        return ReferenceEquals(this, obj) || obj is RunCommandClickEvent other && Equals(other);
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }
}
