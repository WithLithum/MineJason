// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: LGPL-3.0-or-later

namespace MineJason.Text.Builders.Utilities;

/// <summary>
/// Encapsulates data for creation of NBT text components.
/// </summary>
public readonly ref struct NbtTextComponentCreationInfo
{
    /// <summary>
    /// Gets the path.
    /// </summary>
    public string Path { get; init; }

    /// <summary>
    /// Gets the separator.
    /// </summary>
    public TextComponent? Separator { get; init; }

    /// <summary>
    /// Gets a value indicating whether to interpret values.
    /// </summary>
    public bool Interpret { get; init; }

    /// <summary>
    /// Gets a value indicating whether to disable pretty printing for NBT values.
    /// </summary>
    public bool Plain { get; init; }
}
