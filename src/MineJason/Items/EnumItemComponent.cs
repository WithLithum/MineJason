// Copyright (c) WithLithum & contributors 2023-2024. All rights reserved.
// Licensed under the GNU Lesser General Public License, either version 3 or
// (at your opinion) any later version.

namespace MineJason.Items;

using System.Globalization;
using JetBrains.Annotations;

/// <summary>
/// Represents an item component that receives an enum as its value.
/// </summary>
/// <typeparam name="T">The enum.</typeparam>
public abstract class EnumItemComponent<T> : IItemComponent
    where T : Enum
{
    /// <summary>
    /// Initialise a new instance of the <see cref="EnumItemComponent{T}"/> class.
    /// </summary>
    /// <param name="value">The value.</param>
    protected EnumItemComponent(T value)
    {
        Value = value;
    }
    
    /// <summary>
    /// Gets or sets the value of this instance.
    /// </summary>
    [PublicAPI]
    public T Value { get; set; }

    /// <inheritdoc />
    public string GetString()
    {
        return Value.ToString().ToLower(CultureInfo.InvariantCulture);
    }

    /// <inheritdoc />
    public bool IsValid()
    {
        return true;
    }
}