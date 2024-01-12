namespace MineJason.Data;

using System.Globalization;
using System.Text;
using JetBrains.Annotations;

/// <summary>
/// Represents a range in integral condition.
/// </summary>
[PublicAPI]
public struct IntegralRange : IEquatable<IntegralRange>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="IntegralRange"/> class.
    /// </summary>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    public IntegralRange(int? min, int? max)
    {
        Min = min;
        Max = max;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="IntegralRange"/> class.
    /// </summary>
    /// <param name="exact">The exact value to match.</param>
    public IntegralRange(int exact)
    {
        Exact = exact;
    }

    public int? Exact { get; set; }
    
    /// <summary>
    /// Gets or sets the minimum range.
    /// </summary>
    public int? Min { get; set; }

    /// <summary>
    /// Gets or sets the maximum range.
    /// </summary>
    public int? Max { get; set; }
    
    /// <inheritdoc />
    public bool Equals(IntegralRange other)
    {
#if DEBUG
        Console.WriteLine("IntegralRange match with instance: {0}, {1}, {2}", Min, Exact, Max);
        Console.WriteLine("> other: {0}, {1}, {2}", other.Min, other.Exact, other.Max);
        Console.WriteLine("> {0}, {1}, {2}", Min.Equals(other.Min), Max.Equals(other.Max), Exact.Equals(other.Exact));
#endif
        
        return Min.Equals(other.Min) && Max.Equals(other.Max) && Exact.Equals(other.Exact);
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        return obj is IntegralRange other && Equals(other);
    }

    /// <summary>
    /// Converts this coordinate range to its <see cref="string"/> representation.
    /// </summary>
    /// <returns>If none of <see cref="Min"/>, or <see cref="Max"/> was specified, returns <see cref="string.Empty"/>; otherwise, returns the
    /// range as used by a target selector.</returns>
    public override string ToString()
    {
        if (Exact.HasValue)
        {
            return Exact.Value.ToString(CultureInfo.InvariantCulture);
        }
        
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
        return HashCode.Combine(Min, Max, Exact);
    }

    /// <summary>
    /// Determines whether the instance to the <paramref name="left"/> is equivalent to the instance
    /// to the <paramref name="right"/>.
    /// </summary>
    /// <param name="left">The left instance.</param>
    /// <param name="right">The right instance.</param>
    /// <returns><see langword="true"/> if the instance to the <paramref name="left"/> is equivalent to the instance
    /// to the <paramref name="right"/>; otherwise, <see langword="false"/>.</returns>
    public static bool operator ==(IntegralRange left, IntegralRange right)
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
    public static bool operator !=(IntegralRange left, IntegralRange right)
    {
        return !(left == right);
    }
}