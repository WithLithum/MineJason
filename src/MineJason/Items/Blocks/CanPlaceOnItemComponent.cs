// Copyright (c) WithLithum & contributors 2023-2024. All rights reserved.
// Licensed under the GNU Lesser General Public License, either version 3 or
// (at your opinion) any later version.

namespace MineJason.Items.Blocks;

using JetBrains.Annotations;

/// <summary>
/// Represents a component that specifies what block can the block item be placed on, and what block
/// may certain item be used on.
/// </summary>
[PublicAPI]
public class CanPlaceOnItemComponent : InteractivePredicateItemComponent
{
    /// <summary>
    /// The registered type of this data component.
    /// </summary>
    public static readonly ResourceLocation Type = 
        new("minecraft", "can_place_on");
}