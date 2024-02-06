// Copyright (c) WithLithum & contributors 2023-2024. All rights reserved.
// Licensed under the GNU Lesser General Public License, either version 3 or
// (at your opinion) any later version.

namespace MineJason.Data.Selectors.Advancements;

/// <summary>
/// Represents a single pair of advancement condition in a target selector.
/// </summary>
public readonly struct SelectorAdvancementMatch : IEquatable<SelectorAdvancementMatch>
{
    /// <summary>
    /// Initialises a new instance of the <see cref="SelectorAdvancementMatch"/> structure.
    /// </summary>
    /// <param name="advancement">The advancement to check.</param>
    /// <param name="condition">The condition to check.</param>
    public SelectorAdvancementMatch(ResourceLocation advancement, IAdvancementCondition condition)
    {
        Advancement = advancement;
        Condition = condition;
    }
    
    /// <summary>
    /// Gets the advancement to check.
    /// </summary>
    public ResourceLocation Advancement { get; }
    
    /// <summary>
    /// Gets the condition to check.
    /// </summary>
    public IAdvancementCondition Condition { get; }

    /// <inheritdoc />
    public bool Equals(SelectorAdvancementMatch other)
    {
        return Advancement.Equals(other.Advancement) && Condition.Equals(other.Condition);
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        return obj is SelectorAdvancementMatch other && Equals(other);
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        return HashCode.Combine(Advancement, Condition);
    }

    /// <summary>
    /// Returns the string representation of this instance.
    /// </summary>
    /// <returns>The string representation.</returns>
    public override string ToString()
    {
        return $"{Advancement}={Condition}";
    }

    /// <summary>
    /// Determines whether the instance to the left is equivalent to the instance to the right. 
    /// </summary>
    /// <param name="left">The instance to the left.</param>
    /// <param name="right">The instance to the right.</param>
    /// <returns><see langword="true"/> if the instances are equivalent; otherwise, <see langword="false"/>.</returns>
    public static bool operator ==(SelectorAdvancementMatch left, SelectorAdvancementMatch right)
    {
        return left.Equals(right);
    }

    /// <summary>
    /// Determines whether the instance to the left is different to the instance to the right. 
    /// </summary>
    /// <param name="left">The instance to the left.</param>
    /// <param name="right">The instance to the right.</param>
    /// <returns><see langword="true"/> if the instances are different; otherwise, <see langword="false"/>.</returns>
    public static bool operator !=(SelectorAdvancementMatch left, SelectorAdvancementMatch right)
    {
        return !(left == right);
    }
}