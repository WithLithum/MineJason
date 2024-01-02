﻿namespace MineJason.Events;

/// <summary>
/// Represents an open URL click event, that shows a prompt to open a URL in the default browser.
/// </summary>
/// <param name="value">The URL to open.</param>
public sealed class OpenUrlClickEvent(Uri value) : ClickEvent, IEquatable<OpenUrlClickEvent>
{
    /// <summary>
    /// Gets the URL to open.
    /// </summary>
    public Uri Value { get; } = value;

    /// <inheritdoc />
    public bool Equals(OpenUrlClickEvent? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Value.Equals(other.Value);
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        return ReferenceEquals(this, obj) || obj is OpenUrlClickEvent other && Equals(other);
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }
}