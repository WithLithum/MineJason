// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: LGPL-3.0-or-later

namespace MineJason.Components;

using MineJason.Components.Builders;
using MineJason.Data.Coordinates;
using MineJason.Text;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

/// <summary>
/// Represents an NBT chat component that sources data from a block entity. This class cannot be inherited.
/// </summary>
public sealed record BlockNbtChatComponent : BaseNbtChatComponent,
    IEquatable<BlockNbtChatComponent>
{
    /// <summary>
    /// The value of the <c>source</c> field identifying this type.
    /// </summary>
    public const string SourceName = "block";

    /// <summary>
    /// Initializes a new instance of the <see cref="BlockNbtChatComponent"/> class.
    /// </summary>
    public BlockNbtChatComponent()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="BlockNbtChatComponent"/> class.
    /// </summary>
    /// <param name="block">The block.</param>
    /// <param name="path">The path.</param>
    [SetsRequiredMembers]
    public BlockNbtChatComponent(BlockPosition block, string path)
    {
        Path = path;
        Block = block;
    }

    [SetsRequiredMembers]
    internal BlockNbtChatComponent(in TextComponentCreationInfo creationInfo,
        in NBTTextComponentCreationInfo nbtInfo,
        BlockPosition block) : base(creationInfo, nbtInfo)
    {
        Block = block;
    }

    /// <summary>
    /// Gets or sets the position of the block to source data from.
    /// </summary>
    [JsonPropertyName("block")]
    public required BlockPosition Block { get; init; }

    /// <inheritdoc/>
    public override string ToString()
    {
        return $"[block={Block},nbt={Path}]";
    }
}
