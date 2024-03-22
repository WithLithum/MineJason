// Copyright (c) WithLithum & contributors 2023-2024. All rights reserved.
// Licensed under the GNU Lesser General Public License, either version 3 or
// (at your opinion) any later version.

namespace MineJason.Items.Foods;

using JetBrains.Annotations;

/// <summary>
/// Represents a component that specifies an item stack as food, as well as specifying
/// food properties of such item stack. This class cannot be inherited.
/// </summary>
[PublicAPI]
public sealed class FoodItemComponent : ObjectItemComponent<FoodInfo>
{
    /// <summary>
    /// The registered type of this data component.
    /// </summary>
    public static readonly ResourceLocation Type =
        new("minecraft", "food");
    
    /// <summary>
    /// Initialises a new instance of the <see cref="FoodItemComponent"/> class.
    /// </summary>
    /// <param name="value">The value.</param>
    public FoodItemComponent(FoodInfo value) : base(value)
    {
    }
}