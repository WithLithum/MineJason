// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

namespace MineJason.SNbt.Values;

using System.Text.Json.Serialization;
using JetBrains.Annotations;
using MineJason.SNbt.Parsing;
using MineJason.SNbt.TextJson;

/// <summary>
/// Represents a String NBT value consisting of a <see cref="sbyte"/>.
/// </summary>
[JsonConverter(typeof(ByteValueConverter))]
public record struct SNbtByteValue : ISNbtWritable
{
    /// <summary>
    /// The suffix of the NBT representation of this value.
    /// </summary>
    public const char ValueSuffix = 'b';
    
    /// <summary>
    /// Initializes a new instance of the <see cref="SNbtByteValue"/> structure.
    /// </summary>
    /// <param name="value">The boolean value to convert from.</param>
    [PublicAPI]
    public SNbtByteValue(bool value)
    {
        Value = GetValueFromBoolean(value);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="SNbtByteValue"/> structure.
    /// </summary>
    /// <param name="value">The value.</param>
    [PublicAPI]
    public SNbtByteValue(sbyte value)
    {
        Value = value;
    }

    /// <inheritdoc/>
    public readonly SNbtTagType Type => SNbtTagType.Byte;
    
    /// <summary>
    /// Gets or sets the value of this instance.
    /// </summary>
    [PublicAPI]
    public sbyte Value { get; set; }

    /// <summary>
    /// Returns the boolean value of this instance.
    /// </summary>
    /// <returns><see langword="true"/> if <see cref="Value"/> equals to or is bigger than <c>1</c>; otherwise, <see langword="false"/>.</returns>
    [PublicAPI]
    public readonly bool BooleanValue()
    {
        return Value >= 1;
    }

    /// <inheritdoc/>
    public readonly void WriteTo(SNbtWriter writer)
    {
        writer.WriteFormattable(Value, ValueSuffix);
    }
    
    /// <summary>
    /// Converts the specified <see cref="bool"/> to its NBT <see cref="sbyte"/> representation.
    /// </summary>
    /// <param name="value">The value to convert from.</param>
    /// <returns><c>1</c> if value is <see langword="true"/>; otherwise, <c>0</c>.</returns>
    public static sbyte GetValueFromBoolean(bool value)
    {
        return value ? (sbyte)1 : (sbyte)0;
    }

    /// <summary>
    /// Converts the specified value to its string NBT value form.
    /// </summary>
    /// <param name="from">The value to convert from.</param>
    /// <returns>The string NBT value form.</returns>
    public static implicit operator SNbtByteValue(sbyte from)
    {
        return new SNbtByteValue(from);
    }

    /// <summary>
    /// Converts the specified string NBT form of value to its value representation.
    /// </summary>
    /// <param name="from">The string NBT value to convert from.</param>
    /// <returns>The value form.</returns>
    public static implicit operator sbyte(SNbtByteValue from)
    {
        return from.Value;
    }
}