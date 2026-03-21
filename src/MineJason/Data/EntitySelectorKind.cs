// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: LGPL-3.0-or-later

namespace MineJason.Data;

/// <summary>
/// An enumeration of all possible kinds of a target selector.
/// </summary>
public enum EntitySelectorKind
{
    /// <summary>
    /// Selects all online players.
    /// </summary>
    AllPlayers,
    /// <summary>
    /// Selects the nearest player. If the executor is a player, then the executor would be selected
    /// unless arguments excludes the executor.
    /// </summary>
    NearestPlayer,
    /// <summary>
    /// Selects a random player from all online players.
    /// </summary>
    RandomPlayer,
    /// <summary>
    /// Selects all entities that are alive.
    /// </summary>
    AllEntities,
    /// <summary>
    /// Selects the executor.
    /// </summary>
    Executor,
    /// <summary>
    /// Selects the nearest entity (a shorthand of <c>@e[sort=nearest,limit=1]</c>).
    /// </summary>
    /// <remarks>
    /// This selector kind is only supported from Java Edition 1.21.
    /// </remarks>
    NearestEntity
}