// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

namespace MineJason.Serialization.Schema;

using MineJason.Data.Coordinates;
using MineJason.Serialization.IO;
using MineJason.Serialization.Utilities.Results;

/// <summary>
/// Defines a schema that encodes or decodes an instance of <see cref="BlockPosition"/> to or from
/// the given element type.
/// </summary>
public class BlockPositionSchema : ValueSchema<BlockPosition>
{
    /// <summary>
    /// The singleton instance.
    /// </summary>
    public static readonly BlockPositionSchema Instance = new();

    private BlockPositionSchema()
    {
    }

    /// <inheritdoc />
    public override Result<TElement> Encode<TElement>(BlockPosition value,
        IValueEncoder<TElement> encoder,
        string? elementName = null)
    {
        return encoder.CreateString(value.ToString(), elementName);
    }

    /// <inheritdoc />
    public override Result<BlockPosition> Decode<TElement>(TElement value,
        IValueDecoder<TElement> decoder)
    {
        var strResult = decoder.GetString(value);
        if (!strResult.IsSuccess(out var str))
        {
            return strResult.AsError();
        }

        try
        {
            // TODO make this more proper
            return BlockPosition.Parse(str);
        }
        catch (Exception ex)
        {
            return Errors.Error($"{ex.GetType()}: {ex.Message}");
        }
    }
}