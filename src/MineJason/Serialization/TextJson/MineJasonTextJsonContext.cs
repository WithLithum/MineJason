// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: LGPL-3.0-or-later

using System.Text.Json.Serialization;
using JetBrains.Annotations;
using MineJason.Data;
using MineJason.Data.Coordinates;
using MineJason.Data.Loot;
using MineJason.Data.Loot.Numbers;
using MineJason.Data.Nbt;
using MineJason.Data.Selectors;
using MineJason.Text;
using MineJason.Text.Colors;

namespace MineJason.Serialization.TextJson;

/// <summary>
/// Provides serialization context for the serializable types.
/// </summary>
[JsonSerializable(typeof(TextComponent))]
[JsonSerializable(typeof(LiteralTextComponent))]
[JsonSerializable(typeof(TranslateTextComponent))]
[JsonSerializable(typeof(KeybindTextComponent))]
[JsonSerializable(typeof(NbtTextComponent))]
[JsonSerializable(typeof(BlockNbtTextComponent))]
[JsonSerializable(typeof(EntityNbtTextComponent))]
[JsonSerializable(typeof(StorageNbtTextComponent))]
[JsonSerializable(typeof(ScoreTextComponent))]
[JsonSerializable(typeof(EntityTextComponent))]
[JsonSerializable(typeof(ObjectTextComponent))]
[JsonSerializable(typeof(BlockPosition))]
[JsonSerializable(typeof(ITextColor))]
[JsonSerializable(typeof(RgbTextColor))]
[JsonSerializable(typeof(NamedTextColor))]
[JsonSerializable(typeof(NbtDataSource))]
[JsonSerializable(typeof(ResourceLocation))]
[JsonSerializable(typeof(EntitySelector))]
[JsonSerializable(typeof(string))]
[JsonSerializable(typeof(bool))]
[JsonSerializable(typeof(int))]
[JsonSerializable(typeof(float))]
[JsonSerializable(typeof(double))]
[JsonSerializable(typeof(IEntitySelector))]
[JsonSerializable(typeof(EntityGuidSelector))]
[JsonSerializable(typeof(EntitySelectorKind))]
[JsonSerializable(typeof(TagSelector))]
[JsonSerializable(typeof(ILootNumberProvider))]
[JsonSerializable(typeof(LootUniformNumberProvider))]
[JsonSerializable(typeof(LootExactNumberProvider))]
[JsonSerializable(typeof(LootBinomialNumberProvider))]
[JsonSerializable(typeof(LootScoreNumberProvider))]
[JsonSerializable(typeof(ILootScoreTarget))]
[JsonSerializable(typeof(LootScoreContextTarget))]
[JsonSerializable(typeof(LootScoreFixedTarget))]
[JsonSerializable(typeof(LootContextTarget))]
[JsonSerializable(typeof(LootStorageNumberProvider))]
[JsonSerializable(typeof(INbtDataProvider))]
[JsonSerializable(typeof(RawNbtDataProvider))]
[PublicAPI]
public partial class MineJasonTextJsonContext : JsonSerializerContext
{
}