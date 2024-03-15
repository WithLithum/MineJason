// Copyright (c) WithLithum & contributors 2023-2024. All rights reserved.
// Licensed under the GNU Lesser General Public License, either version 3 or
// (at your opinion) any later version.

namespace MineJason.Items.Banners;

using JetBrains.Annotations;
using MineJason.SNbt.Values;

/// <summary>
/// Represents a collection of banner patterns.
/// </summary>
[PublicAPI]
public class BannerPatternsItemComponent : NbtValueItemComponent<SNbtListValue<BannerPattern>>
{
    /// <summary>
    /// The registered type of this data component.
    /// </summary>
    public static readonly ItemComponentType Type = new(typeof(CustomDataItemComponent),
        new ResourceLocation("minecraft", "custom_data"));

    /// <summary>
    /// Initialises a new instance of the <see cref="BannerPatternsItemComponent"/> class.
    /// </summary>
    public BannerPatternsItemComponent() : base([])
    {
    }
}