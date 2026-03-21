// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

namespace MineJason.Serialization.Schema.Primitive;

using MineJason.Serialization.IO;
using MineJason.Serialization.Utilities.Results;

/// <summary>
/// Encodes or decodes <see cref="uint"/>.
/// </summary>
public sealed class UInt32ValueSchema : ValueSchema<uint>
{
    public static readonly UInt32ValueSchema Instance = new();

    private UInt32ValueSchema()
    {
    }

    public override Result<uint> Decode<TElement>(TElement value, IValueDecoder<TElement> decoder)
    {
        return decoder.GetUInt32(value);
    }

    public override Result<TElement> Encode<TElement>(uint value, IValueEncoder<TElement> encoder,
        string? elementName = null)
    {
        return encoder.CreateUInt32(value, elementName);
    }
}