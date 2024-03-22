// Copyright (c) WithLithum & contributors 2023-2024. All rights reserved.
// Licensed under the GNU Lesser General Public License, either version 3 or
// (at your opinion) any later version.

namespace MineJason.Items;

using JetBrains.Annotations;

/// <summary>
/// Represents an item component that specifies the maximum stack size of an
/// item stack.
/// </summary>
[PublicAPI]
public sealed class MaxStackSizeItemComponent : PositiveNumberItemComponent
{
    /// <summary>
    /// The registered type of this data component.
    /// </summary>
    public static readonly ResourceLocation Type = 
        new("minecraft", "max_stack_size");

    /// <summary>
    /// Initialises a new instance of the <see cref="MaxStackSizeItemComponent"/> class.
    /// </summary>
    /// <param name="value">The value.</param>
    public MaxStackSizeItemComponent(int value) : base(value)
    {
    }

    /// <inheritdoc />
    public override bool IsValid()
    {
        return Value is > 0 and < 100;
    }
}