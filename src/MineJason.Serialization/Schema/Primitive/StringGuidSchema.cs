// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using JetBrains.Annotations;
using MineJason.Serialization.IO;
using MineJason.Serialization.Utilities.Results;

namespace MineJason.Serialization.Schema.Primitive;

/// <summary>
/// Encodes or decodes <see cref="Guid"/> in string form. This class cannot be inherited.
/// </summary>
public sealed class StringGuidSchema : ValueSchema<Guid>
{
    public static readonly StringGuidSchema Default = new("D", false);

    public StringGuidSchema() : this("D", false)
    {
    }

    public StringGuidSchema([StringSyntax("GuidFormat"), LocalizationRequired(false)]
        string formatString,
        bool parseExact)
    {
        _formatString = formatString;
        _parseExact = parseExact;
    }

    [StringSyntax("GuidFormat")]
    [LocalizationRequired(false)]
    private readonly string _formatString;

    private readonly bool _parseExact;

    public override Result<TElement> Encode<TElement>(Guid value,
        IValueEncoder<TElement> encoder,
        string? elementName = null)
    {
        return encoder.CreateString(value.ToString(_formatString), elementName);
    }

    public override Result<Guid> Decode<TElement>(TElement value, IValueDecoder<TElement> decoder)
    {
        var stringResult = decoder.GetString(value);
        if (!stringResult.IsSuccess(out var str))
        {
            return stringResult.AsError();
        }

        var success = _parseExact
            ? Guid.TryParseExact(str, _formatString, out var result)
            : Guid.TryParse(str, CultureInfo.InvariantCulture, out result);
        if (!success)
        {
            return Errors.Error("Invalid or malformed UUID");
        }

        return result;
    }
}