// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

namespace MineJason.Serialization.Schema.Primitive;

using MineJason.Serialization.IO;
using MineJason.Serialization.Utilities.Results;

/// <summary>
/// Encodes or decodes <see cref="short"/>. This class cannot be inherited.
/// </summary>
public sealed class Int16ValueSchema : ValueSchema<short>
{
    public static readonly Int16ValueSchema Instance = new();

    private Int16ValueSchema()
    {
    }

    public override Result<short> Decode<TElement>(TElement value, IValueDecoder<TElement> decoder)
    {
        return decoder.GetInt16(value);
    }

    public override Result<TElement> Encode<TElement>(short value, IValueEncoder<TElement> encoder,
        string? elementName = null)
    {
        return encoder.CreateInt16(value, elementName);
    }
}