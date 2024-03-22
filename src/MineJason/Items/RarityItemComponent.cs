// Copyright (c) WithLithum & contributors 2023-2024. All rights reserved.
// Licensed under the GNU Lesser General Public License, either version 3 or
// (at your opinion) any later version.

namespace MineJason.Items;

using JetBrains.Annotations;

/// <summary>
/// Represents an item component that specifies the rarity of an item stack.
/// </summary>
[PublicAPI]
public sealed class RarityItemComponent : EnumItemComponent<ItemRarity>
{
    /// <summary>
    /// The registered type of this data component.
    /// </summary>
    public static readonly ResourceLocation Type = 
        new("minecraft", "rarity");   
    
    /// <summary>
    /// Initialises a new instance of the <see cref="RarityItemComponent"/> class.
    /// </summary>
    /// <param name="value">The value.</param>
    public RarityItemComponent(ItemRarity value) : base(value)
    {
    }
}