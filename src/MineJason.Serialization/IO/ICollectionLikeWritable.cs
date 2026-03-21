// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

namespace MineJason.Serialization.IO;

using MineJason.Serialization.Utilities.Results;

/// <summary>
/// Defines a writable collection-like interface containing the specified elements.
/// </summary>
/// <typeparam name="T">The element type.</typeparam>
public interface ICollectionLikeWritable<T>
{
    Result Add(T value);

    T GetContainer();
}