// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: LGPL-3.0-or-later

namespace MineJason.Data.Components;

using MineJason.Serialization.Schema;

/// <summary>
/// Provides utility methods related to data components.
/// </summary>
public static class DataComponents
{
    /// <summary>
    /// Returns a new wrapper instance that encodes or decodes a <see cref="IDataComponentValue"/>
    /// through the specified schema. This method only supports
    /// <see cref="DataComponentValue{TValue}"/> and <see cref="EmptyDataComponentValue"/>.
    /// </summary>
    /// <param name="valueSchema">The schema to wrap.</param>
    /// <typeparam name="TValue">The type of the value.</typeparam>
    /// <returns>The wrapper instance.</returns>
    public static IValueSchema<IDataComponentValue> WrapSchema<TValue>(
        IValueSchema<TValue> valueSchema)
    {
        return new DataComponentSchemaWrapper<TValue>(valueSchema);
    }
}