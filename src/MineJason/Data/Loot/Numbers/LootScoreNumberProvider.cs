// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: LGPL-3.0-or-later

namespace MineJason.Data.Loot.Numbers;

using System.Text.Json.Serialization;

/// <summary>
/// Provides a number fetched from the score of an entity.
/// </summary>
public readonly struct LootScoreNumberProvider : ILootNumberProvider,
    IEquatable<LootScoreNumberProvider>
{
    /// <summary>
    /// The resource location of a score provider.
    /// </summary>
    public static readonly ResourceLocation Id = new("minecraft", "score");
    
    /// <summary>
    /// Initializes a new instance of the <see cref="LootScoreNumberProvider"/> structure.
    /// </summary>
    /// <param name="target">The target entity to fetch score from.</param>
    /// <param name="score">The objective to fetch score from.</param>
    /// <param name="scale">The scale of the score. Optional.</param>
    [JsonConstructor]
    public LootScoreNumberProvider(ILootScoreTarget target, string score, float? scale = null)
    {
        Target = target;
        Score = score;
        Scale = scale;
    }
    
    /// <summary>
    /// Gets the target entity to fetch score from.
    /// </summary>
    [JsonPropertyName("target")]
    public ILootScoreTarget Target { get; }
    
    /// <summary>
    /// Gets the objective to fetch score from.
    /// </summary>
    [JsonPropertyName("score")]
    public string Score { get; }
    
    /// <summary>
    /// Gets the scale that the score will be multiplied with before returning.
    /// </summary>
    [JsonPropertyName("scale")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public float? Scale { get; }

    /// <inheritdoc />
    public bool Equals(LootScoreNumberProvider other)
    {
        return Target.Equals(other.Target) && Score == other.Score && Nullable.Equals(Scale, other.Scale);
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        return obj is LootScoreNumberProvider other && Equals(other);
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        return HashCode.Combine(Target, Score, Scale);
    }

    /// <summary>
    /// Determines whether the instance to the <paramref name="left"/> is equivalent to the instance to the <paramref name="right"/> hand side.
    /// </summary>
    /// <param name="left">The instance to the left.</param>
    /// <param name="right">The instance to the right.</param>
    /// <returns><see langword="true"/> if the instance to the left is equivalent to the instance to the <paramref name="right"/> hand side; otherwise, <see langword="false"/>.</returns>
    public static bool operator ==(LootScoreNumberProvider left, LootScoreNumberProvider right)
    {
        return left.Equals(right);
    }

    /// <summary>
    /// Determines whether the instance to the <paramref name="left"/> is different from the instance to the <paramref name="right"/> hand side.
    /// </summary>
    /// <param name="left">The instance to the left.</param>
    /// <param name="right">The instance to the right.</param>
    /// <returns><see langword="true"/> if the instance to the left is different from the instance to the <paramref name="right"/> hand side; otherwise, <see langword="false"/>.</returns>
    public static bool operator !=(LootScoreNumberProvider left, LootScoreNumberProvider right)
    {
        return !(left == right);
    }
    
    // This is rather a hacky way to force STJ to output a "type" property
    // we can't use polymorphic types as we require it to be parsable as resource location
    
    /// <inheritdoc />
    [JsonInclude]
    [JsonPropertyOrder(-1)]
    [JsonPropertyName("type")]
    public ResourceLocation Type => Id;
}