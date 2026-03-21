// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: LGPL-3.0-or-later

namespace MineJason.Text;

using JetBrains.Annotations;
using MineJason.Serialization.TextJson;
using System.Text.Json.Serialization;

/// <summary>
/// Represents a text component that displays the specified sprite from the specified texture
/// atlas.
/// </summary>
[PublicAPI]
[JsonConverter(typeof(TextComponentConverter))]
public abstract record ObjectTextComponent : ChatComponent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ObjectTextComponent"/> class.
    /// </summary>
    private protected ObjectTextComponent()
    {
    }

    /// <summary>
    /// Initializes a new instance of <see cref="ObjectTextComponent"/> with the specified data.
    /// </summary>
    /// <param name="creationInfo">The values to assign to properties.</param>
    private protected ObjectTextComponent(in TextComponentCreationInfo creationInfo) : base(creationInfo)
    {
    }

    /// <summary>
    /// Gets the component that is displayed instead in places where object sprite cannot be
    /// displayed.
    /// </summary>
    /// <value>
    /// The fallback component.
    /// </value>
    public ChatComponent? Fallback { get; init; }
}
