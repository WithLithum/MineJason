// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

namespace MineJason.Data.Nbt;

using System.Text.Json.Serialization;
using MineJason.Serialization.TextJson;

/// <summary>
/// Defines a container for NBT data.
/// </summary>
[JsonConverter(typeof(NbtDataProviderConverter))]
public interface INbtDataProvider
{
    /// <summary>
    /// Gets the string NBT data.
    /// </summary>
    /// <returns>The string NBT data.</returns>
    string GetRawNbt();
}