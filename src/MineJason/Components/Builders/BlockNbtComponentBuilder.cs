// Copyright (c) WithLithum & contributors 2023-2024. All rights reserved.
// Licensed under the GNU Lesser General Public License, either version 3 or
// (at your opinion) any later version.

namespace MineJason.Components.Builders;

using MineJason.Data.Coordinates;

/// <summary>
/// Constructs a NBT chat component with data sourced from a block entity. This class cannot be inherited.
/// </summary>
public sealed class BlockNbtComponentBuilder : NbtComponentBuilder<BlockNbtChatComponent>
{
    internal BlockNbtComponentBuilder(AnyBlockPosition position)
    {
        _position = position;
    }
    
    private readonly AnyBlockPosition _position;

    /// <inheritdoc />
    protected override BlockNbtChatComponent CreateComponent(string path)
    {
        return new BlockNbtChatComponent(_position, path);
    }
}