// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

namespace MineJason.SNbt.Values;

using System.Text.Json.Serialization;
using JetBrains.Annotations;
using MineJason.SNbt.Parsing;
using MineJason.SNbt.TextJson;

/// <summary>
/// Represents a <c>TAG_String</c>.
/// </summary>
[PublicAPI]
[JsonConverter(typeof(StringValueConverter))]
public record struct SNbtStringValue : ISNbtWritable
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SNbtStringValue"/> structure.
    /// </summary>
    /// <param name="value">The value.</param>
    public SNbtStringValue(string value)
    {
        Value = value;
    }
    
    /// <summary>
    /// Gets or sets the value of this instance.
    /// </summary>
    public string Value { get; set; }
    
    /// <inheritdoc/>
    public readonly SNbtTagType Type => SNbtTagType.String;
    
    /// <summary>
    /// Converts this instance to its SNBT representation in string.
    /// </summary>
    /// <returns>The SNBT representation.</returns>
    [Obsolete("Use SNbtWritableExtensions.ToSNbtString instead.")]
    public string ToSNbtString()
    {
        return EscapeString(Value);
    }

    /// <summary>
    /// Escapes the specified string.
    /// </summary>
    /// <param name="value">The string to escape.</param>
    /// <param name="singleQuote">If <see langword="true"/>, uses single quotes instead of double quotes.</param>
    /// <returns></returns>
    public static string EscapeString(string value, bool singleQuote = true)
    {
        return singleQuote
            ? $"'{value.Replace("'", "\\'")}'"
            : $"\"{value.Replace("\"", "\\\"")}\"";
    }

    /// <inheritdoc/>
    public void WriteTo(SNbtWriter writer)
    {
        writer.WriteStringValue(Value, true);
    }
    
    /// <summary>
    /// Converts the specified value to its string NBT value form.
    /// </summary>
    /// <param name="from">The value to convert from.</param>
    /// <returns>The string NBT value form.</returns>
    public static implicit operator SNbtStringValue(string from)
    {
        return new SNbtStringValue(from);
    }

    /// <summary>
    /// Converts the specified string NBT form of value to its value representation.
    /// </summary>
    /// <param name="from">The string NBT value to convert from.</param>
    /// <returns>The value form.</returns>
    public static implicit operator string(SNbtStringValue from)
    {
        return from.Value;
    }
}