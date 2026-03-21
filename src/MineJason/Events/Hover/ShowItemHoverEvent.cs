// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: LGPL-3.0-or-later

namespace MineJason.Events.Hover;

using System.Text.Json.Serialization;
using JetBrains.Annotations;
using MineJason.Data;
using MineJason.Data.Nbt;

/// <summary>
/// Represents a show item hover event, that shows an item tooltip.
/// </summary>
/// <remarks>
/// <note type="warning">
/// This type is obsolete. Use <see cref="MineJason.Text.Behaviour.Hover.ShowItemHoverEvent"/>
/// instead.
/// </note>
/// </remarks>
[PublicAPI]
[Obsolete("This ShowItemHoverEvent implementation only supports Minecraft versions up to and until 1.20.4." +
          "Use MineJason.Text.Behaviour.Hover.ShowItemHoverEvent instead.")]
public sealed class ShowItemHoverEvent : HoverEvent, IEquatable<ShowItemHoverEvent>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ShowItemHoverEvent"/> class.
    /// </summary>
    /// <param name="id">The identifier of the type of the item.</param>
    /// <param name="count">The count of the item.</param>
    /// <param name="nbt">The NBT data of the item.</param>
    /// <remarks>
    /// This hover event implementation only supports Minecraft versions up to and until 1.20.4.
    /// </remarks>
    public ShowItemHoverEvent(ResourceLocation id, int? count = null, INbtDataProvider? nbt = null)
    {
        Id = id;
        Count = count;
        Nbt = nbt;
    }
    
    /// <summary>
    /// Gets the identifier of the item.
    /// </summary>
    [JsonPropertyName("id")]
    public ResourceLocation Id { get; }

    /// <summary>
    /// Gets the count of item.
    /// </summary>
    [JsonPropertyName("count")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? Count { get; }

    /// <summary>
    /// Gets the NBT of the item.
    /// </summary>
    [JsonPropertyName("tag")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public INbtDataProvider? Nbt { get; }

    /// <inheritdoc />
    public bool Equals(ShowItemHoverEvent? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Id == other.Id
            && Count == other.Count
            && Equals(Nbt, other.Nbt);
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
