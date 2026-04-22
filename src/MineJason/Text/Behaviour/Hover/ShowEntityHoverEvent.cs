// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using JetBrains.Annotations;
using MineJason.Serialization.TextJson;

namespace MineJason.Text.Behaviour.Hover;

/// <summary>
/// Represents a hover event that displays technical information about an entity.
/// </summary>
[PublicAPI]
[JsonConverter(typeof(HoverEventConverter))]
public sealed class ShowEntityHoverEvent : HoverEvent, IEquatable<ShowEntityHoverEvent>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ShowEntityHoverEvent"/> class.
    /// </summary>
    public ShowEntityHoverEvent()
    {
    }

    /// <summary>
    /// Initializes a new instance of <see cref="ShowEntityHoverEvent"/> with the specified entity
    /// type, identifier and optionally a text component to display as name of that entity.
    /// </summary>
    /// <param name="type">The type of the entity.</param>
    /// <param name="id">The unique identifier of the entity.</param>
    /// <param name="name">The name of the entity to show.</param>
    [SetsRequiredMembers]
    public ShowEntityHoverEvent(ResourceLocation type, Guid id, TextComponent? name = null)
    {
        Type = type;
        Id = id;
        Name = name;
    }

    /// <summary>
    /// Gets the name of the entity to show as.
    /// </summary>
    [JsonPropertyName("name")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public TextComponent? Name { get; init; }

    /// <summary>
    /// Gets the type of the entity.
    /// </summary>
    [JsonPropertyName("type")]
    public required ResourceLocation Type { get; init; }

    /// <summary>
    /// Gets the unique identifier of this entity.
    /// </summary>
    [JsonPropertyName("id")]
    public required Guid Id { get; init; }

    /// <inheritdoc />
    public bool Equals(ShowEntityHoverEvent? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Equals(Name, other.Name) && Type == other.Type && Id.Equals(other.Id);
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        return ReferenceEquals(this, obj) || obj is ShowEntityHoverEvent other && Equals(other);
    }

    /// <inheritdoc />
    public override bool Equals(HoverEvent? other)
    {
        return other is ShowEntityHoverEvent e && Equals(e);
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        return HashCode.Combine(Name, Type, Id);
    }
}