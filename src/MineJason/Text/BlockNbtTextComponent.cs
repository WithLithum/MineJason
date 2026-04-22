// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using MineJason.Data.Coordinates;
using MineJason.Text.Builders.Utilities;

namespace MineJason.Text;

/// <summary>
/// Represents a text component that resolves to the text representation of an NBT value retrieved
/// from a block entity. This class cannot be inherited.
/// </summary>
/// <remarks>
/// This type of component is resolved on the server side and will display nothing if resolved on
/// the client as-is.
/// </remarks>
public sealed record BlockNbtTextComponent : NbtTextComponent,
    IEquatable<BlockNbtTextComponent>
{
    /// <summary>
    /// The value of the <c>source</c> field identifying this type.
    /// </summary>
    public const string SourceName = "block";

    /// <summary>
    /// Initializes a new instance of the <see cref="BlockNbtTextComponent"/> class.
    /// </summary>
    public BlockNbtTextComponent()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="BlockNbtTextComponent"/> class.
    /// </summary>
    /// <param name="block">The block.</param>
    /// <param name="path">The path.</param>
    [SetsRequiredMembers]
    public BlockNbtTextComponent(BlockPosition block, string path)
    {
        Path = path;
        Block = block;
    }

    [SetsRequiredMembers]
    internal BlockNbtTextComponent(in TextComponentCreationInfo creationInfo,
        in NbtTextComponentCreationInfo nbtInfo,
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
