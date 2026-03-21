// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

namespace MineJason.Serialization.IO;

using JetBrains.Annotations;
using MineJason.Serialization.Utilities.Results;

/// <summary>
/// Defines an interface for write access to a collection of the specified type that outputs a
/// collection of the specified elements.
/// </summary>
/// <typeparam name="TValue">The type of the value.</typeparam>
/// <typeparam name="TElement">The type of the element.</typeparam>
public interface IArrayLikeWritable<in TValue, out TElement>
{
    [MustUseReturnValue]
    Result Add(TValue value);

    TElement GetContainer();
}