// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

namespace MineJason.SNbt.Values;

using MineJason.SNbt.Parsing;

/// <summary>
/// Defines an object that can be converted to NBT form via <see cref="SNbtWriter"/>.
/// </summary>
public interface ISNbtWritable
{
    /// <summary>
    /// Gets the type of this tag.
    /// </summary>
    SNbtTagType Type { get; }

    /// <summary>
    /// Writes the NBT form of this instance to the specified writer.
    /// </summary>
    /// <param name="writer">The writer.</param>
    void WriteTo(SNbtWriter writer);
}