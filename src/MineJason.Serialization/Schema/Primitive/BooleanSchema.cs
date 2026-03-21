// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

namespace MineJason.Serialization.Schema.Primitive;

using MineJason.Serialization.IO;
using MineJason.Serialization.Utilities.Results;

/// <summary>
/// Encodes or decodes <see cref="bool"/>. This class cannot be inherited.
/// </summary>
public sealed class BooleanSchema : ValueSchema<bool>
{
    public static readonly BooleanSchema Instance = new();

    private BooleanSchema()
    {
    }

    public override Result<TElement> Encode<TElement>(bool value, IValueEncoder<TElement> encoder,
        string? elementName = null)
    {
        return encoder.CreateBoolean(value, elementName);
    }

    public override Result<bool> Decode<TElement>(TElement value, IValueDecoder<TElement> decoder)
    {
        return decoder.GetBoolean(value);
    }
}