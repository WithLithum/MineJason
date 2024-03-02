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
    /// <param name="maxEntryCount">The maximum number of entries.</param>
    public ItemComponentType(int maxEntryCount)
    {
        MaxEntryCount = maxEntryCount;
    }
    
    /// <summary>
    /// Gets the maximum number of entries of this component type can exist.
    /// </summary>
    public int MaxEntryCount { get; }
}