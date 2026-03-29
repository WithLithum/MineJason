// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

using System.Text.Json.Serialization;
using MineJason.Data.Nbt;
using MineJason.Events;
using MineJason.Events.Hover;
using MineJason.Text;
using MineJason.Text.Colors;

namespace MineJason.Tests.Client.Json;

[JsonSerializable(typeof(ResourceLocation))]
[JsonSerializable(typeof(ClickEvent))]
[JsonSerializable(typeof(HoverEvent))]
[JsonSerializable(typeof(ITextColor))]
[JsonSerializable(typeof(RgbTextColor))]
[JsonSerializable(typeof(TextComponent))]
[JsonSerializable(typeof(INbtDataProvider))]
[JsonSerializable(typeof(RawNbtDataProvider))]
internal sealed partial class JsonTestContext : JsonSerializerContext;