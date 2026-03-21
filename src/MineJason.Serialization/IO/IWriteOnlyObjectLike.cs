// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

namespace MineJason.Serialization.IO;

using JetBrains.Annotations;
using MineJason.Serialization.Utilities.Results;

/// <summary>
/// Defines key-value compound, which the value is represented by the specified key, and is
/// identified by a string name.
/// </summary>
/// <typeparam name="TElement">The type of the value.</typeparam>
public interface IWriteOnlyObjectLike<TElement>
{
    /// <summary>
    /// Attempts to add the specified value to the map.
    /// </summary>
    /// <param name="key">The key</param>
    /// <param name="value">The value.</param>
    /// <returns>
    /// An instance of <see cref="Result"/> indicating whether the operation was successful, and
    /// if not, an error message.
    /// </returns>
    [MustUseReturnValue]
    Result Add(string key, TElement value);

    /// <summary>
    /// Gets the encoded compound value.
    /// </summary>
    /// <returns>The encoded value.</returns>
    TElement GetContainer();
}