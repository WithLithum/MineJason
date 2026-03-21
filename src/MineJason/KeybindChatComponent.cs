// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: LGPL-3.0-or-later

namespace MineJason;
using System;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using MineJason.Text;

/// <summary>
/// Represents a chat component that displays as the key associated with the specified key binding entry
/// of the client that this component is being presented.
/// </summary>
[PublicAPI]
public sealed record KeybindChatComponent : ChatComponent,
    IEquatable<KeybindChatComponent>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="KeybindChatComponent"/> class.
    /// </summary>
    public KeybindChatComponent()
    {
    }

    /// <summary>
    /// Initializes a new instance of <see cref="KeybindChatComponent"/> with the specified
    /// keybind.
    /// </summary>
    /// <param name="keybind"></param>
    [SetsRequiredMembers]
    public KeybindChatComponent(string keybind)
    {
        Keybind = keybind;
    }

    internal KeybindChatComponent(TextComponentCreationInfo creationInfo) : base(creationInfo)
    {
    }

    /// <summary>
    /// Gets the ID of the key binding entry.
    /// </summary>
    public required string Keybind { get; init; }
}
