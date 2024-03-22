// Copyright (c) WithLithum & contributors 2023-2024. All rights reserved.
// Licensed under the GNU Lesser General Public License, either version 3 or
// (at your opinion) any later version.

namespace MineJason.Items.Foods;

using JetBrains.Annotations;
using MineJason.Data.Nbt;
using MineJason.SNbt;
using MineJason.SNbt.Values;

/// <summary>
/// Represents information regarding a food.
/// </summary>
[PublicAPI]
public record FoodInfo : INbtConvertible
{
    /// <summary>
    /// Gets or sets the food points restored by eating this item.
    /// </summary>
    public int Nutrition { get; set; }
    
    /// <summary>
    /// Gets or sets the saturation restored by eating this item and having this item
    /// restoring food point to almost full or completely full. 
    /// </summary>
    public float SaturationModifier { get; set; }
    
    /// <summary>
    /// Gets or sets a value indicating whether this item can be fed to wolves.
    /// </summary>
    public bool IsMeat { get; set; }
    
    /// <summary>
    /// Gets or sets a value indicating whether this item can be eaten even if the hunger
    /// bar is full.
    /// </summary>
    public bool CanAlwaysEat { get; set; }

    /// <summary>
    /// Gets or sets the time, in seconds, needed to eat this item.
    /// </summary>
    public float EatSeconds { get; set; } = 1.6f;

    /// <summary>
    /// Gets a list of effects to apply when eaten.
    /// </summary>
    public SNbtListValue<FoodEffectInfo> Effects { get; } = [];
    
    /// <inheritdoc />
    public ISNbtValue ToNbt()
    {
        return new SNbtObjectBuilder()
            .Property("nutrition", Nutrition)
            .Property("saturation_modifier", SaturationModifier)
            .PropertyNotDefault("is_meat", IsMeat, false)
            .PropertyNotDefault("can_always_eat", CanAlwaysEat, false)
            .PropertyNotDefault("eat_seconds", EatSeconds, 1.6f)
            .PropertyNotEmpty<SNbtListValue<FoodEffectInfo>, FoodEffectInfo>("effects", Effects);
    }
}