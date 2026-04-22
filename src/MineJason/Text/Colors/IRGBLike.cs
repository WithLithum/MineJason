// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

namespace MineJason.Text.Colors;

/// <summary>
/// Defines an object that represents, or can be represented in, RGB values.
/// </summary>
public interface IRGBLike
{
    /// <summary>
    /// Gets the integral representation of the RGB-triplet value.
    /// </summary>
    /// <returns>The RGB triplet value.</returns>
    int AsTriplet();
}
