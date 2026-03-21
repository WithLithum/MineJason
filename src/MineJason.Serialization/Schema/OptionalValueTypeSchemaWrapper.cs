// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

namespace MineJason.Serialization.Schema;

using MineJason.Serialization.IO;
using MineJason.Serialization.Utilities.Results;

internal class OptionalValueTypeSchemaWrapper<TValue> : ValueSchema<TValue?>
    where TValue : struct
{
    private readonly IValueSchema<TValue> _baseSchema;

    internal OptionalValueTypeSchemaWrapper(IValueSchema<TValue> baseSchema)
    {
        _baseSchema = baseSchema;
    }

    public override Result<TElement> Encode<TElement>(TValue? value, IValueEncoder<TElement> encoder,
        string? elementName = null)
    {
        if (!value.HasValue)
        {
            return Errors.NullDisallowed;
        }

        return _baseSchema.Encode(value.Value, encoder, elementName);
    }

    public override Result<TValue?> Decode<TElement>(TElement value, IValueDecoder<TElement> decoder)
    {
        if (!_baseSchema.Decode(value, decoder)
                .Deconstruct(out var result, out var status))
        {
            return status.AsError();
        }

        return result;
    }
}