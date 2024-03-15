// Copyright (c) WithLithum & contributors 2023-2024. All rights reserved.
// Licensed under the GNU Lesser General Public License, either version 3 or
// (at your opinion) any later version.

namespace MineJason.Items;

using JetBrains.Annotations;
using MineJason.SNbt.Values;

/// <summary>
/// Represents an item component, where its value is an NBT value.
/// </summary>
/// <typeparam name="T">The value.</typeparam>
public abstract class NbtValueItemComponent<T> : IItemComponent
    where T: ISNbtValue
{
    /// <summary>
    /// Initialises a new instance of the <see cref="NbtValueItemComponent{T}"/> class.
    /// </summary>
    /// <param name="value">The value.</param>
    protected NbtValueItemComponent(T value)
    {
        Value = value;
    }
    
    /// <summary>
    /// Gets or sets the value of this instance.
    /// </summary>
    [PublicAPI]
    public T Value { get; }
    
    /// <summary>
    /// Converts this instance to its string NBT representation.
    /// </summary>
    /// <returns>The string representation.</returns>
    public string GetString()
    {
        return Value.ToSNbtString();
    }

    /// <inheritdoc />
    public bool IsValid()
    {
        return true;
    }
}