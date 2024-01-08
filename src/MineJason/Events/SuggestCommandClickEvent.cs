// Copyright (c) WithLithum & contributors 2023-2024. All rights reserved.
// Licensed under the GNU Lesser General Public License, either version 3 or
// (at your opinion) any later version.

namespace MineJason.Events;

/// <summary>
/// Represents a click event that enters the specified text into the chat box.
/// </summary>
/// <param name="value">The text to enter into the chat box.</param>
public sealed class SuggestCommandClickEvent(string value) : ClickEvent, IEquatable<SuggestCommandClickEvent>
{
    /// <summary>
    /// Gets the text to enter into the chat box.
    /// </summary>
    public string Value { get; } = value;

    /// <inheritdoc />
    public bool Equals(SuggestCommandClickEvent? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Value == other.Value;
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        return ReferenceEquals(this, obj) || obj is SuggestCommandClickEvent other && Equals(other);
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }
}
