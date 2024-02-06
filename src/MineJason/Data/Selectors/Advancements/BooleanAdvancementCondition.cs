// Copyright (c) WithLithum & contributors 2023-2024. All rights reserved.
// Licensed under the GNU Lesser General Public License, either version 3 or
// (at your opinion) any later version.

namespace MineJason.Data.Selectors.Advancements;

/// <summary>
/// Represents an advancement condition that determines whether the player owns the advancement or
/// did not own the advancement.
/// </summary>
public struct BooleanAdvancementCondition : IAdvancementCondition, IEquatable<BooleanAdvancementCondition>
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
    public bool Value { get; set; }

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