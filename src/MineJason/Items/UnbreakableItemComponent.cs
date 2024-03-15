// Copyright (c) WithLithum & contributors 2023-2024. All rights reserved.
// Licensed under the GNU Lesser General Public License, either version 3 or
// (at your opinion) any later version.

namespace MineJason.Items;

/// <summary>
/// Represents an item component that, when present, prevents an item from being damaged.
/// This class cannot be inherited.
/// </summary>
public sealed class UnbreakableItemComponent : TooltipFlagItemComponent
{
    /// <summary>
    /// The registered type of this data component.
    /// </summary>
    public static readonly ItemComponentType Type = new(typeof(RepairCostItemComponent),
        new ResourceLocation("minecraft", "unbreakable"));
}