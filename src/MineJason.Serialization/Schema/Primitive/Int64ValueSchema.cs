// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

namespace MineJason.Serialization.Schema.Primitive;

using MineJason.Serialization.IO;
using MineJason.Serialization.Utilities.Results;

/// <summary>
/// Encodes or decodes <see cref="long"/>. This class cannot be inherited.
/// </summary>
public sealed class Int64ValueSchema : ValueSchema<long>
{
    public static readonly Int64ValueSchema Instance = new();

    private Int64ValueSchema()
    {
    }

    public override Result<long> Decode<TElement>(TElement value, IValueDecoder<TElement> decoder)
    {
        return decoder.GetInt64(value);
    }

    public override Result<TElement> Encode<TElement>(long value, IValueEncoder<TElement> encoder,
        string? elementName = null)
    {
        return encoder.CreateInt64(value, elementName);
    }
}