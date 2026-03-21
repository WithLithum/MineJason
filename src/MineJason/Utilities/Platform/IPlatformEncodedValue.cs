// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: LGPL-3.0-or-later

namespace MineJason.Utilities.Platform;

using MineJason.Serialization.Schema;

/// <summary>
/// Defines a value that can only be encoded and with the schema provided by the platform.
/// </summary>
public interface IPlatformEncodedValue
{
    /// <summary>
    /// Gets the value of this instance.
    /// </summary>
    /// <returns>The value.</returns>
    object GetValue();

    /// <summary>
    /// Gets the schema that can encode the value.
    /// </summary>
    /// <typeparam name="TElement">The schema.</typeparam>
    /// <returns>The value.</returns>
    IValueSchema<TElement> GetSchema<TElement>();
}