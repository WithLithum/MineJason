// Copyright (c) WithLithum & contributors 2023-2024. All rights reserved.
// Licensed under the GNU Lesser General Public License, either version 3 or
// (at your opinion) any later version.

namespace MineJason.Items;

using JetBrains.Annotations;

/// <summary>
/// Represents an item component that, when present, prevents the dropped entity form of this
/// item from being destroyed by fire or lava.
/// </summary>
[PublicAPI]
public class FireResistantItemComponent : EmptyItemComponent
{
    /// <summary>
    /// The registered type of this data component.
    /// </summary>
    public static readonly ResourceLocation Type = 
        new("minecraft", "fire_resistant");   
}