// Copyright (c) WithLithum & contributors 2023-2024. All rights reserved.
// Licensed under the GNU Lesser General Public License, either version 3 or
// (at your opinion) any later version.

namespace MineJason.Events.Hover;

using System.Text.Json.Serialization;
using JetBrains.Annotations;
using MineJason.Data;

/// <summary>
/// Represents a show item hover event, that shows an item tooltip.
/// </summary>
/// <param name="id">The identifier of the type of the item.</param>
/// <param name="count">The count of the item.</param>
/// <param name="nbt">The NBT data of the item.</param>
[PublicAPI]
public sealed class ShowItemHoverEvent(ResourceLocation id, int? count = null, NbtProvider? nbt = null) : HoverEvent, IEquatable<ShowItemHoverEvent>
{
    /// <summary>
    /// Gets the identifier of the item.
    /// </summary>
    [JsonPropertyName("id")]
    public ResourceLocation Id { get; } = id;

    /// <summary>
    /// Gets the count of item.
    /// </summary>
    [JsonPropertyName("count")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? Count { get; } = count;

    /// <summary>
    /// Gets the NBT of the item.
    /// </summary>
    [JsonPropertyName("tag")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public NbtProvider? Nbt { get; } = nbt;

    /// <inheritdoc />
    public bool Equals(ShowItemHoverEvent? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Id == other.Id && Count == other.Count && Nbt == other.Nbt;
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        return ReferenceEquals(this, obj) || obj is ShowItemHoverEvent other && Equals(other);
    }

    /// <inheritdoc />
    public override bool Equals(HoverEvent? other)
    {
        return other is ShowItemHoverEvent e && Equals(e);
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        return HashCode.Combine(Id, Count, Nbt);
    }
}
