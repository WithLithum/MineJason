// Copyright (c) WithLithum & contributors 2023-2024. All rights reserved.
// Licensed under the GNU Lesser General Public License, either version 3 or 
// (at your opinion) any later version.

namespace MineJason.Data.Coordinates;

/// <summary>
/// Represents a block position.
/// </summary>
public readonly struct BlockPosition
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BlockPosition"/> structure.
    /// </summary>
    /// <param name="x">The X component.</param>
    /// <param name="y">The Y component.</param>
    /// <param name="z">The Z component.</param>
    /// <param name="isRelative">If <see langword="true"/>, all components are relative.</param>
    public BlockPosition(int x, int y, int z, bool isRelative = false)
    {
        X = new BlockPositionComponent(x, isRelative);
        Y = new BlockPositionComponent(y, isRelative);
        Z = new BlockPositionComponent(z, isRelative);
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
    /// Returns the string representation of this instance.
    /// </summary>
    /// <returns>The string.</returns>
    public override string ToString()
    {
        return $"{X} {Y} {Z}";
    }
}
