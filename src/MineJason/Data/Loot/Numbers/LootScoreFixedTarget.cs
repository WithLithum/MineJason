// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: LGPL-3.0-or-later

namespace MineJason.Data.Loot.Numbers;

using System.Text.Json.Serialization;

/// <summary>
/// Specifies an arbitrary entity as a target.
/// </summary>
[Obsolete("Loot score providers are no longer supported in the Client module and is subject to removal.")]
public readonly record struct LootScoreFixedTarget : ILootScoreTarget
{
    /// <summary>
    /// Initializes a new instance of the <see cref="LootScoreFixedTarget"/> class.
    /// </summary>
    /// <param name="name">The name of the entity.</param>
    [JsonConstructor]
    public LootScoreFixedTarget(string name)
    {
        Name = name;
    }

    /// <summary>
    /// Gets the name of this instance.
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; }
}