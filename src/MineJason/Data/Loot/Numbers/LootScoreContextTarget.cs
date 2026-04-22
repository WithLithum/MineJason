// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: LGPL-3.0-or-later

namespace MineJason.Data.Loot.Numbers;

using System.Text.Json.Serialization;

/// <summary>
/// Specifies a target from loot context.
/// </summary>
[Obsolete("Loot score providers are no longer supported in the Client module and is subject to removal.")]
public readonly struct LootScoreContextTarget : ILootScoreTarget, IEquatable<LootScoreContextTarget>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="LootScoreContextTarget"/> structure.
    /// </summary>
    /// <param name="target">The target.</param>
    [JsonConstructor]
    public LootScoreContextTarget(LootContextTarget target)
    {
        Target = target;
    }

    /// <summary>
    /// Gets the target to acquire from the loot context.
    /// </summary>
    [JsonPropertyName("target")]
    public LootContextTarget Target { get; }

    /// <inheritdoc />
    public bool Equals(LootScoreContextTarget other)
    {
        return Target == other.Target;
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        return obj is LootScoreContextTarget other && Equals(other);
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        return (int)Target;
    }

    /// <summary>
    /// Determines whether the instance to the <paramref name="left"/> is equivalent to the instance to the <paramref name="right"/> hand side.
    /// </summary>
    /// <param name="left">The instance to the left.</param>
    /// <param name="right">The instance to the right.</param>
    /// <returns><see langword="true"/> if the instance to the left is equivalent to the instance to the <paramref name="right"/> hand side; otherwise, <see langword="false"/>.</returns>
    public static bool operator ==(LootScoreContextTarget left, LootScoreContextTarget right)
    {
        return left.Equals(right);
    }

    /// <summary>
    /// Determines whether the instance to the <paramref name="left"/> is different from the instance to the <paramref name="right"/> hand side.
    /// </summary>
    /// <param name="left">The instance to the left.</param>
    /// <param name="right">The instance to the right.</param>
    /// <returns><see langword="true"/> if the instance to the left is different from the instance to the <paramref name="right"/> hand side; otherwise, <see langword="false"/>.</returns>
    public static bool operator !=(LootScoreContextTarget left, LootScoreContextTarget right)
    {
        return !(left == right);
    }
}