// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

namespace MineJason.Serialization.Schema.Primitive;

using MineJason.Serialization.IO;
using MineJason.Serialization.Utilities.Results;

/// <summary>
/// Encodes or decodes <see cref="ulong"/>.
/// </summary>
public sealed class UInt64ValueSchema : ValueSchema<ulong>
{
    public static readonly UInt64ValueSchema Instance = new();

    private UInt64ValueSchema()
    {
    }

    public override Result<ulong> Decode<TElement>(TElement value, IValueDecoder<TElement> decoder)
    {
        return decoder.GetUInt64(value);
    }

    public override Result<TElement> Encode<TElement>(ulong value, IValueEncoder<TElement> encoder,
        string? elementName = null)
    {
        return encoder.CreateUInt64(value, elementName);
    }
}