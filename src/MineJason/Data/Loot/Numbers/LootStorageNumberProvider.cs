// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

namespace MineJason.Data.Loot.Numbers;

using System.Text.Json.Serialization;

/// <summary>
/// Provides a number in storage.
/// </summary>
[Obsolete("Loot number providers are no longer provided in the Client module.")]
public readonly record struct LootStorageNumberProvider : ILootNumberProvider
{
    /// <summary>
    /// The resource location of a uniform provider.
    /// </summary>
    public static readonly ResourceLocation Id = new("minecraft", "storage");

    /// <summary>
    /// Initializes a new instance of the <see cref="LootStorageNumberProvider"/> structure.
    /// </summary>
    /// <param name="storage">The storage to acquire number from.</param>
    /// <param name="path">The path to the number.</param>
    [JsonConstructor]
    public LootStorageNumberProvider(ResourceLocation storage, string path)
    {
        Storage = storage;
        Path = path;
    }

    /// <summary>
    /// Gets the identifier of the storage to obtain the number from.
    /// </summary>
    [JsonPropertyName("storage")]
    public ResourceLocation Storage { get; }

    /// <summary>
    /// Gets the path to the tag storing the number.
    /// </summary>
    [JsonPropertyName("path")]
    public string Path { get; }

    // This is rather a hacky way to force STJ to output a "type" property
    // we can't use polymorphic types as we require it to be parsable as resource location

    /// <inheritdoc />
    [JsonInclude]
    [JsonPropertyOrder(-1)]
    [JsonPropertyName("type")]
    public ResourceLocation Type => Id;
}