// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: LGPL-3.0-or-later

namespace MineJason.Data;

using System.Text.Json.Serialization;
using MineJason.Data.Nbt;
using MineJason.Serialization.TextJson;

/// <summary>
/// Represents a NBT data value. This value should not be created manually,
/// but rather be created by an NBT library adapter.
/// </summary>
/// <param name="raw">The raw NBT string. This value is not validated.</param>
[JsonConverter(typeof(NbtProviderConverter))]
[Obsolete("Use other implementations of INbtDataProvider instead.")]
public class NbtProvider(string raw) : INbtDataProvider
{
    /// <summary>
    /// Provides an empty NBT provider.
    /// </summary>
    public static readonly NbtProvider Empty = new("");
    
    /// <summary>
    /// Returns the string representation of this NBT component.
    /// </summary>
    /// <returns>The SNBT representation.</returns>
    public override string ToString() => raw;

    /// <inheritdoc />
    public string GetRawNbt()
    {
        return ToString();
    }
}