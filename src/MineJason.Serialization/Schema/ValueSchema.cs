// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

namespace MineJason.Serialization.Schema;

using MineJason.Serialization.IO;
using MineJason.Serialization.Utilities.Results;

/// <summary>
/// Defines a strongly typed value schema that converts a value of given type to or from the given
/// element type.
/// </summary>
/// <typeparam name="TValue">The type of the value to convert.</typeparam>
/// <typeparam name="TElement">The element type to convert to.</typeparam>
public abstract class ValueSchema<TValue> : IValueSchema<TValue>
{
    /// <summary>
    /// Encodes the specified value to <typeparamref name="TElement"/>.
    /// </summary>
    /// <param name="value">The value to encode.</param>
    /// <param name="encoder">The encoder to use.</param>
    /// <param name="elementName">The name of the element to output.</param>
    /// <returns>The encoded element.</returns>
    public abstract Result<TElement> Encode<TElement>(TValue value, IValueEncoder<TElement> encoder,
        string? elementName = null);

    /// <summary>
    /// Decodes the specified <typeparamref name="TElement"/> to the value it represents.
    /// </summary>
    /// <param name="value">The element to decode.</param>
    /// <param name="decoder">The decoder to use.</param>
    /// <returns>
    /// An instance of <see cref="Result{T}"/> containing either an error or the result value.
    /// </returns>
    public abstract Result<TValue> Decode<TElement>(TElement value, IValueDecoder<TElement> decoder);

    Result<TElement> IValueSchema.Encode<TElement>(object value,
        IValueEncoder<TElement> encoder,
        string? elementName)
    {
        if (value is not TValue realValue)
        {
            throw new ArgumentException("The specified value type is not an acceptable type.",
                nameof(value));
        }

        return Encode(realValue, encoder, elementName);
    }

    Result<object> IValueSchema.Decode<TElement>(TElement value, IValueDecoder<TElement> decoder)
    {
        return Decode(value, decoder);
    }
}