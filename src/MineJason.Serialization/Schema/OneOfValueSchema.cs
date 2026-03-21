// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

namespace MineJason.Serialization.Schema;

using System.Text;
using MineJason.Serialization.IO;
using MineJason.Serialization.Utilities.Results;

/// <summary>
/// Decodes the specified value by attempting to use all specified schemas to decode, and returns
/// the first successful result. Encodes by using the first available schema.
/// </summary>
/// <typeparam name="TValue">The type of the value.</typeparam>
/// <typeparam name="TElement">The element type.</typeparam>
public class OneOfValueSchema<TValue> : ValueSchema<TValue>
{
    private readonly IEnumerable<IValueSchema<TValue>> _children;

    public OneOfValueSchema(IEnumerable<IValueSchema<TValue>> children)
    {
        _children = children;
    }

    public override Result<TValue> Decode<TElement>(TElement value, IValueDecoder<TElement> decoder)
    {
        // Create a new string builder to save error into. 
        var eb = new StringBuilder();
        eb.AppendLine("None of the schemas were successful");

        var counter = 0;

        foreach (var child in _children)
        {
            counter++;

            var result = child.Decode(value, decoder);
            if (result.Error != null)
            {
                eb.AppendLine($" #{counter}: {result.Error}");
                continue;
            }

            return result;
        }

        // If none worked
        return Errors.Error(eb.ToString());
    }

    public override Result<TElement> Encode<TElement>(TValue value, IValueEncoder<TElement> encoder,
        string? elementName = null)
    {
        var first = _children.FirstOrDefault();
        if (first == null)
        {
            throw new InvalidOperationException("No transformer exists.");
        }

        return first.Encode(value, encoder, elementName);
    }
}