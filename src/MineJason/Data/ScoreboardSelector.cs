namespace MineJason.Data;

/// <summary>
/// Represents a scoreboard target selector condition.
/// </summary>
public struct ScoreboardSelector : IEquatable<ScoreboardSelector>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ScoreboardSelector"/> class.
    /// </summary>
    /// <param name="objective">The objective.</param>
    /// <param name="range">The range.</param>
    public ScoreboardSelector(string objective, ScoreboardRange range)
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
    public ScoreboardRange Range { get; set; }

    /// <inheritdoc />
    public bool Equals(ScoreboardSelector other)
    {
        return Objective == other.Objective && Range.Equals(other.Range);
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        return obj is ScoreboardSelector other && Equals(other);
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        return HashCode.Combine(Objective, Range);
    }

    /// <summary>
    /// Determines whether the instance to the <paramref name="left"/> is equivalent to the instance
    /// to the <paramref name="right"/>.
    /// </summary>
    /// <param name="left">The left instance.</param>
    /// <param name="right">The right instance.</param>
    /// <returns><see langword="true"/> if the instance to the <paramref name="left"/> is equivalent to the instance
    /// to the <paramref name="right"/>; otherwise, <see langword="false"/>.</returns>
    public static bool operator ==(ScoreboardSelector left, ScoreboardSelector right)
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
    public static bool operator !=(ScoreboardSelector left, ScoreboardSelector right)
    {
        return !(left == right);
    }
}