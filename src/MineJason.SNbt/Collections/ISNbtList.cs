// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

namespace MineJason.SNbt;

using MineJason.SNbt.Values;

/// <summary>
/// A list of string NBT tags.
/// </summary>
public interface ISNbtList : ISNbtWritable
{
    /// <summary>
    /// Determines whether the specified value is allowed to be added as a member of this instance.
    /// </summary>
    /// <param name="value">The value to check.</param>
    /// <returns><see langword="true"/> if this instance accepts the specified value; otherwise, <see langword="false"/>.</returns>
    bool DoesAccept(ISNbtWritable value);

    /// <summary>
    /// Appends the specified value to the end of this list.
    /// </summary>
    /// <param name="value">The value to add.</param>
    /// <exception cref="ArgumentException">The specified value is not acceptable as an item of this instance.</exception>
    void Add(ISNbtWritable value);
}
