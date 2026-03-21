// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: LGPL-3.0-or-later

namespace MineJason.Data.Coordinates;

using MineJason.Serialization.TextJson;
using System.Text.Json.Serialization;
using JetBrains.Annotations;

/// <summary>
/// Represents a block position.
/// </summary>
[JsonConverter(typeof(BlockPositionConverter))]
[PublicAPI]
public readonly struct BlockPosition
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BlockPosition"/> structure.
    /// </summary>
    /// <param name="x">The X component.</param>
    /// <param name="y">The Y component.</param>
    /// <param name="z">The Z component.</param>
    /// <param name="isRelative">If <see langword="true"/>, all components are relative.</param>
    [Obsolete("Use BlockPosition(int, int, int, BlockPositionComponentType) instead.")]
    public BlockPosition(int x, int y, int z, bool isRelative)
        : this(x, y, z, isRelative ? BlockPositionComponentType.Relative : BlockPositionComponentType.Absolute)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="BlockPosition"/> structure.
    /// </summary>
    /// <param name="x">The X component.</param>
    /// <param name="y">The Y component.</param>
    /// <param name="z">The Z component.</param>
    /// <param name="type">The type for all components.</param>
    public BlockPosition(int x, int y, int z, BlockPositionComponentType type = BlockPositionComponentType.Absolute)
    {
        X = new BlockPositionComponent(x, type);
        Y = new BlockPositionComponent(y, type);
        Z = new BlockPositionComponent(z, type);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="BlockPosition"/> structure.
    /// </summary>
    /// <param name="x">The X component.</param>
    /// <param name="y">The Y component.</param>
    /// <param name="z">The Z component.</param>
    public BlockPosition(BlockPositionComponent x, BlockPositionComponent y, BlockPositionComponent z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    /// <summary>
    /// Gets the X component of this instance.
    /// </summary>
    public BlockPositionComponent X { get; }

    /// <summary>
    /// Gets the Y component of this instance.
    /// </summary>
    public BlockPositionComponent Y { get; }
    
    /// <summary>
    /// Gets the Z component of this instance.
    /// </summary>
    public BlockPositionComponent Z { get; }

    /// <summary>
    /// Parses the specified string into a <see cref="BlockPosition"/> instance.
    /// </summary>
    /// <param name="from">The string to parse.</param>
    /// <returns>The parsed value.</returns>
    /// <exception cref="ArgumentException">Format is invalid.</exception>
    public static BlockPosition Parse(string from)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(from);
        var split = from.Split(' ');

        if (split.Length != 3)
        {
            throw new ArgumentException("Block position requires exactly 3 components.", nameof(from));
        }

        return new(BlockPositionComponent.Parse(split[0]),
            BlockPositionComponent.Parse(split[1]),
            BlockPositionComponent.Parse(split[2]));
    }

    /// <summary>
    /// Determines whether this instance is valid.
    /// </summary>
    /// <remarks>
    /// An instance of <see cref="BlockPosition"/> with local and non-local components mixed are invalid.
    /// </remarks>
    /// <returns><see langword="true"/> if this instance is valid; otherwise, <see langword="false"/>.</returns>
    public bool IsValid()
    {
        BlockPositionComponent[] comps = [X, Y, Z];

        var hasNonLocal = false;
        var hasLocal = false;
        foreach (var x in comps)
        {
            if (x.Type == BlockPositionComponentType.Local)
            {
                hasLocal = true;
            }
            else
            {
                hasNonLocal = true;
            }
        }

        return hasLocal != hasNonLocal;
    }

    /// <summary>
    /// Returns the string representation of this instance.
    /// </summary>
    /// <returns>The string.</returns>
    public override string ToString()
    {
        return $"{X} {Y} {Z}";
    }
}
