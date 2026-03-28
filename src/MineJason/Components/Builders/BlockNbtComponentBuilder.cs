// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: LGPL-3.0-or-later

namespace MineJason.Components.Builders;

using MineJason.Data.Coordinates;
using MineJason.Text;

/// <summary>
/// Constructs a NBT chat component with data sourced from a block entity. This class cannot be inherited.
/// </summary>
public sealed class BlockNbtComponentBuilder : NbtComponentBuilder<BlockNbtTextComponent>
{
    internal BlockNbtComponentBuilder(BlockPosition position)
    {
        _position = position;
    }
    
    private readonly BlockPosition _position;

    /// <inheritdoc />
    public override BlockNbtTextComponent Build()
    {
        var baseInfo = CreateData();
        var nbtInfo = CreateNBTData();

        return new BlockNbtTextComponent(baseInfo, nbtInfo, _position);
    }
}