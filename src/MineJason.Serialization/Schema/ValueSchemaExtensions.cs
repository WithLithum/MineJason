// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

namespace MineJason.Serialization.Schema;

using MineJason.Serialization.IO;
using MineJason.Serialization.Utilities.Results;

/// <summary>
/// Provides various extension methods to value schema implementations.
/// </summary>
public static class ValueSchemaExtensions
{
    /// <summary>
    /// Converts the specified result value to the given element type. If the specified result does
    /// not contain a value, returns an empty or errored result accordingly.
    /// </summary>
    /// <param name="schema">The schema to convert the value with.</param>
    /// <param name="value">
    /// The <see cref="Result{T}"/> that contains the value to encode.
    /// </param>
    /// <param name="encoder">The encoder to use.</param>
    /// <param name="elementName">The element name to use.</param>
    /// <typeparam name="TValue">The type of the value to convert from.</typeparam>
    /// <typeparam name="TElement">The element type to convert to.</typeparam>
    /// <returns>
    /// The result of the conversion attempt.
    /// </returns>
    public static Result<TElement> Encode<TValue, TElement>(
        this IValueSchema<TValue> schema,
        Result<TValue> value,
        IValueEncoder<TElement> encoder,
        string? elementName = null)
    {
        if (value.Error != null)
        {
            return value.AsError();
        }

        return schema.Encode(value.Value!, encoder, elementName);
    }

    /// <summary>
    /// Converts the specified element result value to the specified type. If the specified result
    /// does not contain a value, returns an errored or empty value accordingly.
    /// </summary>
    /// <param name="schema">The schema to convert the element with.</param>
    /// <param name="element">The element to convert.</param>
    /// <param name="decoder">The decoder to use.</param>
    /// <typeparam name="TValue">The type of the value to convert to.</typeparam>
    /// <typeparam name="TElement">The type of the element to convert from.</typeparam>
    /// <returns>The result of the conversion attempt.</returns>
    public static Result<TValue> Decode<TValue, TElement>(
        this IValueSchema<TValue> schema,
        Result<TElement> element,
        IValueDecoder<TElement> decoder)
    {
        if (element.Error != null)
        {
            return element.AsError();
        }

        return schema.Decode(element.Value!, decoder);
    }

    /// <summary>
    /// Converts the specified element result value to the specified type. If the specified result
    /// does not contain a value, returns an errored or empty value accordingly.
    /// </summary>
    /// <param name="schema">The schema to convert the element with.</param>
    /// <param name="element">The element to convert.</param>
    /// <param name="decoder">The decoder to use.</param>
    /// <param name="hasResultValue">
    /// If <see langword="false"/>, <paramref name="result"/> is <see langword="default"/>.
    /// </param>
    /// <param name="result">The result value. If <paramref name="hasResultValue"/> is
    /// <see langword="false"/>, this value is <see langword="default"/>.
    /// </param>
    /// <typeparam name="TValue">The type of the value to convert to.</typeparam>
    /// <typeparam name="TElement">The type of the element to convert from.</typeparam>
    /// <returns>The operation result of the conversion attempt.</returns>
    public static Result Decode<TValue, TElement>(
        this IValueSchema<TValue> schema,
        Result<TElement> element,
        IValueDecoder<TElement> decoder,
        out TValue? result)
    {
        if (!element.IsSuccess(out var v))
        {
            result = default;
            return element.AsError();
        }

        var decodeResult = schema.Decode(v, decoder);
        if (!decodeResult.IsSuccess(out result))
        {
            return decodeResult.AsError();
        }

        return Result.Success();
    }
}