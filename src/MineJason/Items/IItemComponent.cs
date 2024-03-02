// Copyright (c) WithLithum & contributors 2023-2024. All rights reserved.
// Licensed under the GNU Lesser General Public License, either version 3 or
// (at your opinion) any later version.

namespace MineJason.Items;

/// <summary>
/// Represents an item component.
/// </summary>
public interface IItemComponent
{
    /// <summary>
    /// Converts this instance to its value representation.
    /// </summary>
    /// <returns>The value representation. Must be parse-able in game.</returns>
    string GetString();

    /// <summary>
    /// Determines whether this instance is valid.
    /// </summary>
    /// <returns><see langword="true"/> if this instance is valid; otherwise, <see langword="false"/>.</returns>
    bool IsValid();
}