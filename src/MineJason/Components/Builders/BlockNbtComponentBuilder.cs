// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: LGPL-3.0-or-later

namespace MineJason.Components.Builders;

using MineJason.Data.Coordinates;

/// <summary>
/// Constructs a NBT chat component with data sourced from a block entity. This class cannot be inherited.
/// </summary>
public sealed class BlockNbtComponentBuilder : NbtComponentBuilder<BlockNbtChatComponent>
{
    internal BlockNbtComponentBuilder(BlockPosition position)
    {
        _position = position;
    }
    
    private readonly BlockPosition _position;

    /// <inheritdoc />
    public override BlockNbtChatComponent Build()
    {
        var baseInfo = CreateData();
        var nbtInfo = CreateNBTData();

        return new BlockNbtChatComponent(baseInfo, nbtInfo, _position);
    }
}