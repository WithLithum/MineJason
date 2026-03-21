// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: LGPL-3.0-or-later

namespace MineJason.Data.Selectors.Advancements;

using MineJason.Utilities;

/// <summary>
/// Represents a single criterion rule.
/// </summary>
public readonly struct CriterionRule : IEquatable<CriterionRule>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CriterionRule"/> structure.
    /// </summary>
    /// <param name="criterion">The criterion.</param>
    /// <param name="value">The value.</param>
    public CriterionRule(string criterion, bool value)
    {
        Criterion = criterion;
        Value = value;
    }

    /// <summary>
    /// Gets or sets the name of the criterion to check for this condition.
    /// </summary>
    public string Criterion { get; }

    /// <summary>
    /// Gets or sets the value of this condition.
    /// </summary>
    public bool Value { get; }

    /// <inheritdoc />
    public bool Equals(CriterionRule other)
    {
        return Criterion == other.Criterion && Value == other.Value;
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        return obj is CriterionRule other && Equals(other);
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        return HashCode.Combine(Criterion, Value);
    }

    /// <summary>
    /// Returns the string representation of this instance.
    /// </summary>
    /// <returns>The string representation.</returns>
    public override string ToString()
    {
        return $"{Criterion}={SpecificValueUtil.ToLowerBooleanString(Value)}";
    }

    /// <summary>
    /// Attempts to parse the specified string to a criterion advancement rule.
    /// </summary>
    /// <param name="from">The string to parse.</param>
    /// <param name="result">The parse result.</param>
    /// <returns><see langword="true"/> if the syntax is correct; otherwise, <see langword="false"/>.</returns>
    public static bool TryParse(string from, out CriterionRule result)
    {
        return TryParse(from.AsSpan(), out result);
    }

    /// <summary>
    /// Attempts to parse the specified string to a criterion advancement rule.
    /// </summary>
    /// <param name="s">The character sequence to parse.</param>
    /// <param name="result">The parse result.</param>
    /// <returns><see langword="true"/> if the syntax is correct; otherwise, <see langword="false"/>.</returns>
    public static bool TryParse(ReadOnlySpan<char> s, out CriterionRule result)
    {
        result = default;
        Span<Range> split = stackalloc Range[3];

        if (s.Split(split, '=') != 2)
        {
            // Fail: invalid
            return false;
        }

        if (!SpecificValueUtil.TryParseLowerBoolean(s[split[1]], out var value))
        {
            // Fail: value not boolean
            return false;
        }

        result = new CriterionRule(s[split[0]].ToString(), value);
        return true;
    }

    /// <summary>
    /// Returns a new instance of <see cref="CriterionRule"/> with the same criterion as of the specified
    /// <paramref name="rule"/> but with the value negated.
    /// </summary>
    /// <param name="rule">The rule.</param>
    /// <returns>The created instance, with the same criterion and with the value negated.</returns>
    public static CriterionRule operator !(CriterionRule rule)
    {
        return new CriterionRule(rule.Criterion, !rule.Value);
    }

    /// <summary>
    /// Determines whether the instance to the left is equivalent to the instance to the right. 
    /// </summary>
    /// <param name="left">The instance to the left.</param>
    /// <param name="right">The instance to the right.</param>
    /// <returns><see langword="true"/> if the instances are equivalent; otherwise, <see langword="false"/>.</returns>
    public static bool operator ==(CriterionRule left, CriterionRule right)
    {
        return left.Equals(right);
    }

    /// <summary>
    /// Determines whether the instance to the left is different to the instance to the right. 
    /// </summary>
    /// <param name="left">The instance to the left.</param>
    /// <param name="right">The instance to the right.</param>
    /// <returns><see langword="true"/> if the instances are different; otherwise, <see langword="false"/>.</returns>
    public static bool operator !=(CriterionRule left, CriterionRule right)
    {
        return !(left == right);
    }
}