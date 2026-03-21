// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

namespace MineJason.SNbt.Values;

using System.Globalization;
using System.Text.Json.Serialization;
using JetBrains.Annotations;
using MineJason.SNbt.Parsing;
using MineJason.SNbt.TextJson;

/// <summary>
/// Represents a <c>TAG_Short</c>.
/// </summary>
[PublicAPI]
[JsonConverter(typeof(ShortValueConverter))]
public record struct SNbtShortValue : ISNbtWritable
{
    /// <summary>
    /// The suffix of the NBT representation of this value.
    /// </summary>
    public const char ValueSuffix = 's';
    
    /// <summary>
    /// Initializes a new instance of the <see cref="SNbtShortValue"/> structure.
    /// </summary>
    /// <param name="value">The value.</param>
    public SNbtShortValue(short value)
    {
        Value = value;
    }
    
    /// <summary>
    /// Gets or sets the value of this instance.
    /// </summary>
    public short Value { get; set; }
    
    /// <inheritdoc/>
    public readonly SNbtTagType Type => SNbtTagType.IntArray;
    
    /// <summary>
    /// Converts this instance to its SNBT representation in string.
    /// </summary>
    /// <returns>The SNBT representation.</returns>
    [Obsolete("Use SNbtWritableExtensions.ToSNbtString instead.")]
    public string ToSNbtString()
    {
        return $"{Value.ToString(CultureInfo.InvariantCulture)}{ValueSuffix}";
    }

    /// <inheritdoc/>
    public void WriteTo(SNbtWriter writer)
    {
        writer.WriteFormattable(Value, ValueSuffix);
    }
    
    /// <summary>
    /// Converts the specified value to its string NBT value form.
    /// </summary>
    /// <param name="from">The value to convert from.</param>
    /// <returns>The string NBT value form.</returns>
    public static implicit operator SNbtShortValue(short from)
    {
        return new SNbtShortValue(from);
    }

    /// <summary>
    /// Converts the specified string NBT form of value to its value representation.
    /// </summary>
    /// <param name="from">The string NBT value to convert from.</param>
    /// <returns>The value form.</returns>
    public static implicit operator short(SNbtShortValue from)
    {
        return from.Value;
    }
}