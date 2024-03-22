// Copyright (c) WithLithum & contributors 2023-2024. All rights reserved.
// Licensed under the GNU Lesser General Public License, either version 3 or
// (at your opinion) any later version.

namespace MineJason.Items.Blocks;

using MineJason.SNbt;
using MineJason.SNbt.Values;

/// <summary>
/// Represents a dictionary of block states.
/// </summary>
public sealed class BlockStateDictionary : Dictionary<string, string>, INbtConvertible, ISNbtValue
{
    /// <summary>
    /// Converts this instance to its string representation.
    /// </summary>
    /// <returns>The string representation.</returns>
    public ISNbtValue ToNbt()
    {
        var result = new SNbtObjectBuilder();

        foreach (var pair in this)
        {
            result.Property(pair.Key, new SNbtStringValue(pair.Value));
        }

        return result;
    }

    /// <summary>
    /// Converts this instance to its NBT string representation.
    /// </summary>
    /// <returns>The string representation.</returns>
    public string ToSNbtString()
    {
        return ToNbt().ToSNbtString();
    }
}