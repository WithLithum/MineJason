// Copyright (c) WithLithum & contributors 2023-2024. All rights reserved.
// Licensed under the GNU Lesser General Public License, either version 3 or
// (at your opinion) any later version.

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