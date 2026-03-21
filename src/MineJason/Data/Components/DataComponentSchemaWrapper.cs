// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: LGPL-3.0-or-later

namespace MineJason.Data.Components;

using MineJason.Serialization.IO;
using MineJason.Serialization.Schema;
using MineJason.Serialization.Utilities.Results;

internal class DataComponentSchemaWrapper<TValue> :
    ValueSchema<IDataComponentValue>
{
    private readonly IValueSchema<TValue> _parent;

    internal DataComponentSchemaWrapper(IValueSchema<TValue> parent)
    {
        _parent = parent;
    }

    public override Result<TElement> Encode<TElement>(IDataComponentValue value,
        IValueEncoder<TElement> encoder,
        string? elementName = null)
    {
        if (value.IsRemoval)
        {
            // Return an empty collection for this special case.
            return encoder.CreateObjectLike(elementName).GetContainer();
        }

        if (value is not DataComponentValue<TValue> componentValue)
        {
            return Errors.Error("The specified value is not a DataComponentValue");
        }

        return _parent.Encode(componentValue.Value, encoder, elementName);
    }

    public override Result<IDataComponentValue> Decode<TElement>(TElement value, IValueDecoder<TElement> decoder)
    {
        var decodeResult = _parent.Decode(value, decoder);
        if (decodeResult.Error != null)
        {
            return decodeResult.AsError();
        }

        return new DataComponentValue<TValue>(decodeResult.Value!);
    }
}