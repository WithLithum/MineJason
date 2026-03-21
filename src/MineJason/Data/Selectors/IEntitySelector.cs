// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: LGPL-3.0-or-later

namespace MineJason.Data.Selectors;

using System.Text.Json.Serialization;
using MineJason.Serialization.TextJson;

/// <summary>
/// Defines an entity selector.
/// </summary>
[JsonConverter(typeof(AnySelectorConverter))]
public interface IEntitySelector
{
}