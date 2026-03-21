// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: LGPL-3.0-or-later

namespace MineJason.Serialization.Schema;

using MineJason.Serialization.IO;
using MineJason.Serialization.Utilities.Results;
using MineJason.Utilities;

/// <summary>
/// Defines the schema that encodes or decodes a <see cref="Guid"/> to or from an array of four
/// <see cref="int"/> instances represented in the specified element type.
/// </summary>
public class MinecraftUuidSchema : ValueSchema<Guid>
{
    /// <summary>
    /// The singleton instance.
    /// </summary>
    public static readonly MinecraftUuidSchema Instance = new();

    private MinecraftUuidSchema()
    {
    }

    /// <inheritdoc />
    public override Result<TElement> Encode<TElement>(Guid value,
        IValueEncoder<TElement> encoder,
        string? elementName = null)
    {
        var mc = value.ToMinecraft();
        var array = encoder.CreateInt32Array(elementName);

        var res1 = array.Add(mc[0]);
        var res2 = array.Add(mc[1]);
        var res3 = array.Add(mc[2]);
        var res4 = array.Add(mc[3]);

        if (!res1.IsSuccess() || !res2.IsSuccess() || !res3.IsSuccess() || !res4.IsSuccess())
        {
            return Errors.Error("Unable to add integers to UUID array");
        }

        return array.GetContainer();
    }

    /// <inheritdoc />
    public override Result<Guid> Decode<TElement>(TElement value,
        IValueDecoder<TElement> decoder)
    {
        var valResult = decoder.GetInt32Array(value);
        if (!valResult.IsSuccess(out var val))
        {
            return valResult.AsError();
        }

        if (val.TryGetNonEnumeratedCount(out var neCount)
            && neCount != 4)
        {
            return Errors.Error("Expected array of four integers");
        }

        var array = val.Take(4).ToArray();
        if (array.Length != 4)
        {
            return Errors.Error("Expected array of four integers");
        }

        return GuidExtensions.CreateGuid(array[0], array[1], array[2], array[3]);
    }
}