// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: LGPL-3.0-or-later

namespace MineJason.Data.Loot.Numbers;

using System.Text.Json.Serialization;
using MineJason.Serialization.TextJson;

/// <summary>
/// Defines a number provider.
/// </summary>
[JsonConverter(typeof(LootNumberProviderConverter))]
public interface ILootNumberProvider
{
    /// <summary>
    /// Gets the registered type of this instance.
    /// </summary>
    [JsonInclude]
    [JsonPropertyOrder(-1)]
    [JsonPropertyName("type")]
    public ResourceLocation Type { get; }
}