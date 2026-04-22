// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

namespace MineJason.Serialization.Schema;

using MineJason.Data.Components;
using MineJason.Serialization.IO;
using MineJason.Serialization.Utilities.Results;
using MineJason.Utilities.Platform;
using DataComponentMap = IReadOnlyDictionary<ResourceLocation, Data.Components.IDataComponentValue>;

/// <summary>
/// Defines a schema that encodes or decodes a dictionary of resource locations and data components
/// to or from the given element type.
/// </summary>
public class DataComponentMapSchema : ValueSchema<DataComponentMap>
{
    private static string GetComponentId(ResourceLocation id, bool removal)
    {
        return removal
            ? $"!{id}"
            : id.ToString();
    }

    /// <inheritdoc />
    public override Result<TElement> Encode<TElement>(DataComponentMap value,
        IValueEncoder<TElement> encoder,
        string? elementName = null)
    {
        var dataProvider = PlatformService.Provider;
        var col = encoder.CreateObjectLike();
        foreach (var (key, obj) in value)
        {
            var writeKey = GetComponentId(key, obj.IsRemoval);

            if (obj.IsRemoval)
            {
                // Store an empty object.
                col.Add(writeKey, encoder.CreateObjectLike(writeKey).GetContainer());
                continue;
            }

            var schema = dataProvider?.GetDataComponentSchema(key);
            if (schema == null)
            {
                return Errors.Error($"Don't know how to encode '{key}'");
            }

            var encodeResult = schema.Encode(obj, encoder, key.ToString());
            if (!encodeResult.IsSuccess(out var encoded))
            {
                return Errors.Error($"[{key}] {encodeResult.Error}");
            }

            col.Add(writeKey, encoded);
        }

        return col.GetContainer();
    }

    #region Decoder

    /// <inheritdoc />
    public override Result<DataComponentMap> Decode<TElement>(
        TElement value,
        IValueDecoder<TElement> decoder)
    {
        var colResult = decoder.GetObjectLike(value);
        if (!colResult.IsSuccess(out var col))
        {
            return colResult.AsError();
        }

        var dict = new Dictionary<ResourceLocation, IDataComponentValue>();

        foreach (var (key, obj) in col.EnumerateObject())
        {
            // Parse key and determine if the key is a removal. If it is, 
            var realKey = key.AsSpan();
            var isRemoval = false;
            if (key.StartsWith('!'))
            {
                realKey = realKey[1..];
                isRemoval = true;
            }

            var decodeResult = DecodeOneComponent(realKey, obj, isRemoval, decoder, dict);
            if (!decodeResult.IsSuccess())
            {
                return decodeResult.AsError();
            }
        }

        return dict;
    }

    private static Result DecodeOneComponent<TElement>(ReadOnlySpan<char> realKey,
        TElement obj,
        bool isRemoval,
        IValueDecoder<TElement> decoder,
        Dictionary<ResourceLocation, IDataComponentValue> dict)
    {
        if (!ResourceLocation.TryParse(realKey, out var id))
        {
            return Errors.Error("Malformed data component ID");
        }

        // If it is a removal patch, then require it to be an empty object ('{}'). If not, then
        // it is a failure.
        if (isRemoval)
        {
            return DecodeRemoval(obj, decoder, id, dict);
        }

        // Decode component value with the schema we got from the provider.
        var decodeResult = DecodeComponentValue(decoder, id, obj);
        if (!decodeResult.IsSuccess(out var decoded))
        {
            return decodeResult.AsError();
        }

        dict.Add(id, decoded);
        return Result.Success();
    }

    private static Result DecodeRemoval<TElement>(TElement obj,
        IValueDecoder<TElement> decoder,
        ResourceLocation id,
        Dictionary<ResourceLocation, IDataComponentValue> dict)
    {
        var checkEmpty = CheckEmptyCompound(decoder.GetObjectLike(obj));
        if (checkEmpty.Error != null)
        {
            return Errors.Error(checkEmpty.Error);
        }

        dict.Add(id, EmptyDataComponentValue.Instance);
        return Result.Success();
    }

    private static Result<IDataComponentValue> DecodeComponentValue<TElement>(
        IValueDecoder<TElement> decoder,
        ResourceLocation id,
        TElement element)
    {
        var schema = PlatformService.Provider?.GetDataComponentSchema(id);
        if (schema == null)
        {
            return Errors.Error($"Don't know how to decode '{id}'");
        }

        return schema.Decode(element, decoder);
    }

    private static Result CheckEmptyCompound<TElement>(Result<IReadOnlyObjectLike<TElement>> obj)
    {
        if (!obj.IsSuccess(out var objLike))
        {
            return obj.AsError();
        }

        if (objLike.EnumerateObject().Any())
        {
            return Errors.Error("Removal patch is not empty");
        }

        return Result.Success();
    }

    #endregion
}