// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: LGPL-3.0-or-later

namespace MineJason.Serialization.Schema;

using MineJason.Data;
using MineJason.Data.Selectors;
using MineJason.Serialization.IO;
using MineJason.Serialization.Utilities.Results;

/// <summary>
/// Converts entity selectors from or to the given element type.
/// </summary>
public class EntitySelectorSchema : ValueSchema<EntitySelector>
{
    /// <summary>
    /// The singleton instance.
    /// </summary>
    public static readonly EntitySelectorSchema Instance = new();

    private EntitySelectorSchema()
    {
    }

    /// <inheritdoc />
    public override Result<TElement> Encode<TElement>(EntitySelector value,
        IValueEncoder<TElement> encoder,
        string? elementName = null)
    {
        string str;
        try
        {
            // TODO make this more proper
            str = EntitySelectorStringFormatter.ToString(value);
        }
        catch (Exception ex)
        {
            return Errors.Error(ex.Message);
        }

        return encoder.CreateString(str, elementName);
    }

    /// <inheritdoc />
    public override Result<EntitySelector> Decode<TElement>(TElement value,
        IValueDecoder<TElement> decoder)
    {
        var strResult = decoder.GetString(value);
        if (!strResult.IsSuccess(out var str)) return strResult.AsError();

        try
        {
            return EntitySelectorStringFormatter.ParseSelector(str);
        }
        catch (Exception ex)
        {
            return Errors.Error(ex.Message);
        }
    }
}