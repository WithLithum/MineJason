// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

namespace MineJason.Utilities.Platform;

using MineJason.Data.Components;
using MineJason.Serialization.Schema;

/// <summary>
/// Defines a platform provider.
/// </summary>
public interface IPlatformProvider
{
    /// <summary>
    /// Obtains the schema of the given type.
    /// </summary>
    /// <param name="type">The type.</param>
    /// <returns>The schema.</returns>
    IValueSchema? GetValueSchema(Type type);

    /// <summary>
    /// Obtains the schema for the given data component.
    /// </summary>
    /// <param name="type">The type.</param>
    /// <returns>The schema.</returns>
    IValueSchema<IDataComponentValue>? GetDataComponentSchema(
        ResourceLocation type);
}