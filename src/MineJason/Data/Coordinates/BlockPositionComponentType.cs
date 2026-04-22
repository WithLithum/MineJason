// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

namespace MineJason.Data.Coordinates;

/// <summary>
/// An enumeration of types allowed for a block position component.
/// </summary>
public enum BlockPositionComponentType
{
    /// <summary>
    /// Absolute coordinates.
    /// </summary>
    Absolute,
    /// <summary>
    /// Coordinates relative to the local of execution of the relevant command.
    /// </summary>
    Relative,
    /// <summary>
    /// Coordinates relative to the directions of the executor.
    /// </summary>
    Local
}
