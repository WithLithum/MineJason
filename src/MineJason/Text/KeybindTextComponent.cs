// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;

namespace MineJason.Text;

/// <summary>
/// Represents a text component that displays the key associated with the specified key settings
/// configured on the client displayed. This class cannot be inherited.
/// </summary>
[PublicAPI]
public sealed record KeybindTextComponent : TextComponent,
    IEquatable<KeybindTextComponent>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="KeybindTextComponent"/> class.
    /// </summary>
    public KeybindTextComponent()
    {
    }

    /// <summary>
    /// Initializes a new instance of <see cref="KeybindTextComponent"/> with the specified
    /// keybind.
    /// </summary>
    /// <param name="keybind">The identifier of the key to display.</param>
    [SetsRequiredMembers]
    public KeybindTextComponent(string keybind)
    {
        Keybind = keybind;
    }

    internal KeybindTextComponent(TextComponentCreationInfo creationInfo) : base(creationInfo)
    {
    }

    /// <summary>
    /// Gets the ID of the key binding entry.
    /// </summary>
    public required string Keybind { get; init; }
}
