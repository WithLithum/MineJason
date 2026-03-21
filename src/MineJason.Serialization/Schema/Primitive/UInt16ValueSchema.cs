// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

namespace MineJason.Serialization.Schema.Primitive;

using MineJason.Serialization.IO;
using MineJason.Serialization.Utilities.Results;

/// <summary>
/// Encodes or decodes <see cref="ushort"/>. This class cannot be inherited.
/// </summary>
public sealed class UInt16ValueSchema : ValueSchema<ushort>
{
    public static readonly UInt16ValueSchema Instance = new();

    private UInt16ValueSchema()
    {
    }

    public override Result<ushort> Decode<TElement>(TElement value, IValueDecoder<TElement> decoder)
    {
        return decoder.GetUInt16(value);
    }

    public override Result<TElement> Encode<TElement>(ushort value, IValueEncoder<TElement> encoder,
        string? elementName = null)
    {
        return encoder.CreateUInt16(value, elementName);
    }
}