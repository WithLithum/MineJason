// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: LGPL-3.0-or-later

namespace MineJason.Events.Hover;

using MineJason.Serialization.TextJson;
using System.Text.Json.Serialization;

/// <summary>
/// Represents a hover event, an event triggered when the player puts their mouse pointer over the
/// text being displayed.
/// </summary>
[JsonConverter(typeof(HoverEventConverter))]
public abstract class HoverEvent : IEquatable<HoverEvent>
{
    private protected HoverEvent()
    {
    }

    /// <inheritdoc />
    public abstract bool Equals(HoverEvent? other);

    /// <inheritdoc />
    public abstract override int GetHashCode();

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        return Equals(obj as HoverEvent);
    }
}
