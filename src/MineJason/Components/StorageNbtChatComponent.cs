// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: LGPL-3.0-or-later

namespace MineJason.Components;

using MineJason.Components.Builders;
using MineJason.Text;
using System;
using System.Diagnostics.CodeAnalysis;

/// <summary>
/// Represents an NBT chat component that sources the value from a storage. This class cannot be inherited.
/// </summary>
public sealed record StorageNbtChatComponent : BaseNbtChatComponent,
    IEquatable<StorageNbtChatComponent>
{
    /// <summary>
    /// The value of the <c>source</c> field identifying this type.
    /// </summary>
    public const string SourceName = "storage";

    /// <summary>
    /// Initializes a new instance of the <see cref="StorageNbtChatComponent"/> class.
    /// </summary>
    public StorageNbtChatComponent()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="StorageNbtChatComponent"/> class.
    /// </summary>
    /// <param name="storage">The ID of the storage.</param>
    /// <param name="path">The NBT path.</param>
    [SetsRequiredMembers]
    public StorageNbtChatComponent(ResourceLocation storage, string path)
    {
        Storage = storage;
        Path = path;
    }

    [SetsRequiredMembers]
    internal StorageNbtChatComponent(in TextComponentCreationInfo creationInfo,
        in NBTTextComponentCreationInfo nbtInfo,
        ResourceLocation storage) : base(creationInfo, nbtInfo)
    {
        Storage = storage;
    }

    /// <summary>
    /// Gets or sets the ID of the storage to source NBT from.
    /// </summary>
    public required ResourceLocation Storage { get; init; }

    /// <inheritdoc/>
    public override string ToString()
    {
        return $"[storage={Storage},nbt={Path}]";
    }
}
