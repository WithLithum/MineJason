// Copyright (c) WithLithum & contributors 2023-2024. All rights reserved.
// Licensed under the GNU Lesser General Public License, either version 3 or
// (at your opinion) any later version.

namespace MineJason.Items;

using JetBrains.Annotations;

/// <summary>
/// Represents an item component that stores an object.
/// </summary>
/// <typeparam name="T">The item component value.</typeparam>
public abstract class ObjectItemComponent<T> : IItemComponent
    where T : INbtConvertible
{
    /// <summary>
    /// Initialises a new instance of the <see cref="ObjectItemComponent{T}"/> class.
    /// </summary>
    /// <param name="value">The value.</param>
    protected ObjectItemComponent(T value)
    {
        Value = value;
    }
    
    /// <summary>
    /// Gets or sets the specified object stored in this component.
    /// </summary>
    [PublicAPI]
    public T Value { get; set; }

    /// <inheritdoc />
    public string GetString()
    {
        return Value.ToNbt().ToSNbtString();
    }

    /// <inheritdoc />
    public bool IsValid()
    {
        return true;
    }
}