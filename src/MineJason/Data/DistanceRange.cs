// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: LGPL-3.0-or-later

namespace MineJason.Data;

using System.Text;
using JetBrains.Annotations;

/// <summary>
/// Gets or sets the distance range.
/// </summary>
[PublicAPI]
public readonly struct DistanceRange : IEquatable<DistanceRange>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DistanceRange"/> structure.
    /// </summary>
    /// <param name="exact">The exact value to match.</param>
    public DistanceRange(double exact)
    {
        Exact = exact;
    }
    
    /// <summary>
    /// Initializes a new instance of the <see cref="DistanceRange"/> structure.
    /// </summary>
    /// <param name="min">The minimum value. If <see langword="null"/>, it is treated that the range does not have a minimum.</param>
    /// <param name="max">The maximum value. If <see langword="null"/>, it is treated that the range does not have a maximum.</param>
    public DistanceRange(double? min, double? max)
    {
        Min = min;
        Max = max;
    }
    
    /// <summary>
    /// Gets or sets the minimum range.
    /// </summary>
    public double? Min { get; }

    /// <summary>
    /// Gets or sets the maximum range.
    /// </summary>
    public double? Max { get; }

    /// <summary>
    /// Gets or sets the exact value to match.
    /// </summary>
    public double? Exact { get; }

    /// <inheritdoc />
    public bool Equals(DistanceRange other)
    {
        return Min.Equals(other.Min) && Max.Equals(other.Max);
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        return obj is DistanceRange other && Equals(other);
    }

    /// <summary>
    /// Converts this coordinate range to its <see cref="string"/> representation.
    /// </summary>
    /// <returns>If none of <see cref="Min"/>, or <see cref="Max"/> was specified, returns <see cref="string.Empty"/>; otherwise, returns the
    /// range as used by a target selector.</returns>
    public override string ToString()
    {
        var builder = new StringBuilder();

        if (!Min.HasValue && !Max.HasValue)
        {
            return string.Empty;
        }
        
        if (Min.HasValue)
        {
            builder.Append(Min);
        }

        builder.Append("..");

        if (Max.HasValue)
        {
            builder.Append(Max);
        }

        return builder.ToString();
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        return HashCode.Combine(Min, Max);
    }

    /// <summary>
    /// Determines whether the instance to the <paramref name="left"/> is equivalent to the instance
    /// to the <paramref name="right"/>.
    /// </summary>
    /// <param name="left">The left instance.</param>
    /// <param name="right">The right instance.</param>
    /// <returns><see langword="true"/> if the instance to the <paramref name="left"/> is equivalent to the instance
    /// to the <paramref name="right"/>; otherwise, <see langword="false"/>.</returns>
    public static bool operator ==(DistanceRange left, DistanceRange right)
    {
        return left.Equals(right);
    }

    /// <summary>
    /// Determines whether the instance to the <paramref name="left"/> does not equals to the instance
    /// to the <paramref name="right"/>.
    /// </summary>
    /// <param name="left">The left instance.</param>
    /// <param name="right">The right instance.</param>
    /// <returns><see langword="true"/> if the instance to the <paramref name="left"/> is not equivalent to the instance
    /// to the <paramref name="right"/>; otherwise, <see langword="false"/>.</returns>
    public static bool operator !=(DistanceRange left, DistanceRange right)
    {
        return !(left == right);
    }

    /// <summary>
    /// Returns a new <see cref="DistanceRange"/> that matches for an exact distance.
    /// </summary>
    /// <param name="distance">The distance.</param>
    /// <returns>The instance.</returns>
    public static DistanceRange MatchExact(double distance)
    {
        return new DistanceRange(distance);
    }
    
    /// <summary>
    /// Returns a new <see cref="DistanceRange"/> that matches for a range between.
    /// </summary>
    /// <param name="min">The minimum distance.</param>
    /// <param name="max">The maximum distance</param>
    /// <returns>The instance.</returns>
    public static DistanceRange MatchRange(double? min, double? max)
    {
        return new DistanceRange(min, max);
    }
}