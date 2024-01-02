namespace MineJason.Events;

/// <summary>
/// Represents a copy to clipboard click event, that copies the specified text to the clipboard.
/// </summary>
/// <param name="value">The text to copy to the clipboard.</param>
public sealed class CopyToClipboardClickEvent(string value) : ClickEvent, IEquatable<CopyToClipboardClickEvent>
{
    /// <summary>
    /// Gets the text to copy to the clipboard.
    /// </summary>
    public string Value { get; } = value;

    /// <inheritdoc />
    public bool Equals(CopyToClipboardClickEvent? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Value == other.Value;
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        return ReferenceEquals(this, obj) || obj is CopyToClipboardClickEvent other && Equals(other);
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }
}
