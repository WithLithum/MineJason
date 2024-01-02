namespace MineJason.Events.Hover;

using System.Text.Json.Serialization;

/// <summary>
/// Represents a show text hover event, that shows a chat component as a tooltip.
/// </summary>
/// <param name="contents">The contents of the tooltip.</param>
public sealed class ShowTextHoverEvent(ChatComponent contents) : HoverEvent, IEquatable<ShowTextHoverEvent>
{
    /// <summary>
    /// Gets the contents of the tooltip.
    /// </summary>
    [JsonPropertyName("contents")]
    public ChatComponent Contents { get; } = contents;

    /// <inheritdoc />
    public bool Equals(ShowTextHoverEvent? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Contents.Equals(other.Contents);
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        return ReferenceEquals(this, obj) || obj is ShowTextHoverEvent other && Equals(other);
    }

    /// <inheritdoc />
    public override bool Equals(HoverEvent? other)
    {
        return other is ShowTextHoverEvent e && Equals(e);
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        return Contents.GetHashCode();
    }
}