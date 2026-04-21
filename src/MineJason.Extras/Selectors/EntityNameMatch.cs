// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: LGPL-3.0-or-later

namespace MineJason.Extras.Selectors;

/// <summary>
/// Checks whether an entity is or is not of the specified name.
/// </summary>
public readonly struct EntityNameMatch : IEquatable<EntityNameMatch>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="EntityNameMatch"/> class.
    /// </summary>
    /// <param name="name">The name.</param>
    /// <param name="value">The value.</param>
    public EntityNameMatch(string name, bool value)
    {
        Name = name;
        Value = value;
    }

    /// <summary>
    /// Gets or sets the name to check.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Gets or sets the value to check.
    /// </summary>
    public bool Value { get; }

    /// <inheritdoc/>
    public bool Equals(EntityNameMatch other)
    {
        return other.Name.Equals(this.Name, StringComparison.Ordinal)
            && other.Value.Equals(this.Value);
    }

    /// <inheritdoc/>
    public override bool Equals(object? obj)
    {
        return obj is EntityNameMatch match && Equals(match);
    }

    /// <summary>
    /// Creates a new instance of <see cref="EntityNameMatch"/> by negating the value of the
    /// specified match.
    /// </summary>
    /// <param name="match">The match to negate.</param>
    /// <returns>The negated value.</returns>
    public static EntityNameMatch operator !(EntityNameMatch match)
    {
        return new EntityNameMatch(match.Name, false);
    }

    /// <summary>
    /// Wraps the specified string to a positive <see cref="EntityNameMatch"/>.
    /// </summary>
    /// <param name="value">The value.</param>
    public static implicit operator EntityNameMatch(string value)
    {
        return new EntityNameMatch(value, true);
    }

    /// <summary>
    /// Determines whether the instance to the left is equivalent to the instance to the right. 
    /// </summary>
    /// <param name="left">The instance to the left.</param>
    /// <param name="right">The instance to the right.</param>
    /// <returns><see langword="true"/> if the instances are equivalent; otherwise, <see langword="false"/>.</returns>
    public static bool operator ==(EntityNameMatch left, EntityNameMatch right)
    {
        return left.Equals(right);
    }

    /// <summary>
    /// Determines whether the instance to the left is different to the instance to the right. 
    /// </summary>
    /// <param name="left">The instance to the left.</param>
    /// <param name="right">The instance to the right.</param>
    /// <returns><see langword="true"/> if the instances are different; otherwise, <see langword="false"/>.</returns>
    public static bool operator !=(EntityNameMatch left, EntityNameMatch right)
    {
        return !(left == right);
    }

    /// <inheritdoc/>
    public override int GetHashCode()
    {
        return HashCode.Combine(Name, Value);
    }
}
