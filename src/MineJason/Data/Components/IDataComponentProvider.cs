// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: LGPL-3.0-or-later

namespace MineJason.Data.Components;

using MineJason.Serialization.Schema;

/// <summary>
/// Defines a data component provider.
/// </summary>
public interface IDataComponentProvider
{
    /// <summary>
    /// Returns the schema associated with the specified data component type.
    /// </summary>
    /// <param name="id">The data component type.</param>
    /// <returns>The schema, or <see langword="null"/> if not supported.</returns>
    IValueSchema<IDataComponentValue>? GetSchema(ResourceLocation id);
}