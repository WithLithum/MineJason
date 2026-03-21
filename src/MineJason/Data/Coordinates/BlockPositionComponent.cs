// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: LGPL-3.0-or-later

namespace MineJason.Data.Coordinates;

using System.Diagnostics.CodeAnalysis;
using System.Globalization;

/// <summary>
/// Represents a single component in a block position.
/// </summary>
public readonly struct BlockPositionComponent : IEquatable<BlockPositionComponent>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BlockPositionComponent"/> structure.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <param name="isRelative">If <see langword="true"/>, this instance is a relative coordinate component.</param>
    [Obsolete("Use BlockPositionComponent(int, BlockPositionComponentType) instead")]
    public BlockPositionComponent(int value, bool isRelative) : this(value,
        isRelative
        ? BlockPositionComponentType.Relative
        : BlockPositionComponentType.Absolute)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="BlockPositionComponent"/> structure.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <param name="type">The type.</param>
    public BlockPositionComponent(int value, BlockPositionComponentType type = BlockPositionComponentType.Absolute)
    {
        Value = value;
        Type = type;
    }

    /// <summary>
    /// Gets the value of the component.
    /// </summary>
    public int Value { get; }

    /// <summary>
    /// Gets the type of the component.
    /// </summary>
    public BlockPositionComponentType Type { get; }

    /// <summary>
    /// Gets a value indicating whether this instance is relative.
    /// </summary>
    [Obsolete("Check for Type instead.")]
    public bool IsRelative => Type == BlockPositionComponentType.Relative;

    /// <summary>
    /// Adds the instance to the left with the instance to the right.
    /// </summary>
    /// <param name="left">The instance to the left.</param>
    /// <param name="right">The instance to the right.</param>
    /// <returns>The result of the operation.</returns>
    /// <exception cref="ArgumentException">The relativity is different between the instances.</exception>
    public static BlockPositionComponent operator +(BlockPositionComponent left, BlockPositionComponent right)
    {
        if (left.Type != right.Type)
        {
            throw new ArgumentException("The instance to the left have a different type than the instance to the right.");
        }

        return new(left.Value + right.Value);
    }

    /// <summary>
    /// Adds the instance to the left with the number to the right.
    /// </summary>
    /// <param name="left">The instance to the left.</param>
    /// <param name="right">The number to the right.</param>
    /// <returns>The result of the operation.</returns>
    public static BlockPositionComponent operator +(BlockPositionComponent left, int right)
    {
        return new(left.Value + right);
    }

    /// <summary>
    /// Adds the number to the left with the instance to the right.
    /// </summary>
    /// <param name="left">The number to the left.</param>
    /// <param name="right">The instance to the right.</param>
    /// <returns>The result of the operation.</returns>
    public static BlockPositionComponent operator +(int left, BlockPositionComponent right)
    {
        return new(left + right.Value);
    }

    /// <summary>
    /// Subtracts the instance to the left with the instance to the right.
    /// </summary>
    /// <param name="left">The instance to the left.</param>
    /// <param name="right">The instance to the right.</param>
    /// <returns>The result of the operation.</returns>
    /// <exception cref="ArgumentException">The relativity is different between the instances.</exception>
    public static BlockPositionComponent operator -(BlockPositionComponent left, BlockPositionComponent right)
    {
        if (left.Type != right.Type)
        {
            throw new ArgumentException("The instance to the left have a different type than the instance to the right.");
        }

        return new(left.Value - right.Value);
    }

    /// <summary>
    /// Subtracts the instance to the left with the number to the right.
    /// </summary>
    /// <param name="left">The instance to the left.</param>
    /// <param name="right">The number to the right.</param>
    /// <returns>The result of the operation.</returns>
    public static BlockPositionComponent operator -(BlockPositionComponent left, int right)
    {
        return new(left.Value - right);
    }

    /// <summary>
    /// Subtracts the number to the left with the instance to the right.
    /// </summary>
    /// <param name="left">The number to the left.</param>
    /// <param name="right">The instance to the right.</param>
    /// <returns>The result of the operation.</returns>
    public static BlockPositionComponent operator -(int left, BlockPositionComponent right)
    {
        return new(left - right.Value);
    }

    /// <summary>
    /// Multiplies the instance to the left with the instance to the right.
    /// </summary>
    /// <param name="left">The instance to the left.</param>
    /// <param name="right">The instance to the right.</param>
    /// <returns>The result of the operation.</returns>
    /// <exception cref="ArgumentException">The relativity is different between the instances.</exception>
    public static BlockPositionComponent operator *(BlockPositionComponent left, BlockPositionComponent right)
    {
        if (left.Type != right.Type)
        {
            throw new ArgumentException("The instance to the left have a different type than the instance to the right.");
        }

        return new(left.Value * right.Value);
    }

    /// <summary>
    /// Multiplies the instance to the left with the number to the right.
    /// </summary>
    /// <param name="left">The instance to the left.</param>
    /// <param name="right">The number to the right.</param>
    /// <returns>The result of the operation.</returns>
    public static BlockPositionComponent operator *(BlockPositionComponent left, int right)
    {
        return new(left.Value * right);
    }

    /// <summary>
    /// Multiplies the number to the left with the instance to the right.
    /// </summary>
    /// <param name="left">The number to the left.</param>
    /// <param name="right">The instance to the right.</param>
    /// <returns>The result of the operation.</returns>
    public static BlockPositionComponent operator *(int left, BlockPositionComponent right)
    {
        return new(left * right.Value);
    }

    /// <summary>
    /// Divides the instance to the left with the instance to the right.
    /// </summary>
    /// <param name="left">The instance to the left.</param>
    /// <param name="right">The instance to the right.</param>
    /// <returns>The result of the operation.</returns>
    /// <exception cref="ArgumentException">The relativity is different between the instances.</exception>
    public static BlockPositionComponent operator /(BlockPositionComponent left, BlockPositionComponent right)
    {
        if (left.Type != right.Type)
        {
            throw new ArgumentException("The instance to the left have a different type than the instance to the right.");
        }

        return new(left.Value / right.Value);
    }

    /// <summary>
    /// Divides the instance to the left with the number to the right.
    /// </summary>
    /// <param name="left">The instance to the left.</param>
    /// <param name="right">The number to the right.</param>
    /// <returns>The result of the operation.</returns>
    public static BlockPositionComponent operator /(BlockPositionComponent left, int right)
    {
        return new(left.Value / right);
    }

    /// <summary>
    /// Divides the number to the left with the instance to the right.
    /// </summary>
    /// <param name="left">The number to the left.</param>
    /// <param name="right">The instance to the right.</param>
    /// <returns>The result of the operation.</returns>
    public static BlockPositionComponent operator /(int left, BlockPositionComponent right)
    {
        return new(left / right.Value);
    }

    /// <inheritdoc/>
    public bool Equals(BlockPositionComponent other)
    {
        return Type == other.Type && Value == other.Value;
    }

    /// <inheritdoc/>
    public override bool Equals([NotNullWhen(true)] object? obj)
    {
        return obj is BlockPositionComponent component && component.Equals(this);
    }

    /// <inheritdoc/>
    public override int GetHashCode()
    {
        return HashCode.Combine(Type, Value);
    }

    /// <summary>
    /// Determines whether the instance to the left is equivalent to the instance to the right.
    /// </summary>
    /// <param name="left">The instance to the left.</param>
    /// <param name="right">The instance to the right.</param>
    /// <returns><see langword="true"/> if the instance to the left is equivalent to the instance to the right; otherwise, <see langword="false"/>.</returns>
    public static bool operator ==(BlockPositionComponent left, BlockPositionComponent right)
    {
        return left.Equals(right);
    }

    /// <summary>
    /// Determines whether the instance to the left is not equivalent to the instance to the right.
    /// </summary>
    /// <param name="left">The instance to the left.</param>
    /// <param name="right">The instance to the right.</param>
    /// <returns><see langword="true"/> if the instance to the left is not equivalent to the instance to the right; otherwise, <see langword="false"/>.</returns>
    public static bool operator !=(BlockPositionComponent left, BlockPositionComponent right)
    {
        return !(left == right);
    }

    /// <summary>
    /// Parses the specified string into a <see cref="BlockPositionComponent"/> instance.
    /// </summary>
    /// <param name="from">The string to parse.</param>
    /// <returns>The parsed value.</returns>
    public static BlockPositionComponent Parse(string from)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(from);

        if (from == "~")
        {
            return new(0, BlockPositionComponentType.Relative);
        }

        if (from.StartsWith('~'))
        {
            return new(int.Parse(from[1..], CultureInfo.InvariantCulture),
                BlockPositionComponentType.Relative);
        }
        else if (from.StartsWith('^'))
        {
            return new(int.Parse(from[1..], CultureInfo.InvariantCulture),
                BlockPositionComponentType.Local);
        }
        else
        {
            return new(int.Parse(from, CultureInfo.InvariantCulture));
        }
    }

    /// <summary>
    /// Converts this instance to its string representation.
    /// </summary>
    /// <returns>The string.</returns>
    public override string ToString()
    {
        if (Type != BlockPositionComponentType.Absolute)
        {
            var prefix = Type switch
            {
                BlockPositionComponentType.Relative => '~',
                BlockPositionComponentType.Local => '^',
                _ => throw new InvalidOperationException("How do we even get to here!?")
            };

            if (Value == 0)
            {
                return prefix.ToString();
            }
            else
            {
                return $"{prefix}{Value}";
            }
        }

        return Value.ToString(CultureInfo.InvariantCulture);
    }
}
