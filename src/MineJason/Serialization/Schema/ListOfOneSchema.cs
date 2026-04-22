// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

namespace MineJason.Serialization.Schema;

using MineJason.Serialization.IO;
using MineJason.Serialization.Utilities.Results;

internal sealed class ListOfOneSchema<TValue> : ValueSchema<IReadOnlyCollection<TValue>>
{
    private readonly IValueSchema<TValue> _schema;

    public ListOfOneSchema(IValueSchema<TValue> schema)
    {
        _schema = schema;
    }

    public override Result<TElement> Encode<TElement>(IReadOnlyCollection<TValue> value,
        IValueEncoder<TElement> encoder,
        string? elementName = null)
    {
        throw new NotSupportedException();
    }

    public override Result<IReadOnlyCollection<TValue>> Decode<TElement>(TElement value, IValueDecoder<TElement> decoder)
    {
        var singleResult = _schema.Decode(value, decoder);
        if (!singleResult) return singleResult.AsError();

        return new TValue[] { singleResult.Value! };
    }
}