// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: LGPL-3.0-or-later

namespace MineJason.Data.Components;

/// <summary>
/// Represents a data component value that is empty and has exactly one instance; the
/// <see cref="Instance"/> property.
/// </summary>
public sealed class EmptyDataComponentValue : IDataComponentValue
{
    private EmptyDataComponentValue()
    {
    }

    /// <summary>
    /// Gets the singleton instance.
    /// </summary>
    public static EmptyDataComponentValue Instance { get; } = new();

    /// <inheritdoc />
    public bool IsRemoval => true;
}