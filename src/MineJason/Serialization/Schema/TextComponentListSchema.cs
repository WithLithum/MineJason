// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

namespace MineJason.Serialization.Schema;

using MineJason.Serialization.IO;
using MineJason.Serialization.Utilities.Results;
using MineJason.Text;

/// <summary>
/// Defines a schema that encodes or decodes a list of text components to or from the given element
/// type.
/// </summary>
public class TextComponentListSchema : ValueSchema<IReadOnlyList<TextComponent>>
{
    /// <summary>
    /// The singleton instance.
    /// </summary>
    public static readonly TextComponentListSchema Instance = new();

    /// <inheritdoc />
    public override Result<TElement> Encode<TElement>(IReadOnlyList<TextComponent> value,
        IValueEncoder<TElement> encoder,
        string? elementName = null)
    {
        var col = encoder.CreateCollection(elementName);

        foreach (var component in value)
        {
            var elementResult = TextComponentSchema.Instance.Encode(component,
                encoder);

            var addResult = col.Add(elementResult);
            if (!addResult.IsSuccess())
            {
                return addResult.AsError();
            }
        }

        return col.GetContainer();
    }

    /// <inheritdoc />
    public override Result<IReadOnlyList<TextComponent>> Decode<TElement>(TElement value,
        IValueDecoder<TElement> decoder)
    {
        var colResult = decoder.GetCollection(value);
        if (!colResult.IsSuccess(out var collection))
        {
            return colResult.AsError();
        }

        var list = new List<TextComponent>();

        // Decode the list
        foreach (var element in collection)
        {
            var textResult = TextComponentSchema.Instance.Decode(element,
                decoder);
            if (!textResult.IsSuccess(out var text)) return textResult.AsError();

            list.Add(text);
        }

        return list.AsReadOnly();
    }
}