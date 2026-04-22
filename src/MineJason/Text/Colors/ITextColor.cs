// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

using System.Text.Json.Serialization;
using MineJason.Serialization.TextJson;

namespace MineJason.Text.Colors;

/// <summary>
/// Represents a text color.
/// </summary>
[JsonConverter(typeof(TextColorConverter))]
public interface ITextColor
{
    /// <summary>
    /// Generates the value of the <c>color</c> field.
    /// </summary>
    /// <returns>The value of the <c>color</c> field of the text.</returns>
    string GenerateColorText();
}