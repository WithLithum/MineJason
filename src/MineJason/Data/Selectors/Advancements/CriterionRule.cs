// Copyright (c) WithLithum & contributors 2023-2024. All rights reserved.
// Licensed under the GNU Lesser General Public License, either version 3 or
// (at your opinion) any later version.

namespace MineJason.Data.Selectors.Advancements;

using MineJason.Utilities;

/// <summary>
/// Represents a single criterion rule.
/// </summary>
public readonly struct CriterionRule : IEquatable<CriterionRule>
{
    /// <summary>
    /// Initialise a new instance of the <see cref="CriterionRule"/> structure.
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
        result = default;
        var split = from.Split('=');

        if (split.Length != 2)
        {
            // Fail: invalid
            return false;
        }

        if (!SpecificValueUtil.TryParseLowerBoolean(split[1], out var value))
        {
            // Fail: value not boolean
            return false;
        }

        result = new CriterionRule(split[0], value);
        return true;
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