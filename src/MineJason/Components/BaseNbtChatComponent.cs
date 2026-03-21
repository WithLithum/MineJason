// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: LGPL-3.0-or-later

using MineJason.Components.Builders;
using MineJason.Text;
using System.Diagnostics.CodeAnalysis;

namespace MineJason.Components;

/// <summary>
/// Represents an NBT chat component.
/// </summary>
public abstract record BaseNbtChatComponent : ChatComponent, IEquatable<BaseNbtChatComponent>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BaseNbtChatComponent"/> class.
    /// </summary>
    private protected BaseNbtChatComponent()
    {
    }

    /// <summary>
    /// Initializes a new instance of <see cref="BaseNbtChatComponent"/> with the specified style
    /// settings data.
    /// </summary>
    /// <param name="baseCreationInfo">The base settings data.</param>
    /// <param name="nbtCreationInfo">The NBT settings data.</param>
    [SetsRequiredMembers]
    private protected BaseNbtChatComponent(in TextComponentCreationInfo baseCreationInfo,
        in NBTTextComponentCreationInfo nbtCreationInfo) : base(baseCreationInfo)
    {
        Path = nbtCreationInfo.Path;
        Separator = nbtCreationInfo.Separator;
        Interpret = nbtCreationInfo.Interpret;
        Plain = nbtCreationInfo.Plain;
    }

    /// <summary>
    /// Gets or sets the path to the NBT value to interpret.
    /// </summary>
    public required string Path { get; init; }

    /// <summary>
    /// Gets or sets a value indicating whether to attempt to interpret the NBT value retrieved
    /// as a text component.
    /// </summary>
    public bool Interpret { get; init; }

    /// <summary>
    /// Gets a value indicating whether to render NBT values without styling when it would be
    /// pretty printed and styled.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Settings both this property and <see cref="Interpret"/> to <see langword="true"/> at the
    /// same time is <i>not supported</i> by Minecraft.
    /// </para>
    /// <para>This property was introduced in 26.1.</para>
    /// </remarks>
    public bool Plain { get; init; }

    /// <summary>
    /// Gets or sets the component as the separator between multiple values.
    /// </summary>
    public ChatComponent? Separator { get; init; }
}
