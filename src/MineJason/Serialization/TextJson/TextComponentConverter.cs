// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

namespace MineJason.Serialization.TextJson;

using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using MineJason.Serialization.Schema;
using MineJason.Text;

/// <summary>
/// Converts text components from and to JSON.
/// </summary>
public class TextComponentConverter() :
    JsonSchemaBasedConverter<TextComponent>(TextComponentSchema.Instance)
{
    /// <inheritdoc />
    public override bool CanConvert(Type typeToConvert)
    {
        return typeToConvert.IsAssignableTo(typeof(TextComponent));
    }
}
