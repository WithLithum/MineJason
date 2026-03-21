// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: LGPL-3.0-or-later

namespace MineJason.Data.Selectors;

using JetBrains.Annotations;

/// <summary>
/// An enumeration of all possible entity selector sorting modes.
/// </summary>
[PublicAPI]
public enum EntitySelectorSortMode
{
    /// <summary>
    /// Do not sort. The default option.
    /// </summary>
    Arbitrary,
    /// <summary>
    /// Sort by increasing distance.
    /// </summary>
    Nearest,
    /// <summary>
    /// Sort by decreasing distance.
    /// </summary>
    Furthest,
    /// <summary>
    /// Sort randomly.
    /// </summary>
    Random
}