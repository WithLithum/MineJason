// Copyright (c) WithLithum & contributors 2023-2024. All rights reserved.
// Licensed under the GNU Lesser General Public License, either version 3 or
// (at your opinion) any later version.

namespace MineJason.Items;

/// <summary>
/// Represents an item component that does not contain any value. This type of components are used
/// as switch flags and works by detecting its presence.
/// </summary>
public abstract class EmptyItemComponent : IItemComponent
{
    /// <inheritdoc />
    public string GetString()
    {
        return "{}";
    }

    /// <inheritdoc />
    public bool IsValid()
    {
        return true;
    }
}