// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

namespace MineJason.Serialization.IO;

using MineJason.Serialization.Utilities.Results;

/// <summary>
/// Defines key-value compound, which the value is represented by the specified key, and is
/// identified by a string name.
/// </summary>
/// <typeparam name="T">The type of the value.</typeparam>
public interface IReadOnlyObjectLike<T>
{
    bool ContainsKey(string name);

    /// <summary>
    /// Returns an enumerable that can be used to enumerate all properties within this instance.
    /// </summary>
    /// <returns>The enumerable.</returns>
    IEnumerable<KeyValuePair<string, T>> EnumerateObject();

    Result<T> Get(string name);
}