// Copyright (c) WithLithum & contributors 2023-2024. All rights reserved.
// Licensed under the GNU Lesser General Public License, either version 3 or 
// (at your opinion) any later version.

namespace MineJason.Data.Coordinates;

using MineJason.Serialization.TextJson;
using System.Text.Json.Serialization;

/// <summary>
/// Represents a block position.
/// </summary>
[JsonConverter(typeof(AnyBlockPositionConverter))]
public readonly struct AnyBlockPosition : IEquatable<AnyBlockPosition>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AnyBlockPosition"/> structure with a world block position.
    /// </summary>
    /// <param name="worldPosition">The position.</param>
    public AnyBlockPosition(BlockPosition worldPosition)
    {
        IsLocal = false;
        WorldPosition = worldPosition;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="AnyBlockPosition"/> structure with a local block position.
    /// </summary>
    /// <param name="localPosition">The position.</param>
    public AnyBlockPosition(LocalBlockPosition localPosition)
    {
        IsLocal = true;
        LocalPosition = localPosition;
    }

    /// <summary>
    /// Determines whether this instance is a local block position.
    /// </summary>
    public bool IsLocal { get; }

    /// <summary>
    /// Gets the local block position.
    /// </summary>
    public BlockPosition? WorldPosition { get; }

    /// <summary>
    /// Gets the world block position.
    /// </summary>
    public LocalBlockPosition? LocalPosition { get; }
    
    /// <inheritdoc/>
    public bool Equals(AnyBlockPosition other)
    {
        if (IsLocal)
        {
            return LocalPosition.Equals(other.LocalPosition);
        }
        else
        {
            return WorldPosition.Equals(other.WorldPosition);
        }
    }

    /// <inheritdoc/>
    public override bool Equals(object? obj)
    {
        return obj is AnyBlockPosition position && Equals(position);
    }

    /// <summary>
    /// Determines whether the instance to the left is equivalent to the instance to the right.
    /// </summary>
    /// <param name="left">The instance to the left.</param>
    /// <param name="right">The instance to the right.</param>
    /// <returns><see langword="true"/> if the instance to the left is equivalent to the instance to the right; otherwise, <see langword="false"/>.</returns>
    public static bool operator ==(AnyBlockPosition left, AnyBlockPosition right)
    {
        return left.Equals(right);
    }

    /// <summary>
    /// Determines whether the instance to the left is not equivalent to the instance to the right.
    /// </summary>
    /// <param name="left">The instance to the left.</param>
    /// <param name="right">The instance to the right.</param>
    /// <returns><see langword="true"/> if the instance to the left is not equivalent to the instance to the right; otherwise, <see langword="false"/>.</returns>
    public static bool operator !=(AnyBlockPosition left, AnyBlockPosition right)
    {
        return !(left == right);
    }

    /// <inheritdoc/>
    public override int GetHashCode()
    {
        if (IsLocal)
        {
            return LocalPosition.GetHashCode();
        }
        else
        {
            return WorldPosition.GetHashCode();
        }
    }

    /// <inheritdoc/>
    public override string ToString()
    {
        if (IsLocal)
        {
            return LocalPosition!.ToString()!;
        }
        else
        {
            return WorldPosition!.ToString()!;
        }
    }
}
