// Copyright (c) WithLithum & contributors 2023-2024. All rights reserved.
// Licensed under the GNU Lesser General Public License, either version 3 or
// (at your opinion) any later version.

namespace MineJason.Items;

using JetBrains.Annotations;

/// <summary>
/// Represents an item component that sets the damage value of a damage-able item. This class cannot be inherited.
/// </summary>
[PublicAPI]
public sealed class DamageItemComponent : PositiveNumberItemComponent
{
    /// <summary>
    /// The identifier of this data component.
    /// </summary>
    public static readonly ResourceLocation Id = new("minecraft", "damage");
    
    /// <summary>
    /// Initializes a new instance of the <see cref="DamageItemComponent"/> class.
    /// </summary>
    /// <param name="value">The value.</param>
    public DamageItemComponent(int value) : base(value)
    {
    }
}