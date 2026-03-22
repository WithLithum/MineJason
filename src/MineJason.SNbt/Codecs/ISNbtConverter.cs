// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

namespace MineJason.SNbt.Codecs;

/// <summary>
/// Defines a converter that converts objects or values to string NBT.
/// </summary>
[Obsolete("Use Serialization instead.")]
public interface ISNbtConverter
{
    /// <summary>
    /// Writes the specified value to the specified writer as its string NBT representation.
    /// </summary>
    /// <param name="value">The value to write.</param>
    /// <param name="writer">The writer to write to.</param>
    /// <typeparam name="T">The type of the value.</typeparam>
    void WriteTo<T>(T value, SNbtWriter writer);

    /// <summary>
    /// Writes the specified value to the specified writer as its string NBT representation.
    /// </summary>
    /// <param name="value">The value to write.</param>
    /// <param name="writer">The writer to write to.</param>
    void WriteTo(object value, SNbtWriter writer);
}