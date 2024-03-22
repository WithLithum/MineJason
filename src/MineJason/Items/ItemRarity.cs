// Copyright (c) WithLithum & contributors 2023-2024. All rights reserved.
// Licensed under the GNU Lesser General Public License, either version 3 or
// (at your opinion) any later version.

namespace MineJason.Items;

/// <summary>
/// Represents the internal rarity of value of an item which automatically controls the
/// default colour of item names.
/// </summary>
public enum ItemRarity
{
    /// <summary>
    /// The common rarity. Represented by <see cref="KnownColor.White"/>, and is represented by 
    /// <see cref="KnownColor.Aqua"/> when enchanted.
    /// </summary>
    Common,
    /// <summary>
    /// The uncommon rarity. Represented by <see cref="KnownColor.Yellow"/>, and is represented by
    /// <see cref="KnownColor.Aqua"/> when enchanted.
    /// </summary>
    Uncommon,
    /// <summary>
    /// The rare rarity. Represented by <see cref="KnownColor.Aqua"/>, and is represented by
    /// <see cref="KnownColor.LightPurple"/> when enchanted.
    /// </summary>
    Rare,
    /// <summary>
    /// The epic rarity. Represented by <see cref="KnownColor.LightPurple"/>.
    /// </summary>
    Epic
}