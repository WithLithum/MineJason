// Copyright (c) WithLithum & contributors 2023-2024. All rights reserved.
// Licensed under the GNU Lesser General Public License, either version 3 or
// (at your opinion) any later version.

namespace MineJason.Events.Hover;

using System.Text.Json.Serialization;
using JetBrains.Annotations;

/// <summary>
/// Represents a show entity hover event, a hover event that displays entity type and GUID information
/// in a tooltip that is shown.
/// </summary>
/// <param name="type">The type of the entity.</param>
/// <param name="id">The unique identifier of the entity.</param>
/// <param name="name">The name of the entity to show.</param>
[PublicAPI]
public sealed class ShowEntityHoverEvent(ResourceLocation type, Guid id, ChatComponent? name = null) : HoverEvent, IEquatable<ShowEntityHoverEvent>
{
    /// <summary>
    /// Gets the name of the entity to show as.
    /// </summary>
    [JsonPropertyName("name")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public ChatComponent? Name { get; } = name;

    /// <summary>
    /// Gets the type of the entity.
    /// </summary>
    [JsonPropertyName("type")]
    public ResourceLocation Type { get; } = type;

    /// <summary>
    /// Gets the unique identifier of this entity.
    /// </summary>
    [JsonPropertyName("id")]
    public Guid Id { get; } = id;

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