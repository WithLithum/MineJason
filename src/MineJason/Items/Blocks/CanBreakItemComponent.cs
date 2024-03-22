// Copyright (c) WithLithum & contributors 2023-2024. All rights reserved.
// Licensed under the GNU Lesser General Public License, either version 3 or
// (at your opinion) any later version.

namespace MineJason.Items.Blocks;

using JetBrains.Annotations;

/// <summary>
/// Represents a component that defines the blocks that the item may break. Does not override
/// tools properties and block break-ability.
/// </summary>
[PublicAPI]
public class CanBreakItemComponent : InteractivePredicateItemComponent
{
    /// <summary>
    /// The registered type of this data component.
    /// </summary>
    public static readonly ResourceLocation Type =
        new("minecraft", "can_break");
}