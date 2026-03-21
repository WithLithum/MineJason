// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: LGPL-3.0-or-later

namespace MineJason.Data.Loot.Numbers;

using System.Text.Json.Serialization;

/// <summary>
/// Provides an exact value.
/// </summary>
public readonly record struct LootExactNumberProvider : ILootNumberProvider
{
    /// <summary>
    /// The registry identifier of this provider.
    /// </summary>
    public static readonly ResourceLocation Id = new("minecraft", "constant");

    /// <summary>
    /// Initializes a new instance of the <see cref="LootExactNumberProvider"/> struct.
    /// </summary>
    /// <param name="value">The exact value.</param>
    [JsonConstructor]
    public LootExactNumberProvider(float value)
    {
        Value = value;
    }

    /// <summary>
    /// Gets the value of this instance.
    /// </summary>
    [JsonPropertyName("value")]
    public float Value { get; }

    // This is rather a hacky way to force STJ to output a "type" property
    // we can't use polymorphic types as we require it to be parsable as resource location

    /// <inheritdoc />
    [JsonInclude]
    [JsonPropertyOrder(-1)]
    [JsonPropertyName("type")]
    public ResourceLocation Type => Id;
}