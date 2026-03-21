// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: LGPL-3.0-or-later

namespace MineJason.Data.Selectors;

/// <summary>
/// Represents a scoreboard selector value requirement.
/// </summary>
public interface IScoreboardRange
{
    /// <summary>
    /// Gets the objective.
    /// </summary>
    string Objective { get; }
    
    /// <summary>
    /// Converts this instance to its string representation.
    /// </summary>
    /// <returns>The string.</returns>
    string GetString();
}