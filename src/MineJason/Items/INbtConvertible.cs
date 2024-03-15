// Copyright (c) WithLithum & contributors 2023-2024. All rights reserved.
// Licensed under the GNU Lesser General Public License, either version 3 or
// (at your opinion) any later version.

namespace MineJason.Items;

using MineJason.Data;
using MineJason.SNbt.Values;

/// <summary>
/// Represents a type that can be converted to NBT.
/// </summary>
public interface INbtConvertible
{
    /// <summary>
    /// Converts this instance to NBT.
    /// </summary>
    /// <returns>The NBT.</returns>
    public ISNbtValue ToNbt();
}