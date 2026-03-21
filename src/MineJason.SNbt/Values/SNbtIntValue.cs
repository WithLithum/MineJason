// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

namespace MineJason.SNbt.Values;

using System.Text.Json.Serialization;
using JetBrains.Annotations;
using MineJason.SNbt.Parsing;
using MineJason.SNbt.TextJson;

/// <summary>
/// Represents a <c>TAG_Int</c>.
/// </summary>
[PublicAPI]
[JsonConverter(typeof(IntValueConverter))]
public record struct SNbtIntValue : ISNbtWritable
{
    /// <summary>
    /// Initializes a new instance of <see cref="SNbtIntValue"/> structure.
    /// </summary>
    /// <param name="value">The value.</param>
    public SNbtIntValue(int value)
    {
        Value = value;
    }
    
    /// <summary>
    /// Gets or sets the value of this instance.
    /// </summary>
    public int Value { get; set; }

    /// <inheritdoc/>
    public readonly SNbtTagType Type => SNbtTagType.Int;

    /// <summary>
    /// Converts the specified instance representation to its <c>TAG_Int</c> representation.
    /// </summary>
    /// <param name="from">The value to convert from.</param>
    public static implicit operator SNbtIntValue(int from)
    {
        return new SNbtIntValue(from);
    }

    /// <summary>
    /// Converts the specified <c>TAG_Int</c> representation to its instance representation.
    /// </summary>
    /// <param name="from">The tag to convert from.</param>
    public static implicit operator int(SNbtIntValue from)
    {
        return from.Value;
    }

    /// <inheritdoc/>
    public void WriteTo(SNbtWriter writer)
    {
        writer.WriteValue(Value);
    }
}