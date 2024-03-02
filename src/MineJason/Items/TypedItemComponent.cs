// Copyright (c) WithLithum & contributors 2023-2024. All rights reserved.
// Licensed under the GNU Lesser General Public License, either version 3 or 
// (at your opinion) any later version.

namespace MineJason.Items;

/// <summary>
/// Represents a typed item component with a single value.
/// </summary>
/// <typeparam name="T">The type of the value.</typeparam>
public abstract class TypedItemComponent<T> : IItemComponent
{
    /// <summary>
    /// Initialises a new instance of the <see cref="TypedItemComponent{T}"/> class.
    /// </summary>
    /// <param name="value">The value.</param>
    protected TypedItemComponent(T value)
    {
        Value = value;
    }

    /// <summary>
    /// Gets or sets the value of this instance.
    /// </summary>
    public T Value { get; set; }

    /// <inheritdoc/>
    public virtual string GetString()
    {
        return Value?.ToString() ?? "<null>";
    }

    /// <inheritdoc/>
    public abstract bool IsValid();
}
