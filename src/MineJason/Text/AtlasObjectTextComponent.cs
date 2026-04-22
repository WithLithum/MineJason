// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

namespace MineJason.Text;

using System.Diagnostics.CodeAnalysis;

/// <summary>
/// Represents a text component that displays a single sprite sourced from the specified atlas.
/// </summary>
public sealed record AtlasObjectTextComponent : ObjectTextComponent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AtlasObjectTextComponent"/> class.
    /// </summary>
    public AtlasObjectTextComponent()
    {
    }

    /// <summary>
    /// Initializes a new instance of <see cref="AtlasObjectTextComponent"/> with the specified
    /// values for properties.
    /// </summary>
    /// <param name="creationInfo">The values.</param>
    internal AtlasObjectTextComponent(in TextComponentCreationInfo creationInfo) : base(creationInfo)
    {
    }

    /// <summary>
    /// Initializes a new instance of <see cref="AtlasObjectTextComponent"/> with the specified
    /// sprite and optionally the atlas to source the sprite from.
    /// </summary>
    /// <param name="sprite">The sprite to display.</param>
    /// <param name="atlas">
    /// The atlas to source the sprite from.  If <see langword="null"/>, uses
    /// <c>minecraft:blocks</c>.
    /// </param>
    [SetsRequiredMembers]
    public AtlasObjectTextComponent(ResourceLocation sprite,
        ResourceLocation? atlas = null)
    {
        Sprite = sprite;
        Atlas = atlas;
    }

    /// <summary>
    /// Gets the identifier of the atlas to source the sprite from.
    /// </summary>
    /// <value>
    /// The atlas identifier. If <see langword="null"/>, uses <c>minecraft:blocks</c>.
    /// </value>
    public ResourceLocation? Atlas { get; init; }

    /// <summary>
    /// Gets the identifier of the sprite to display.
    /// </summary>
    /// <value>
    /// The sprite identifier.
    /// </value>
    public required ResourceLocation Sprite { get; init; }
}