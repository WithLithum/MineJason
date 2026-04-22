// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: LGPL-3.0-or-later

namespace MineJason.Extras.Selectors.Matching.Predicates;

/// <summary>
/// Represents a target selector condition where an entity must either pass or fail a predicate
/// check to be selected.
/// </summary>
public readonly struct PredicateCondition : IEquatable<PredicateCondition>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PredicateCondition"/> structure.
    /// </summary>
    /// <param name="predicate">The predicate.</param>
    /// <param name="match">Whether the entity should pass or fail.</param>
    public PredicateCondition(ResourceLocation predicate, bool match)
    {
        Predicate = predicate;
        Match = match;
    }

    /// <summary>
    /// Gets the predicate to match with.
    /// </summary>
    public ResourceLocation Predicate { get; }

    /// <summary>
    /// Gets a value indicating whether the entity should pass or fail the predicate check in order to fulfill
    /// this condition.
    /// </summary>
    public bool Match { get; }

    /// <inheritdoc />
    public bool Equals(PredicateCondition other)
    {
        return Predicate.Equals(other.Predicate) && Match == other.Match;
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        return obj is PredicateCondition other && Equals(other);
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        return HashCode.Combine(Predicate, Match);
    }

    /// <summary>
    /// Returns the string representation of this instance.
    /// </summary>
    /// <returns>The string representation.</returns>
    public override string ToString()
    {
        return Match
            ? Predicate.ToString()
            : $"!{Predicate}";
    }

    /// <summary>
    /// Resolves the string representation as a predicate target selector condition.
    /// </summary>
    /// <param name="from">The string representation.</param>
    /// <param name="result">The parsing result.</param>
    /// <returns><see langword="true"/> if the syntax is correct; otherwise, <see langword="false"/>.</returns>
    public static bool TryParse(string from, out PredicateCondition result)
    {
        // TODO make a span-based version
        if (string.IsNullOrWhiteSpace(from))
        {
            result = default;
            return false;
        }

        result = default;
        var match = !from.StartsWith('!');
        if (!ResourceLocation.TryParse(match
                ? from
                : from[1..], out var location))
        {
            return false;
        }

        result = new PredicateCondition(location, match);
        return true;
    }

    /// <summary>
    /// Returns a new instance of the <see cref="PredicateCondition"/> with the same predicate but with
    /// its <see cref="Match"/> value negated.
    /// </summary>
    /// <param name="condition">The condition to negate.</param>
    /// <returns>The negated condition.</returns>
    public static PredicateCondition operator !(PredicateCondition condition)
    {
        return new PredicateCondition(condition.Predicate, !condition.Match);
    }

    /// <summary>
    /// Determines whether the instance to the left is equivalent to the instance to the right. 
    /// </summary>
    /// <param name="left">The instance to the left.</param>
    /// <param name="right">The instance to the right.</param>
    /// <returns><see langword="true"/> if the instances are equivalent; otherwise, <see langword="false"/>.</returns>
    public static bool operator ==(PredicateCondition left, PredicateCondition right)
    {
        return left.Equals(right);
    }

    /// <summary>
    /// Determines whether the instance to the left is different to the instance to the right. 
    /// </summary>
    /// <param name="left">The instance to the left.</param>
    /// <param name="right">The instance to the right.</param>
    /// <returns><see langword="true"/> if the instances are different; otherwise, <see langword="false"/>.</returns>
    public static bool operator !=(PredicateCondition left, PredicateCondition right)
    {
        return !(left == right);
    }
}