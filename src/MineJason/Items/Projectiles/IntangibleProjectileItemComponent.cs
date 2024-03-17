// Copyright (c) WithLithum & contributors 2023-2024. All rights reserved.
// Licensed under the GNU Lesser General Public License, either version 3 or
// (at your opinion) any later version.

namespace MineJason.Items.Projectiles;

using JetBrains.Annotations;

/// <summary>
/// Represents an item component that, when present, prevents the projectile launched
/// by the item from being picked up by a player.
/// This class cannot be inherited.
/// </summary>
[PublicAPI]
public sealed class IntangibleProjectileItemComponent : EmptyItemComponent
{
    /// <summary>
    /// The registered type of this data component.
    /// </summary>
    public static readonly ResourceLocation Type = 
        new("minecraft", "intangible_projectile");
}