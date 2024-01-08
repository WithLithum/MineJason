// Copyright (c) WithLithum & contributors 2023-2024. All rights reserved.
// Licensed under the GNU Lesser General Public License, either version 3 or
// (at your opinion) any later version.

namespace MineJason;
using System;

/// <summary>
/// Represents a chat component that displays as the key associated with the specified key binding entry
/// of the client that this component is being presented.
/// </summary>
/// <param name="keybind">The ID of the key binding entry.</param>
public sealed class KeybindChatComponent(string keybind) : ChatComponent("keybind"),
    IEquatable<KeybindChatComponent>
{
    /// <summary>
    /// Gets the ID of the key binding entry.
    /// </summary>
    public string Keybind { get; } = keybind;

    /// <inheritdoc />
    public bool Equals(KeybindChatComponent? other)
    {
        if (ReferenceEquals(null, other))
        {
            return false;
        }

        if (ReferenceEquals(this, other))
        {
            return true;
        }

        return base.Equals(other) && Keybind == other.Keybind;
    }

    /// <inheritdoc />
    public override bool Equals(ChatComponent? other)
    {
        return base.Equals(other) && other is KeybindChatComponent component && Equals(component);
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        return ReferenceEquals(this, obj) || obj is KeybindChatComponent other && Equals(other);
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        return Keybind.GetHashCode();
    }
}
