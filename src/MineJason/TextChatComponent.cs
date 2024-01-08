// Copyright (c) WithLithum & contributors 2023-2024. All rights reserved.
// Licensed under the GNU Lesser General Public License, either version 3 or
// (at your opinion) any later version.

namespace MineJason;

/// <summary>
/// Represents a chat component that displays a text.
/// </summary>
/// <param name="text">The text to display.</param>
public sealed class TextChatComponent(string text) : ChatComponent("text"), IEquatable<TextChatComponent>
{
    /// <summary>
    /// Gets the text to display.
    /// </summary>
    public string Text { get; } = text;

    /// <inheritdoc />
    public bool Equals(TextChatComponent? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return Text == other.Text;
    }

    /// <inheritdoc />
    public override bool Equals(ChatComponent? other)
    {
        return other is TextChatComponent component && StyleEquals(this, component) && Equals(component);
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        return ReferenceEquals(this, obj) || obj is TextChatComponent other && Equals(other);
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        return Text.GetHashCode();
    }

    /// <inheritdoc />
    public override string ToString()
    {
        return $"({base.ToString()} | text{{{Text}}})";
    }
}