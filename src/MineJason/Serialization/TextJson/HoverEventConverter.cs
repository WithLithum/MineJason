// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

using MineJason.Text.Behaviour.Hover;

namespace MineJason.Serialization.TextJson;

using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using MineJason.Serialization.Schema;

/// <summary>
/// Converts <see cref="HoverEvent"/> and its children from or to JSON.
/// </summary>
public class HoverEventConverter() :
    JsonSchemaBasedConverter<HoverEvent>(HoverEventSchema.Instance)
{
    /// <inheritdoc />
    public override bool CanConvert(Type typeToConvert)
    {
        return typeToConvert.IsAssignableTo(typeof(HoverEvent));
    }
}