// Copyright (c) WithLithum & contributors 2023-2024. All rights reserved.
// Licensed under the GNU Lesser General Public License, either version 3 or 
// (at your opinion) any later version.

namespace MineJason.Data.Coordinates;

using System;
using System.Globalization;

/// <summary>
/// Represents a local (specified by <c>^</c>) block position.
/// </summary>
public struct LocalBlockPosition
{
    /// <summary>
    /// Initializes a new instance of the <see cref="LocalBlockPosition"/> structure.
    /// </summary>
    /// <param name="x">The X component.</param>
    /// <param name="y">The Y component.</param>
    /// <param name="z">The Z component.</param>
    public LocalBlockPosition(int x, int y, int z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    /// <summary>
    /// Gets or sets the X component.
    /// </summary>
    public int X { get; set; }

    /// <summary>
    /// Gets or sets the Y component.
    /// </summary>
    public int Y { get; set; }

    /// <summary>
    /// Gets or sets the Z component.
    /// </summary>
    public int Z { get; set; }

    /// <summary>
    /// Subtracts the instance to the <paramref name="left"/> with the instance to the <paramref name="right"/>.
    /// </summary>
    /// <param name="left">The instance to the left.</param>
    /// <param name="right">The instance to the right.</param>
    /// <returns>The result of operation.</returns>
    public static LocalBlockPosition operator -(LocalBlockPosition left, LocalBlockPosition right)
    {
        return new LocalBlockPosition(left.X - right.X,
            left.Y - right.Y,
            left.Z - right.Z);
    }

    /// <summary>
    /// Adds the instance to the <paramref name="left"/> with the instance to the <paramref name="right"/>.
    /// </summary>
    /// <param name="left">The instance to the left.</param>
    /// <param name="right">The instance to the right.</param>
    /// <returns>The result of operation.</returns>
    public static LocalBlockPosition operator +(LocalBlockPosition left, LocalBlockPosition right)
    {
        return new LocalBlockPosition(left.X + right.X,
            left.Y + right.Y,
            left.Z + right.Z);
    }

    /// <summary>
    /// Multiplies the instance to the <paramref name="left"/> with the instance to the <paramref name="right"/>.
    /// </summary>
    /// <param name="left">The instance to the left.</param>
    /// <param name="right">The instance to the right.</param>
    /// <returns>The result of operation.</returns>
    public static LocalBlockPosition operator *(LocalBlockPosition left, LocalBlockPosition right)
    {
        return new LocalBlockPosition(left.X * right.X,
            left.Y * right.Y,
            left.Z * right.Z);
    }

    /// <summary>
    /// Divides the instance to the <paramref name="left"/> with the instance to the <paramref name="right"/>.
    /// </summary>
    /// <param name="left">The instance to the left.</param>
    /// <param name="right">The instance to the right.</param>
    /// <returns>The result of operation.</returns>
    public static LocalBlockPosition operator /(LocalBlockPosition left, LocalBlockPosition right)
    {
        return new LocalBlockPosition(left.X / right.X,
            left.Y / right.Y,
            left.Z / right.Z);
    }

    internal static LocalBlockPosition Parse(string value)
    {
        var split = value.Split(' ');

        if (!value.Contains('^') || split.Length != 3)
        {
            throw new ArgumentException("Invalid local block position.", nameof(value));
        }

        static int ParseComponent(string value)
        {
            if (value.Length < 2)
            {
                throw new ArgumentException("Cannot mix local & world coordinates.", nameof(value));
            }

            return int.Parse(value[1..], CultureInfo.InvariantCulture);
        }

        return new(ParseComponent(split[0]), ParseComponent(split[1]), ParseComponent(split[2]));
    }

    /// <inheritdoc/>
    public override readonly string ToString()
    {
        static string ToComponentString(int value)
        {
            if (value == 0)
            {
                return "^";
            }
            
            return string.Format(CultureInfo.InvariantCulture, "^{0}", value.ToString(CultureInfo.InvariantCulture));
        }

        return $"{ToComponentString(X)} {ToComponentString(Y)} {ToComponentString(Z)}";
    }
}
