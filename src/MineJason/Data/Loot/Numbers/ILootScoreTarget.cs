// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

namespace MineJason.Data.Loot.Numbers;

using System.Text.Json.Serialization;

/// <summary>
/// Defines a target for <see cref="LootScoreNumberProvider"/>.
/// </summary>
[Obsolete("Loot score providers are no longer supported in the Client module and is subject to removal.")]
[JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
[JsonDerivedType(typeof(LootScoreFixedTarget), "fixed")]
[JsonDerivedType(typeof(LootScoreContextTarget), "context")]
public interface ILootScoreTarget
{
}