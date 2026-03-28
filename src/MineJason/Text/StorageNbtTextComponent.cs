// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: LGPL-3.0-or-later

using System.Diagnostics.CodeAnalysis;
using MineJason.Text.Builders.Utilities;

namespace MineJason.Text;

/// <summary>
/// Represents a text component that resolves to the text representation of an NBT value retrieved
/// from a storage. This class cannot be inherited.
/// </summary>
/// <remarks>
/// This type of component is resolved on the server side and will display nothing if resolved on
/// the client as-is.
/// </remarks>
public sealed record StorageNbtTextComponent : NbtTextComponent,
    IEquatable<StorageNbtTextComponent>
{
    /// <summary>
    /// The value of the <c>source</c> field identifying this type.
    /// </summary>
    public const string SourceName = "storage";

    /// <summary>
    /// Initializes a new instance of the <see cref="StorageNbtTextComponent"/> class.
    /// </summary>
    public StorageNbtTextComponent()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="StorageNbtTextComponent"/> class.
    /// </summary>
    /// <param name="storage">The ID of the storage.</param>
    /// <param name="path">The NBT path.</param>
    [SetsRequiredMembers]
    public StorageNbtTextComponent(ResourceLocation storage, string path)
    {
        Storage = storage;
        Path = path;
    }

    [SetsRequiredMembers]
    internal StorageNbtTextComponent(in TextComponentCreationInfo creationInfo,
        in NbtTextComponentCreationInfo nbtInfo,
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
