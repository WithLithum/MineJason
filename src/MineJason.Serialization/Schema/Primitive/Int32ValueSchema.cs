// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

namespace MineJason.Serialization.Schema.Primitive;

using MineJason.Serialization.IO;
using MineJason.Serialization.Utilities.Results;

/// <summary>
/// Encodes or decodes <see cref="int"/>. This class cannot be inherited.
/// </summary>
public sealed class Int32ValueSchema : ValueSchema<int>
{
    public static readonly Int32ValueSchema Instance = new();

    private Int32ValueSchema()
    {
    }

    public override Result<int> Decode<TElement>(TElement value, IValueDecoder<TElement> decoder)
    {
        return decoder.GetInt32(value);
    }

    public override Result<TElement> Encode<TElement>(int value, IValueEncoder<TElement> encoder,
        string? elementName = null)
    {
        return encoder.CreateInt32(value, elementName);
    }
}