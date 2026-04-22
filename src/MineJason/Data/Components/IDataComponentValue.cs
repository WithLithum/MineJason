// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

namespace MineJason.Data.Components;

/// <summary>
/// Defines the access interface for data component values.
/// </summary>
public interface IDataComponentValue
{
    /// <summary>
    /// Gets a value indicating whether this instance is an empty placeholder value used to
    /// indicate a removal patch.
    /// </summary>
    /// <remarks>
    /// Flags accepting the presence of an empty value should use a specialized schema.
    /// </remarks>
    bool IsRemoval { get; }
}