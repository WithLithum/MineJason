// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: LGPL-3.0-or-later

namespace MineJason.Text.Behaviour.Hover;

using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using MineJason.Data.Components;

/// <summary>
/// Represents a hover event that displays an item stack tooltip.
/// </summary>
[PublicAPI]
public sealed class ShowItemHoverEvent : HoverEvent, IEquatable<ShowItemHoverEvent>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ShowItemHoverEvent"/> class.
    /// </summary>
    public ShowItemHoverEvent()
    {
    }

    /// <summary>
    /// Initializes a new instance of <see cref="ShowItemHoverEvent"/> with the specified item
    /// data.
    /// </summary>
    /// <param name="id">The identifier of the item type.</param>
    /// <param name="count">The count of the item stack.</param>
    /// <param name="components">The data components of the item stack.</param>
    [SetsRequiredMembers]
    public ShowItemHoverEvent(ResourceLocation id,
        int count = -1,
        IReadOnlyDictionary<ResourceLocation, IDataComponentValue>? components = null)
    {
        Id = id;
        Count = count;
        Components = components;
    }

    /// <summary>
    /// Gets the identifier of the item type to display.
    /// </summary>
    public required ResourceLocation Id { get; init; }

    /// <summary>
    /// Gets the count of the item stack to display.
    /// </summary>
    public int? Count { get; init; }

    /// <summary>
    /// Gets the data components of the item to show.
    /// </summary>
    public IReadOnlyDictionary<ResourceLocation, IDataComponentValue>? Components { get; init; }
    
    /// <inheritdoc />
    public override int GetHashCode()
    {
        return HashCode.Combine(Id, Count, Components);
    }
    
    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        if (obj is null)
        {
            return false;
        }

        if (ReferenceEquals(this, obj))
        {
            return true;
        }

        if (obj.GetType() != GetType())
        {
            return false;
        }

        return Equals((ShowItemHoverEvent)obj);
    }

    /// <inheritdoc />
    public override bool Equals(HoverEvent? other)
    {
        return other is ShowItemHoverEvent si && Equals(si);
    }
    
    /// <inheritdoc />
    public bool Equals(ShowItemHoverEvent? other)
    {
        return other != null && Id == other.Id && Count == other.Count
               && other.Components == Components;
    }
}