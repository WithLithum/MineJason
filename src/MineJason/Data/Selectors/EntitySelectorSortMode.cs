// Copyright (c) WithLithum & contributors 2023-2024. All rights reserved.
// Licensed under the GNU Lesser General Public License, either version 3 or
// (at your opinion) any later version.

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