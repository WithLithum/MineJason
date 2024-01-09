namespace MineJason.Data;

using System.Text;

/// <summary>
/// Represents a range in scoreboard condition.
/// </summary>
public struct ScoreboardRange : IEquatable<ScoreboardRange>
{
    /// <summary>
    /// Gets or sets the minimum range.
    /// </summary>
    public int? Min { get; set; }
    
    /// <summary>
    /// Gets or sets the maximum range.
    /// </summary>
    public int? Max { get; set; }

    /// <inheritdoc />
    public bool Equals(ScoreboardRange other)
    {
        return Min.Equals(other.Min) && Max.Equals(other.Max);
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        return obj is ScoreboardRange other && Equals(other);
    }

    /// <summary>
    /// Converts this coordinate range to its <see cref="string"/> representation.
    /// </summary>
    /// <returns>If none of <see cref="Min"/>, or <see cref="Max"/> was specified, returns <see cref="string.Empty"/>; otherwise, returns the
    /// range as used by a target selector.</returns>
    public override string ToString()
    {
        var builder = new StringBuilder();

        if (!Min.HasValue && !Max.HasValue)
        {
            return string.Empty;
        }
        
        if (Min.HasValue)
        {
            builder.Append(Min);
        }

        builder.Append("..");

        if (Max.HasValue)
        {
            builder.Append(Max);
        }

        return builder.ToString();
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        return HashCode.Combine(Min, Max);
    }

    /// <summary>
    /// Determines whether the instance to the <paramref name="left"/> is equivalent to the instance
    /// to the <paramref name="right"/>.
    /// </summary>
    /// <param name="left">The left instance.</param>
    /// <param name="right">The right instance.</param>
    /// <returns><see langword="true"/> if the instance to the <paramref name="left"/> is equivalent to the instance
    /// to the <paramref name="right"/>; otherwise, <see langword="false"/>.</returns>
    public static bool operator ==(ScoreboardRange left, ScoreboardRange right)
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
    public static bool operator !=(ScoreboardRange left, ScoreboardRange right)
    {
        return !(left == right);
    }
}