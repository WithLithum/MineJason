// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

using MineJason.Serialization.IO;
using MineJason.Serialization.Utilities.Results;

namespace MineJason.Serialization.Schema.Primitive;
/// <summary>
/// Encodes or decodes <see cref="string"/>. This class cannot be inherited.
/// </summary>
public sealed class StringValueSchema : ValueSchema<string>
{
    public static readonly StringValueSchema Instance = new();

    private StringValueSchema()
    {
    }

    public override Result<string> Decode<TElement>(TElement value, IValueDecoder<TElement> decoder)
    {
        return decoder.GetString(value);
    }

    public override Result<TElement> Encode<TElement>(string value, IValueEncoder<TElement> encoder,
        string? elementName = null)
    {
        return encoder.CreateString(value, elementName);
    }
}