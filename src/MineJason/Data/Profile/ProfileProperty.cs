// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

namespace MineJason.Data.Profile;

/// <summary>
/// Specifies additional metadata for a player profile.
/// </summary>
public record ProfileProperty
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ProfileProperty"/> class.
    /// </summary>
    public ProfileProperty()
    {
    }
    
    /// <summary>
    /// Gets the name of the property.
    /// </summary>
    public required string Name { get; init; }

    /// <summary>
    /// Gets the value of the property.
    /// </summary>
    public required string Value { get; init; }

    /// <summary>
    /// Gets the base64-encoded Mojang signature of the value.
    /// </summary>
    public string? Signature { get; init; }
}