// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: LGPL-3.0-or-later

namespace MineJason.Data.Loot;

using System.Text.Json.Serialization;
using JetBrains.Annotations;
using MineJason.Serialization.TextJson;

/// <summary>
/// An enumeration of all possible loot context targets.
/// </summary>
/// <remarks>
/// <para>
/// The meaning of each
/// </para>
/// </remarks>
[PublicAPI]
[JsonConverter(typeof(JsonLowerSnakeStringEnumConverter<LootContextTarget>))]
public enum LootContextTarget
{
    /// <summary>
    /// <c>this</c> entity.
    /// </summary>
    This,
    /// <summary>
    /// <c>killer</c> entity.
    /// </summary>
    Killer,
    /// <summary>
    /// <c>direct_killer</c> entity.
    /// </summary>
    DirectKiller,
    /// <summary>
    /// <c>killer_player</c> entity.
    /// </summary>
    KillerPlayer
}