﻿// Copyright (c) WithLithum & contributors 2023-2024. All rights reserved.
// Licensed under the GNU Lesser General Public License, either version 3 or
// (at your opinion) any later version.

namespace MineJason.Items;

using JetBrains.Annotations;

/// <summary>
/// Represents an item component that specifies the maximum (full amount of) damage value
/// of an item stack.
/// </summary>
[PublicAPI]
public class MaxDamageItemComponent : PositiveNumberItemComponent
{
    /// <summary>
    /// The registered type of this data component.
    /// </summary>
    public static readonly ResourceLocation Type = 
        new("minecraft", "max_damage");   
    
    /// <summary>
    /// Initialises a new instance of the <see cref="MaxDamageItemComponent"/> class.
    /// </summary>
    /// <param name="value"></param>
    public MaxDamageItemComponent(int value) : base(value)
    {
    }
}