// Copyright (c) WithLithum & contributors 2023-2024. All rights reserved.
// Licensed under the GNU Lesser General Public License, either version 3 or
// (at your opinion) any later version.

namespace MineJason.Data;

using System.Text.Json.Serialization;
using MineJason.Serialization.TextJson;

/// <summary>
/// Represents a NBT data value. This value should not be created manually,
/// but rather be created by an NBT library adapter.
/// </summary>
/// <param name="raw">The raw NBT string. This value is not validated.</param>
[JsonConverter(typeof(NbtProviderConverter))]
public class NbtProvider(string raw)
{
    /// <summary>
    /// Returns the string representation of this NBT component.
    /// </summary>
    /// <returns>The SNBT representation.</returns>
    public override string ToString() => raw;
}