// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

namespace MineJason.SNbt.Values;

using MineJason.SNbt.Parsing;
using MineJason.SNbt.Values.Guids;

/// <summary>
/// Represents a globally/universally unique identifier in NBT.
/// </summary>
public struct SNbtGuidValue : ISNbtWritable, IEquatable<SNbtGuidValue>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SNbtGuidValue"/> structure.
    /// </summary>
    /// <param name="value">The value.</param>
    public SNbtGuidValue(Guid value)
    {
        Value = value;
    }

    /// <summary>
    /// Gets or sets the value of this instance.
    /// </summary>
    public Guid Value { get; set; }

    /// <inheritdoc/>
    public readonly SNbtTagType Type => SNbtTagType.IntArray;

    /// <inheritdoc />
    public readonly bool Equals(SNbtGuidValue other)
    {
        return other.Value.Equals(Value);
    }

    /// <inheritdoc />
    public readonly void WriteTo(SNbtWriter writer)
    {
        GuidHelper.WriteGuid(Value, writer);
    }

    /// <inheritdoc />
    public readonly override bool Equals(object? obj)
    {
        return obj is SNbtGuidValue value && Equals(value);
    }

    /// <summary>
    /// Determines whether the instance to the left is equivalent to the instance to the right.
    /// </summary>
    /// <param name="left">The instance to the left.</param>
    /// <param name="right">The instance to the right.</param>
    /// <returns><see langword="true"/> if the instances are equivalent; otherwise, <see langword="false"/>.</returns>
    public static bool operator ==(SNbtGuidValue left, SNbtGuidValue right)
    {
        return left.Equals(right);
    }

    /// <summary>
    /// Determines whether the instance to the left is different from the instance to the right.
    /// </summary>
    /// <param name="left">The instance to the left.</param>
    /// <param name="right">The instance to the right.</param>
    /// <returns><see langword="true"/> if the instances are different from each other; otherwise, <see langword="false"/>.</returns>
    public static bool operator !=(SNbtGuidValue left, SNbtGuidValue right)
    {
        return !(left == right);
    }

    /// <inheritdoc/>
    public override readonly int GetHashCode()
    {
        return Value.GetHashCode();
    }

    /// <summary>
    /// Converts the specified <see cref="Guid"/> to its NBT representation.
    /// </summary>
    /// <param name="guid">The GUID to convert from.</param>
    public static implicit operator SNbtGuidValue(Guid guid)
    {
        return new SNbtGuidValue(guid);
    }

    /// <summary>
    /// Converts the specified NBT representation to its instance representation.
    /// </summary>
    /// <param name="value">The NBT representation to convert from.</param>
    public static implicit operator Guid(SNbtGuidValue value)
    {
        return value.Value;
    }
}
