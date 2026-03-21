// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

namespace MineJason.Serialization.Schema.Primitive;

using MineJason.Serialization.IO;
using MineJason.Serialization.Utilities.Results;

/// <summary>
/// Encodes or decodes <see cref="byte"/>. This class cannot be inherited.
/// </summary>
public sealed class ByteValueSchema : ValueSchema<byte>
{
    public static readonly ByteValueSchema Instance = new();

    private ByteValueSchema()
    {
    }

    public override Result<byte> Decode<TElement>(TElement value, IValueDecoder<TElement> decoder)
    {
        return decoder.GetByte(value);
    }

    public override Result<TElement> Encode<TElement>(byte value, IValueEncoder<TElement> encoder,
        string? elementName = null)
    {
        return encoder.CreateByte(value, elementName);
    }
}