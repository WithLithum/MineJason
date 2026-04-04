// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: LGPL-3.0-or-later

using MineJason.Text.Behaviour.Click;

namespace MineJason.Serialization.TextJson;

using MineJason.Events;
using MineJason.Serialization.Schema;

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