// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: LGPL-3.0-or-later

namespace MineJason.Data;

using System.Diagnostics.CodeAnalysis;
using MineJason.Data.Components;

/// <summary>
/// Represents an item stack.
/// </summary>
[SuppressMessage("Naming", "CA1711",
    Justification = "ItemStack is a thing which represents a stack of items in-game")]
public record DisplayItemStack
{
    /// <summary>
    /// Gets the identifier of the item type to display.
    /// </summary>
    public required ResourceLocation Id { get; init; }
    
    /// <summary>
    /// Gets the count of the items in the item stack to display.
    /// </summary>
    public required int Count { get; init; }
    
    /// <summary>
    /// Gets the dictionary that contains component patches to apply onto this instance.
    /// </summary>
    public IReadOnlyDictionary<ResourceLocation, IDataComponentValue>? Components { get; init; }
}