// Copyright (c) WithLithum & contributors 2023-2024. All rights reserved.
// Licensed under the GNU Lesser General Public License, either version 3 or
// (at your opinion) any later version.

namespace MineJason.Items.Blocks;

using MineJason.Data;
using MineJason.Data.Nbt;
using MineJason.SNbt;
using MineJason.SNbt.Values;

/// <summary>
/// Represents a predicate for block interaction related components.
/// </summary>
public class InteractBlockPredicate : INbtConvertible, ISNbtValue, ISNbtWritable
{
    /// <summary>
    /// Gets the type/tag entries to match.
    /// </summary>
    public TypeTagCollection Blocks { get; } = [];
    
    /// <summary>
    /// Gets or sets the NBT to match.
    /// </summary>
    public SNbtCompound? Nbt { get; set; }

    /// <summary>
    /// Gets a dictionary of block states to match.
    /// </summary>
    public BlockStateDictionary BlockState { get; } = [];

    /// <summary>
    /// Converts this instance to its NBT representation.
    /// </summary>
    /// <returns>The NBT representation.</returns>
    public ISNbtValue ToNbt()
    {
        return new SNbtObjectBuilder()
            .Property("blocks", Blocks)
            .PropertyNullable("nbt", Nbt)
            .PropertyNotEmpty("state", BlockState);
    }

    /// <summary>
    /// Converts this instance to its String NBT representation.
    /// </summary>
    /// <returns>The String NBT representation.</returns>
    public string ToSNbtString()
    {
        return ToNbt().ToSNbtString();
    }

    /// <summary>
    /// Writes the data of this instance, in the String NBT form, to a <see cref="SNbtWriter"/>.
    /// </summary>
    /// <param name="writer">The writer to write to.</param>
    public void WriteTo(SNbtWriter writer)
    {
        new SNbtObjectBuilder()
            .Property("blocks", Blocks)
            .PropertyNullable("nbt", Nbt)
            .PropertyNotEmpty("state", BlockState)
            .WriteTo(writer);
    }
}