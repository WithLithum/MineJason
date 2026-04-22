// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

using MineJason.Data.Coordinates;

namespace MineJason.Text.Builders;

/// <summary>
/// Constructs a new instance of <see cref="BlockNbtTextComponent"/>. This class cannot be
/// inherited.
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