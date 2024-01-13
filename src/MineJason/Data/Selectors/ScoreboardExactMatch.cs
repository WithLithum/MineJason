namespace MineJason.Data.Selectors;

using System.Globalization;

/// <summary>
/// Represents an exact match scoreboard value.
/// </summary>
/// <param name="objective">The objective.</param>
/// <param name="value">The value.</param>
public struct ScoreboardExactMatch(string objective, int value) : IScoreboardRange, IEquatable<ScoreboardExactMatch>
{
    /// <summary>
    /// Gets or sets the value of this instance.
    /// </summary>
    public int Value { get; set; } = value;

    /// <inheritdoc />
    public bool Equals(ScoreboardExactMatch other)
    {
        return Value == other.Value;
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

    public string Objective { get; } = objective;

    /// <inheritdoc />
    public string GetString()
    {
        return Value.ToString(CultureInfo.InvariantCulture);
    }

    public static bool operator ==(ScoreboardExactMatch left, ScoreboardExactMatch right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(ScoreboardExactMatch left, ScoreboardExactMatch right)
    {
        return !(left == right);
    }
}