// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: LGPL-3.0-or-later

namespace MineJason.Text;

using MineJason.Data.Profile;

/// <summary>
/// Represent a text component that displays a player head as a sprite.
/// </summary>
public sealed record PlayerObjectTextComponent : ObjectTextComponent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AtlasObjectTextComponent"/> class.
    /// </summary>
    public PlayerObjectTextComponent()
    {
    }

    /// <summary>
    /// Initializes a new instance of <see cref="AtlasObjectTextComponent"/> with the specified
    /// values for properties.
    /// </summary>
    /// <param name="creationInfo">The values.</param>
    internal PlayerObjectTextComponent(in TextComponentCreationInfo creationInfo) : base(creationInfo)
    {
    }

    /// <summary>
    /// Gets the player profile to display.
    /// </summary>
    public required PlayerProfile Player { get; init; }
}