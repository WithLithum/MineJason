// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

namespace MineJason.SNbt.Values;

using System.Text.Json.Serialization;
using JetBrains.Annotations;
using MineJason.SNbt.Parsing;
using MineJason.SNbt.TextJson;

/// <summary>
/// Represents the <c>TAG_Double</c>.
/// </summary>
[PublicAPI]
[JsonConverter(typeof(DoubleValueConverter))]
public record struct SNbtDoubleValue : ISNbtWritable
{
    /// <summary>
    /// The suffix of the NBT representation of this value.
    /// </summary>
    public const char ValueSuffix = 'd';
    
    /// <summary>
    /// Initializes a new instance of the <see cref="SNbtDoubleValue"/> class.
    /// </summary>
    /// <param name="value">The value.</param>
    public SNbtDoubleValue(double value)
    {
        Value = value;
    }
    
    /// <inheritdoc/>
    public readonly SNbtTagType Type => SNbtTagType.Double;

    /// <summary>
    /// Gets or sets the value of this instance.
    /// </summary>
    public double Value { get; set; }
    
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
    public static implicit operator SNbtDoubleValue(double from)
    {
        return new SNbtDoubleValue(from);
    }

    /// <summary>
    /// Converts the specified string NBT form of value to its value representation.
    /// </summary>
    /// <param name="from">The string NBT value to convert from.</param>
    /// <returns>The value form.</returns>
    public static implicit operator double(SNbtDoubleValue from)
    {
        return from.Value;
    }
}