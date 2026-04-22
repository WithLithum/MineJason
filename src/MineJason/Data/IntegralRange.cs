// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

namespace MineJason.Data;

using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Text;
using JetBrains.Annotations;

/// <summary>
/// Represents a range in integral condition.
/// </summary>
[PublicAPI]
public readonly struct IntegralRange : IEquatable<IntegralRange>
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

    /// <summary>
    /// Gets the exact match.
    /// </summary>
    public int? Exact { get; }

    /// <summary>
    /// Gets or sets the minimum range.
    /// </summary>
    public int? Min { get; }

    /// <summary>
    /// Gets or sets the maximum range.
    /// </summary>
    public int? Max { get; }

    /// <inheritdoc />
    public bool Equals(IntegralRange other)
    {
        return Min.Equals(other.Min) && Max.Equals(other.Max) && Exact.Equals(other.Exact);
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        return obj is IntegralRange other && Equals(other);
    }

    /// <summary>
    /// Converts the specified string representation of a number range into its
    /// <see cref="IntegralRange"/> equivalent.
    /// </summary>
    /// <param name="s">The string to parse.</param>
    /// <returns>
    /// A new instance of <see cref="IntegralRange"/> equivalent to the number range contained in
    /// <paramref name="s"/>.
    /// </returns>
    /// <exception cref="FormatException">
    /// The number range contained in <paramref name="s"/> is invalid.
    /// </exception>
    public static IntegralRange Parse(in ReadOnlySpan<char> s)
    {
        if (TryParse(s, out var result))
        {
            return result;
        }

        throw new FormatException("The specified number range is not in a correct format.");
    }

    /// <summary>
    /// Converts the specified string representation of a number range into its
    /// <see cref="IntegralRange"/> equivalent.
    /// </summary>
    /// <param name="s">The string to parse.</param>
    /// <param name="result">
    /// When this method returns, contains the result of successfully parsing <paramref name="s"/>
    /// or an undefined value on failure.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if <paramref name="s"/> was successfully parsed; otherwise,
    /// <see langword="false"/>.
    /// </returns>
    public static bool TryParse(in ReadOnlySpan<char> s,
        out IntegralRange result)
    {
        if (s.StartsWith("..", StringComparison.Ordinal)) // like '..123'
        {
            if (!int.TryParse(s[2..], CultureInfo.InvariantCulture, out var max))
            {
                result = default;
                return false;
            }

            result = new IntegralRange(null, max);
        }
        else if (s.EndsWith("..", StringComparison.Ordinal)) // like '123..'
        {
            if (!int.TryParse(s[..^2], CultureInfo.InvariantCulture, out var min))
            {
                result = default;
                return false;
            }

            result = new IntegralRange(min, null);
        }
        else if (s.Contains("..", StringComparison.Ordinal)) // like '123..321'
        {
            Span<Range> valueRanges = stackalloc Range[3];
            if (s.Split(valueRanges, "..", StringSplitOptions.TrimEntries) != 2
                || !int.TryParse(s[valueRanges[0]], CultureInfo.InvariantCulture, out var min)
                || !int.TryParse(s[valueRanges[1]], CultureInfo.InvariantCulture, out var max))
            {
                result = default;
                return false;
            }

            result = new IntegralRange(min, max);
        }
        else
        {
            if (!int.TryParse(s, CultureInfo.InvariantCulture, out var value))
            {
                result = default;
                return false;
            }

            result = new IntegralRange(value);
        }

        return true;
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