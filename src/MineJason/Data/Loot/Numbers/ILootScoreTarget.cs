// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: LGPL-3.0-or-later

namespace MineJason.Data.Loot.Numbers;

using System.Text.Json.Serialization;

/// <summary>
/// Defines a target for <see cref="LootScoreNumberProvider"/>.
/// </summary>
[JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
[JsonDerivedType(typeof(LootScoreFixedTarget), "fixed")]
[JsonDerivedType(typeof(LootScoreContextTarget), "context")]
public interface ILootScoreTarget
{
}