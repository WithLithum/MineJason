// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: LGPL-3.0-or-later

namespace MineJason.Serialization.TextJson;

using System.Text.Json.Serialization;
using JetBrains.Annotations;
using MineJason.Components;
using MineJason.Data;
using MineJason.Data.Coordinates;
using MineJason.Data.Loot;
using MineJason.Data.Loot.Numbers;
using MineJason.Data.Nbt;
using MineJason.Data.Selectors;
using MineJason.Text;
using MineJason.Text.Colors;

/// <summary>
/// Provides serialization context for the serializable types.
/// </summary>
[JsonSerializable(typeof(ChatComponent))]
[JsonSerializable(typeof(TextChatComponent))]
[JsonSerializable(typeof(TranslatableChatComponent))]
[JsonSerializable(typeof(KeybindChatComponent))]
[JsonSerializable(typeof(BaseNbtChatComponent))]
[JsonSerializable(typeof(BlockNbtChatComponent))]
[JsonSerializable(typeof(EntityNbtChatComponent))]
[JsonSerializable(typeof(StorageNbtChatComponent))]
[JsonSerializable(typeof(ScoreboardChatComponent))]
[JsonSerializable(typeof(EntityChatComponent))]
[JsonSerializable(typeof(ObjectTextComponent))]
[JsonSerializable(typeof(BlockPosition))]
[JsonSerializable(typeof(IChatColor))]
[JsonSerializable(typeof(TextColor))]
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