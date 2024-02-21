// Copyright (c) WithLithum & contributors 2023-2024. All rights reserved.
// Licensed under the GNU Lesser General Public License, either version 3 or
// (at your opinion) any later version.

namespace MineJason.Serialization.TextJson;

using System.Text.Json;
using System.Text.Json.Serialization;
using JetBrains.Annotations;
using MineJason.Colors;
using MineJason.Components;
using MineJason.Data;
using MineJason.Data.Coordinates;

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
[JsonSerializable(typeof(AnyBlockPosition))]
[JsonSerializable(typeof(IChatColor))]
[JsonSerializable(typeof(RgbChatColor))]
[JsonSerializable(typeof(KnownColor))]
[JsonSerializable(typeof(NbtDataSource))]
[JsonSerializable(typeof(NbtProvider))]
[JsonSerializable(typeof(ResourceLocation))]
[JsonSerializable(typeof(EntitySelector))]
[JsonSerializable(typeof(ScoreboardSearcher))]
[JsonSerializable(typeof(string))]
[JsonSerializable(typeof(bool))]
[PublicAPI]
public partial class MineJasonTextJsonContext : JsonSerializerContext
{
}