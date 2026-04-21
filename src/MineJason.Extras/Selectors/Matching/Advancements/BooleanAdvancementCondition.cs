// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: LGPL-3.0-or-later

namespace MineJason.Extras.Selectors.Matching.Advancements;

/// <summary>
/// Represents an advancement condition that determines whether the player owns the advancement or
/// did not own the advancement.
/// </summary>
public readonly struct BooleanAdvancementCondition : IAdvancementCondition, IEquatable<BooleanAdvancementCondition>
{
    /// <summary>
    /// Initialises a new instance of the <see cref="BooleanAdvancementCondition"/> structure.
    /// </summary>
    /// <param name="value">The value.</param>
    public BooleanAdvancementCondition(bool value)
    {
        Value = value;
    }

    /// <summary>
    /// Gets or sets a value indicating the condition value.
    /// </summary>
    public bool Value { get; }

    /// <summary>
    /// Returns the string representation of this instance.
    /// </summary>
    /// <returns>The string representation.</returns>
    public override string ToString()
    {
        return Value ? "true" : "false";
    }

    /// <summary>
    /// Gets the hash code of this instance.
    /// </summary>
    /// <returns>The hash code.</returns>
    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }

    /// <inheritdoc />
    public bool Equals(BooleanAdvancementCondition other)
    {
        return Value == other.Value;
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        return obj is BooleanAdvancementCondition other && Equals(other);
    }

    /// <summary>
    /// Returns a new instance of <see cref="BooleanAdvancementCondition"/> with its value negated from the specified instance.
    /// </summary>
    /// <param name="condition">The condition to negate.</param>
    /// <returns>The negated instance.</returns>
    public static BooleanAdvancementCondition operator !(BooleanAdvancementCondition condition)
    {
        return new BooleanAdvancementCondition(!condition.Value);
    }

    /// <summary>
    /// Determines whether the instance to the left is equivalent to the instance to the right. 
    /// </summary>
    /// <param name="left">The instance to the left.</param>
    /// <param name="right">The instance to the right.</param>
    /// <returns><see langword="true"/> if the instances are equivalent; otherwise, <see langword="false"/>.</returns>
    public static bool operator ==(BooleanAdvancementCondition left, BooleanAdvancementCondition right)
    {
        return left.Equals(right);
    }

    /// <summary>
    /// Determines whether the instance to the left is different to the instance to the right. 
    /// </summary>
    /// <param name="left">The instance to the left.</param>
    /// <param name="right">The instance to the right.</param>
    /// <returns><see langword="true"/> if the instances are different; otherwise, <see langword="false"/>.</returns>
    public static bool operator !=(BooleanAdvancementCondition left, BooleanAdvancementCondition right)
    {
        return !(left == right);
    }
}