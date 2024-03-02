// Copyright (c) WithLithum & contributors 2023-2024. All rights reserved.
// Licensed under the GNU Lesser General Public License, either version 3 or
// (at your opinion) any later version.

namespace MineJason.Items;

using MineJason.Utilities;

/// <summary>
/// Provides a base for item components that accepts a positive number.
/// </summary>
public abstract class PositiveNumberItemComponent : IItemComponent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PositiveNumberItemComponent"/> class.
    /// </summary>
    /// <param name="value">The value.</param>
    protected PositiveNumberItemComponent(int value)
    {
        Value = value;
    }
    
    /// <summary>
    /// Gets or sets the value.
    /// </summary>
    public int Value { get; set; }

    /// <inheritdoc />
    public string GetString()
    {
        return Value.ToStringNeutral();
    }

    /// <inheritdoc />
    public bool IsValid()
    {
        return Value >= 0;
    }
}