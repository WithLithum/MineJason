// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

namespace MineJason.Data.Nbt;

/// <summary>
/// Defines configuration values for component conversion.
/// </summary>
public record ComponentConversionSettings
{
    /// <summary>
    /// The default settings.
    /// </summary>
    public static readonly ComponentConversionSettings Default = new();

    /// <summary>
    /// Gets a value indicating whether the <c>type</c> field should be omitted when converting a
    /// text component to NBT.
    /// </summary>
    public bool DoNotEmitTypeOnSerialize { get; init; }
}
