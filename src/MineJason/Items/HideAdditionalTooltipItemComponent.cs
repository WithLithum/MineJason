// Copyright (c) WithLithum & contributors 2023-2024. All rights reserved.
// Licensed under the GNU Lesser General Public License, either version 3 or
// (at your opinion) any later version.

namespace MineJason.Items;

/// <summary>
/// Represents an item component that, when present, hides additional information from
/// the item tooltip.
/// </summary>
public class HideAdditionalTooltipItemComponent : EmptyItemComponent
{
    /// <summary>
    /// The registered type of this data component.
    /// </summary>
    public static readonly ItemComponentType Type = new(typeof(CustomDataItemComponent),
        new ResourceLocation("minecraft", "hide_additional_tooltip"));    
}