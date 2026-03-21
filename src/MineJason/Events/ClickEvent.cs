// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: LGPL-3.0-or-later

namespace MineJason.Events;

using System.Text.Json.Serialization;
using MineJason.Serialization.TextJson;

/// <summary>
/// Represents a click event.
/// </summary>
[JsonConverter(typeof(ClickEventConverter))]
public abstract class ClickEvent
{
    private protected ClickEvent()
    {
    }
}
