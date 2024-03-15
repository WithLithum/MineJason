// Copyright (c) WithLithum & contributors 2023-2024. All rights reserved.
// Licensed under the GNU Lesser General Public License, either version 3 or
// (at your opinion) any later version.

namespace MineJason.Items.Banners;

using JetBrains.Annotations;
using MineJason.SNbt.Values;
using MineJason;

/// <summary>
/// Represents a banner pattern.
/// </summary>
[PublicAPI]
public record struct BannerPattern : ISNbtValue
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BannerPattern"/> class.
    /// </summary>
    /// <param name="color">The color.</param>
    /// <param name="pattern">The pattern.</param>
    public BannerPattern(string color, ResourceLocation pattern)
    {
        Color = color;
        Pattern = pattern;
    }
    
    /// <summary>
    /// Gets or sets the color of this pattern.
    /// </summary>
    public string Color { get; set; }
    
    /// <summary>
    /// Gets or sets the type of this pattern.
    /// </summary>
    public ResourceLocation Pattern { get; set; }

    /// <summary>
    /// Converts this instance to its string NBT equivalent.
    /// </summary>
    /// <returns>The converted string.</returns>
    public string ToSNbtString()
    {
        return new SNbtCompound()
        {
            { "color", new SNbtStringValue(Color) },
            { "pattern", new SNbtStringValue(Pattern.ToString()) }
        }.ToSNbtString();
    }
}