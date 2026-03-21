// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

namespace MineJason.SNbt.Values;

using System.Text.Json.Serialization;
using JetBrains.Annotations;
using MineJason.SNbt.Parsing;
using MineJason.SNbt.TextJson;

/// <summary>
/// Represents the <c>TAG_Float</c>.
/// </summary>
[PublicAPI]
[JsonConverter(typeof(FloatValueConverter))]
public record struct SNbtFloatValue : ISNbtWritable
{
    /// <summary>
    /// The suffix of the NBT representation of this value.
    /// </summary>
    public const char ValueSuffix = 'f';
    
    /// <summary>
    /// Initializes a new instance of the <see cref="SNbtFloatValue"/> with the specified value.
    /// </summary>
    /// <param name="value"></param>
    public SNbtFloatValue(float value)
    {
        Value = value;
    }
    
    /// <summary>
    /// Gets or sets the value of this instance.
    /// </summary>
    public float Value { get; set; }
    
    /// <inheritdoc/>
    public readonly SNbtTagType Type => SNbtTagType.Float;
    
    /// <inheritdoc />
    public readonly void WriteTo(SNbtWriter writer)
    {
        writer.WriteFormattable(Value, ValueSuffix);
    }
    
    /// <summary>
    /// Converts the specified value to its string NBT value form.
    /// </summary>
    /// <param name="from">The value to convert from.</param>
    /// <returns>The string NBT value form.</returns>
    public static implicit operator SNbtFloatValue(float from)
    {
        return new SNbtFloatValue(from);
    }

    /// <summary>
    /// Converts the specified string NBT form of value to its value representation.
    /// </summary>
    /// <param name="from">The string NBT value to convert from.</param>
    /// <returns>The value form.</returns>
    public static implicit operator float(SNbtFloatValue from)
    {
        return from.Value;
    }
}