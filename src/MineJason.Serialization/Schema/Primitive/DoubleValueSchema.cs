// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

namespace MineJason.Serialization.Schema.Primitive;

using MineJason.Serialization.IO;
using MineJason.Serialization.Utilities.Results;

/// <summary>
/// Encodes or decodes <see cref="double"/>. This class cannot be inherited.
/// </summary>
public sealed class DoubleValueSchema : ValueSchema<double>
{
    public static readonly DoubleValueSchema Instance = new();

    private DoubleValueSchema()
    {
    }

    public override Result<double> Decode<TElement>(TElement value, IValueDecoder<TElement> decoder)
    {
        return decoder.GetDouble(value);
    }

    public override Result<TElement> Encode<TElement>(double value, IValueEncoder<TElement> encoder,
        string? elementName = null)
    {
        return encoder.CreateDouble(value, elementName);
    }
}