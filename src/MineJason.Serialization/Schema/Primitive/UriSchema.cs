// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

namespace MineJason.Serialization.Schema.Primitive;

using MineJason.Serialization.IO;
using MineJason.Serialization.Utilities.Results;

public sealed class UriSchema : ValueSchema<Uri>
{
    /// <summary>
    /// The singleton instance.
    /// </summary>
    public static readonly UriSchema Instance = new();

    private UriSchema()
    {
    }

    public override Result<TElement> Encode<TElement>(Uri value,
        IValueEncoder<TElement> encoder,
        string? elementName = null)
    {
        return encoder.CreateString(value.ToString(), elementName);
    }

    public override Result<Uri> Decode<TElement>(TElement value, IValueDecoder<TElement> decoder)
    {
        var result = decoder.GetString(value);
        if (result.Error != null)
        {
            return result.AsError();
        }

        if (!Uri.TryCreate(result.Value!, UriKind.RelativeOrAbsolute, out var uri))
        {
            return Errors.Error("Malformed URI");
        }

        return uri;
    }
}