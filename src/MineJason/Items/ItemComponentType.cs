// Copyright (c) WithLithum & contributors 2023-2024. All rights reserved.
// Licensed under the GNU Lesser General Public License, either version 3 or
// (at your opinion) any later version.

namespace MineJason.Items;

/// <summary>
/// Represents a type of item component.
/// </summary>
public readonly struct ItemComponentType
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ItemComponentType"/> class.
    /// </summary>
    /// <param name="acceptedType">The accepted type of the value.</param>
    public ItemComponentType(Type acceptedType)
    {
        AcceptedType = acceptedType;
    }

    /// <summary>
    /// Gets the type of that is accepted.
    /// </summary>
    public Type AcceptedType { get; }
}