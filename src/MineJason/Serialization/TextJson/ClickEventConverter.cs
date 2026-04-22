// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

using MineJason.Serialization.Schema;
using MineJason.Text.Behaviour.Click;

namespace MineJason.Serialization.TextJson;

/// <summary>
/// Converts <see cref="ClickEvent"/> and its inheritors to or from JSON.
/// </summary>
public class ClickEventConverter() : JsonSchemaBasedConverter<ClickEvent>(ClickEventSchema.TextInstance)
{
    /// <inheritdoc />
    public override bool CanConvert(Type typeToConvert)
    {
        return typeToConvert.IsAssignableTo(typeof(ClickEvent));
    }
}