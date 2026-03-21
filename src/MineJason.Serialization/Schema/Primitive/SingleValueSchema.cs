// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

namespace MineJason.Serialization.Schema.Primitive;

using MineJason.Serialization.IO;
using MineJason.Serialization.Utilities.Results;

/// <summary>
/// Encodes or decodes <see cref="float"/>. This class cannot be inherited.
/// </summary>
/// <typeparam name="TElement">The element type.</typeparam>
public sealed class SingleValueSchema : ValueSchema<float>
{
    public static readonly SingleValueSchema Instance = new();

    private SingleValueSchema()
    {
    }

    public override Result<float> Decode<TElement>(TElement value, IValueDecoder<TElement> decoder)
    {
        return decoder.GetSingle(value);
    }

    public override Result<TElement> Encode<TElement>(float value, IValueEncoder<TElement> encoder,
        string? elementName = null)
    {
        return encoder.CreateSingle(value, elementName);
    }
}