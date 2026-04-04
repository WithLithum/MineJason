// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: LGPL-3.0-or-later

using System.Text.Json.Serialization;
using MineJason.Serialization.TextJson;

namespace MineJason.Text.Behaviour.Hover;

/// <summary>
/// Controls the content of the tooltip displayed when hovering over a displayed text component.
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
