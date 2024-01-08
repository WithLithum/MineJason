// Copyright (c) WithLithum & contributors 2023-2024. All rights reserved.
// Licensed under the GNU Lesser General Public License, either version 3 or
// (at your opinion) any later version.

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