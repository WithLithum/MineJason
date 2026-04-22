// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

namespace MineJason.Serialization.Schema;

using MineJason.Serialization.IO;
using MineJason.Serialization.Utilities;
using MineJason.Serialization.Utilities.Results;
using MineJason.Utilities.Platform;

internal class PlatformValueSchema : ValueSchema<IPlatformEncodedValue>
{
    private PlatformValueSchema()
    {
    }

    internal static readonly PlatformValueSchema Instance = new();

    /// <inheritdoc />
    public override Result<TElement> Encode<TElement>(IPlatformEncodedValue value,
        IValueEncoder<TElement> encoder,
        string? elementName = null)
    {
        var schema = PlatformService.Provider?.GetValueSchema(value.GetType());
        if (schema == null)
        {
            return Errors.Error("Don't know how to encode the platform value");
        }

        var v = value.GetValue();
        var valueSchema = value.GetSchema<TElement>();

        return valueSchema.Encode(v, encoder, elementName);
    }

    /// <inheritdoc />
    public override Result<IPlatformEncodedValue> Decode<TElement>(TElement value,
        IValueDecoder<TElement> decoder)
    {
        return Errors.Error("Decoding of platform values are not supported");
    }
}