﻿// Copyright (c) WithLithum & contributors 2023-2024. All rights reserved.
// Licensed under the GNU Lesser General Public License, either version 3 or
// (at your opinion) any later version.

namespace MineJason.Events.Hover;

using System.Text.Json.Serialization;

/// <summary>
/// Represents a hover event, an event triggered when the player puts their mouse pointer over the text
/// being displayed.
/// </summary>
[JsonPolymorphic(TypeDiscriminatorPropertyName = "action")]
[JsonDerivedType(typeof(ShowTextHoverEvent), "show_text")]
[JsonDerivedType(typeof(ShowEntityHoverEvent), "show_entity")]
[JsonDerivedType(typeof(ShowItemHoverEvent), "show_item")]
public abstract class HoverEvent : IEquatable<HoverEvent>
{
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
