// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: LGPL-3.0-or-later

using System.Globalization;

namespace MineJason.Extras.Selectors.Matching;

/// <summary>
/// Represents an exact match scoreboard value.
/// </summary>
/// <param name="objective">The objective.</param>
/// <param name="value">The value.</param>
public readonly struct ScoreboardExactMatch(string objective, int value) : IScoreboardRange, IEquatable<ScoreboardExactMatch>
{
    /// <summary>
    /// Gets or sets the value of this instance.
    /// </summary>
    public int Value { get; } = value;

    /// <inheritdoc />
    public readonly bool Equals(ScoreboardExactMatch other)
    {
        return Value == other.Value
            && Objective.Equals(other.Objective, StringComparison.Ordinal);
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        return obj is ScoreboardExactMatch other && Equals(other);
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        return Value;
    }

    /// <summary>
    /// Gets or sets the objective to match.
    /// </summary>
    public string Objective { get; } = objective;

    /// <inheritdoc />
    public readonly string GetString()
    {
        return Value.ToString(CultureInfo.InvariantCulture);
    }

    /// <summary>
    /// Determines whether the instance to the left is equivalent to the instance to the right. 
    /// </summary>
    /// <param name="left">The instance to the left.</param>
    /// <param name="right">The instance to the right.</param>
    /// <returns><see langword="true"/> if the instances are equivalent; otherwise, <see langword="false"/>.</returns>
    public static bool operator ==(ScoreboardExactMatch left, ScoreboardExactMatch right)
    {
        return left.Equals(right);
    }

    /// <summary>
    /// Determines whether the instance to the left is different to the instance to the right. 
    /// </summary>
    /// <param name="left">The instance to the left.</param>
    /// <param name="right">The instance to the right.</param>
    /// <returns><see langword="true"/> if the instances are different; otherwise, <see langword="false"/>.</returns>
    public static bool operator !=(ScoreboardExactMatch left, ScoreboardExactMatch right)
    {
        return !(left == right);
    }
}