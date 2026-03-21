// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: LGPL-3.0-or-later

namespace MineJason;

using System.Text.Json.Serialization;
using MineJason.Serialization.TextJson;

/// <summary>
/// Represents a chat color.
/// </summary>
[JsonConverter(typeof(ChatColorConverter))]
public interface IChatColor
{
    /// <summary>
    /// Generates the value of the <c>color</c> field.
    /// </summary>
    /// <returns>The value of the <c>color</c> field of the text.</returns>
    string GenerateColorText();
}