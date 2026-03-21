// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

namespace MineJason.Serialization.Schema;

using MineJason.Serialization.IO;
using MineJason.Serialization.Utilities.Results;

public class CollectionSchema<TValue> : ValueSchema<IReadOnlyCollection<TValue>>
{
    private readonly IValueSchema<TValue> _itemSchema;

    public CollectionSchema(IValueSchema<TValue> itemSchema)
    {
        _itemSchema = itemSchema;
    }

    public override Result<TElement> Encode<TElement>(IReadOnlyCollection<TValue> value,
        IValueEncoder<TElement> encoder,
        string? elementName = null)
    {
        var col = encoder.CreateCollection(elementName);
        foreach (var item in value)
        {
            var addResult = col.Add(_itemSchema.Encode(item, encoder));
            if (!addResult.IsSuccess())
            {
                return addResult.AsError();
            }
        }

        return col.GetContainer();
    }

    public override Result<IReadOnlyCollection<TValue>> Decode<TElement>(TElement value,
        IValueDecoder<TElement> decoder)
    {
        if (!decoder.GetCollection(value).Deconstruct(out var collection,
            out var status))
        {
            return status.AsError();
        }

        var list = new List<TValue>();
        foreach (var item in collection)
        {
            var addResult = list.Add(_itemSchema.Decode(item, decoder));
            if (!addResult.IsSuccess())
            {
                return addResult.AsError();
            }
        }

        return list;
    }
}