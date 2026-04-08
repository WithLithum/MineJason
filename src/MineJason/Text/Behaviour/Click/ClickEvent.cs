// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: LGPL-3.0-or-later

using System.Text.Json.Serialization;
using MineJason.Serialization.TextJson;

namespace MineJason.Text.Behaviour.Click;

/// <summary>
/// Represents an action triggered by clicking on a text component with mouse.
/// </summary>
[JsonConverter(typeof(ClickEventConverter))]
public abstract class ClickEvent
{
    private protected ClickEvent()
    {
    }
}
