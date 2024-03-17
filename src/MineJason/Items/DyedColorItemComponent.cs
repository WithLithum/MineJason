﻿// Copyright (c) WithLithum & contributors 2023-2024. All rights reserved.
// Licensed under the GNU Lesser General Public License, either version 3 or 
// (at your opinion) any later version.

namespace MineJason.Items;

using JetBrains.Annotations;
using MineJason.Colors;
using MineJason.Utilities;

/// <summary>
/// Represents an item component that sets the dyed color for dye-able items.
/// This class cannot be inherited.
/// </summary>
[PublicAPI]
public sealed class DyedColorItemComponent : TypedItemComponent<RgbChatColor>
{
    /// <summary>
    /// The registered type of this data component.
    /// </summary>
    public static readonly ResourceLocation Type = 
        new("minecraft", "dyed_color");

    /// <summary>
    /// Initializes a new instance of the <see cref="RgbChatColor"/> class.
    /// </summary>
    /// <param name="value">The color.</param>
    public DyedColorItemComponent(RgbChatColor value) : base(value)
    {
    }

    /// <inheritdoc/>
    public override bool IsValid()
    {
        return true;
    }

    /// <inheritdoc/>
    public override string ToString()
    {
        return Value.ToNbtColor().ToStringNeutral();
    }
}
