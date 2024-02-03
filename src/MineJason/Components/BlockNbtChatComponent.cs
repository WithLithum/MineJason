// Copyright (c) WithLithum & contributors 2023-2024. All rights reserved.
// Licensed under the GNU Lesser General Public License, either version 3 or 
// (at your opinion) any later version.

namespace MineJason.Components;

using MineJason.Data.Coordinates;
using System;
using System.Text.Json.Serialization;

/// <summary>
/// Represents an NBT chat component that sources data from a block entity. This class cannot be inherited.
/// </summary>
public sealed class BlockNbtChatComponent : BaseNbtChatComponent, IEquatable<BlockNbtChatComponent>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BlockNbtChatComponent"/> class.
    /// </summary>
    /// <param name="block">The block.</param>
    /// <param name="path">The path.</param>
    public BlockNbtChatComponent(AnyBlockPosition block, string path) : base(path)
    {
        Block = block;
    }

    /// <summary>
    /// Gets or sets the position of the block to source data from.
    /// </summary>
    [JsonPropertyName("block")]
    public AnyBlockPosition Block { get; set; }

    /// <inheritdoc/>
    public override bool Equals(BaseNbtChatComponent? other)
    {
        return other is BlockNbtChatComponent component && Equals(component);
    }

    /// <inheritdoc/>
    public override bool Equals(object? obj)
    {
        return obj is BlockNbtChatComponent component && Equals(component);
    }

    /// <inheritdoc/>
    public bool Equals(BlockNbtChatComponent? other)
    {
        return base.Equals(other) && Block.Equals(other.Block);
    }

    /// <inheritdoc/>
    public override int GetHashCode()
    {
        return HashCode.Combine(base.GetHashCode(), Block.GetHashCode());
    }

    /// <inheritdoc/>
    public override string ToString()
    {
        return $"[block={Block},nbt={Path}]";
    }
}
