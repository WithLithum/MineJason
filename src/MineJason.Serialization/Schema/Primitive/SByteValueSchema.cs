// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

namespace MineJason.Serialization.Schema.Primitive;

using MineJason.Serialization.IO;
using MineJason.Serialization.Utilities.Results;

/// <summary>
/// Encodes or decodes <see cref="sbyte"/>. This class cannot be inherited.
/// </summary>
public sealed class SByteValueSchema : ValueSchema<sbyte>
{
    public static readonly SByteValueSchema Instance = new();

    private SByteValueSchema()
    {
    }

    public override Result<sbyte> Decode<TElement>(TElement value, IValueDecoder<TElement> decoder)
    {
        return decoder.GetSByte(value);
    }

    public override Result<TElement> Encode<TElement>(sbyte value, IValueEncoder<TElement> encoder,
        string? elementName = null)
    {
        return encoder.CreateSByte(value, elementName);
    }
}