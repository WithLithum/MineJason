// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: LGPL-3.0-or-later

namespace MineJason.Data.Profile;

/// <summary>
/// Specifies the skin and cape textures, as well as the model of a player.
/// </summary>
/// <remarks>
/// All the properties in the record are optional. If none are specified, the game will use a
/// default value of a Steve skin.
/// </remarks>
public record PlayerProfile
{
    /// <summary>
    /// Gets the name of the player profile to reference.
    /// </summary>
    public string? Name { get; init; }

    /// <summary>
    /// Gets the identifier of the player profile to reference.
    /// </summary>
    /// <value>
    /// The UUID of the player. If absent, the game will resolve the value from <see cref="Name"/>
    /// using the Mojang API.
    /// </value>
    public Guid? Id { get; init; }

    /// <summary>
    /// Gets a list of additional properties of the player profile referenced.
    /// </summary>
    /// <value>
    /// A list of optional properties. If absent, the game will resolve the value from the
    /// referenced player profile.
    /// </value>
    public IReadOnlyCollection<ProfileProperty>? Properties { get; init; }

    /// <summary>
    /// Gets the local skin texture to use.
    /// </summary>
    /// <value>
    /// The local skin texture. If specified, the skin resolved from the player profile is ignored.
    /// </value>
    public ResourceLocation? Texture { get; init; }

    /// <summary>
    /// Gets the local cape texture to use.
    /// </summary>
    /// <value>
    /// The local cape texture. If specified, the cape resolved from the player profile is ignored.
    /// </value>
    public ResourceLocation? Cape { get; init; }

    /// <summary>
    /// Gets the player model type to use.
    /// </summary>
    /// <value>
    /// The player model type. If specified, the model resolved from the player profile is ignored.
    /// </value>
    public PlayerModel? Model { get; init; }
}