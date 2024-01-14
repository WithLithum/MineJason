// Copyright (c) WithLithum & contributors 2023-2024. All rights reserved.
// Licensed under the GNU Lesser General Public License, either version 3 or
// (at your opinion) any later version.

namespace MineJason.Data.Selectors;

using JetBrains.Annotations;

/// <summary>
/// Represents a scoreboard target selector condition.
/// </summary>
[PublicAPI]
public struct ScoreboardRangeMatch : IEquatable<ScoreboardRangeMatch>, IScoreboardRange
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ScoreboardRangeMatch"/> class.
    /// </summary>
    /// <param name="objective">The objective.</param>
    /// <param name="range">The range.</param>
    public ScoreboardRangeMatch(string objective, IntegralRange range)
    {
        Objective = objective;
        Range = range;
    }
    
    /// <summary>
    /// Gets or sets the objective to check for.
    /// </summary>
    public string Objective { get; set; }
    
    /// <summary>
    /// Gets or sets the range to match.
    /// </summary>
    public IntegralRange Range { get; set; }

    /// <inheritdoc />
    public bool Equals(ScoreboardRangeMatch other)
    {
#if DEBUG
        Console.WriteLine("ScoreboardRangeMatch comparison with instance: {0}, {1}", other.Objective, other.Range);
#endif
        
        return Objective == other.Objective && Range.Equals(other.Range);
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        #if DEBUG
        Console.WriteLine("ScoreboardRangeMatch comparison with object: {0}", obj);
        #endif
        
        return obj is ScoreboardRangeMatch other && Equals(other);
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        return HashCode.Combine(Objective, Range);
    }

    /// <inheritdoc />
    public string GetString()
    {
        return Range.ToString();
    }

    /// <summary>
    /// Determines whether the instance to the <paramref name="left"/> is equivalent to the instance
    /// to the <paramref name="right"/>.
    /// </summary>
    /// <param name="left">The left instance.</param>
    /// <param name="right">The right instance.</param>
    /// <returns><see langword="true"/> if the instance to the <paramref name="left"/> is equivalent to the instance
    /// to the <paramref name="right"/>; otherwise, <see langword="false"/>.</returns>
    public static bool operator ==(ScoreboardRangeMatch left, ScoreboardRangeMatch right)
    {
        return left.Equals(right);
    }

    /// <summary>
    /// Determines whether the instance to the <paramref name="left"/> does not equals to the instance
    /// to the <paramref name="right"/>.
    /// </summary>
    /// <param name="left">The left instance.</param>
    /// <param name="right">The right instance.</param>
    /// <returns><see langword="true"/> if the instance to the <paramref name="left"/> is not equivalent to the instance
    /// to the <paramref name="right"/>; otherwise, <see langword="false"/>.</returns>
    public static bool operator !=(ScoreboardRangeMatch left, ScoreboardRangeMatch right)
    {
        return !(left == right);
    }

    /// <inheritdoc />
    public override string ToString()
    {
        return $"ScoreboardRangeMatch({Objective}={Range})";
    }
}