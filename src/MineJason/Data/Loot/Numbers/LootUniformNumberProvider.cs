// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: LGPL-3.0-or-later

namespace MineJason.Data.Loot.Numbers;

using System.Text.Json.Serialization;

/// <summary>
/// Provides a random value from an inclusive uniform distribution between two values.
/// </summary>
public readonly record struct LootUniformNumberProvider : ILootNumberProvider
{
    /// <summary>
    /// The resource location of a uniform provider.
    /// </summary>
    public static readonly ResourceLocation Id = new ResourceLocation("minecraft", "uniform");
    
    /// <summary>
    /// Initializes a new instance of the <see cref="LootUniformNumberProvider"/> structure.
    /// </summary>
    /// <param name="min">The minimum value (inclusive).</param>
    /// <param name="max">The maximum value (inclusive).</param>
    [JsonConstructor]
    public LootUniformNumberProvider(float min, float max)
    {
        Min = min;
        Max = max;
    }
    
    /// <summary>
    /// Gets the minimum value (inclusive) of this instance.
    /// </summary>
    [JsonPropertyName("min")]
    public float Min { get; }
    
    /// <summary>
    /// Gets the maximum value (inclusive) of this instance.
    /// </summary>
    [JsonPropertyName("max")]
    public float Max { get; }
    
    // This is rather a hacky way to force STJ to output a "type" property
    // we can't use polymorphic types as we require it to be parsable as resource location
    
    /// <inheritdoc />
    [JsonInclude]
    [JsonPropertyOrder(-1)]
    [JsonPropertyName("type")]
    public ResourceLocation Type => Id;
}