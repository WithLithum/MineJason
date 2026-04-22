// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using JetBrains.Annotations;
using MineJason.Serialization.TextJson;

namespace MineJason.Text.Behaviour.Hover;

/// <summary>
/// Represents a hover event that shows a text component in tooltip.
/// </summary>
[PublicAPI]
[JsonConverter(typeof(HoverEventConverter))]
public sealed class ShowTextHoverEvent : HoverEvent, IEquatable<ShowTextHoverEvent>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ShowTextHoverEvent"/>.
    /// </summary>
    public ShowTextHoverEvent()
    {
    }

    /// <summary>
    /// Initializes a new instance of <see cref="ShowTextHoverEvent"/> with the specified contents.
    /// </summary>
    /// <param name="contents">The contents of the tooltip.</param>
    [SetsRequiredMembers]
    public ShowTextHoverEvent(TextComponent contents)
    {
        Contents = contents;
    }

    /// <summary>
    /// Gets the contents of the tooltip.
    /// </summary>
    [JsonPropertyName("contents")]
    public required TextComponent Contents { get; init; }

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