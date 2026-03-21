// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

namespace MineJason.Serialization.Schema;

using MineJason.Serialization.IO;
using MineJason.Serialization.Utilities.Results;

/// <summary>
/// Defines encode and decode functions that encodes or decodes an arbitrary object to or from the
/// given element type.
/// </summary>
/// <remarks>
/// <para>
/// This interface is not meant for direct consumption as it is not strongly typed. Instead, it is
/// to be used by other value schemas for storing child schemas heterogenously.
/// </para>
/// <para>
/// If possible, implementers should inherit from <see cref="ValueSchema{TValue}"/>, or
/// implement <see cref="IValueSchema{TValue}"/> and implement this interface explicitly.
/// </para>
/// </remarks>
/// <seealso cref="ValueSchema{TValue}"/>
public interface IValueSchema
{
    /// <summary>
    /// Converts the specified object to an element.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <param name="encoder">The encoder to use.</param>
    /// <param name="elementName">The name of the element to output.</param>
    /// <returns>The converted element.</returns>
    Result<TElement> Encode<TElement>(object value, IValueEncoder<TElement> encoder,
        string? elementName = null);

    /// <summary>
    /// Converts the specified element to an object.
    /// </summary>
    /// <param name="value">The element to convert from.</param>
    /// <param name="decoder">The decoder to use.</param>
    /// <returns>
    /// An instance of <see cref="Result{T}"/> containing either an error message or the
    /// resulting value.
    /// </returns>
    Result<object> Decode<TElement>(TElement value, IValueDecoder<TElement> decoder);
}