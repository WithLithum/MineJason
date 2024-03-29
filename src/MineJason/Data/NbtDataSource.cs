﻿// Copyright (c) WithLithum & contributors 2023-2024. All rights reserved.
// Licensed under the GNU Lesser General Public License, either version 3 or
// (at your opinion) any later version.

namespace MineJason.Data;

using System.Text.Json.Serialization;
using MineJason.Serialization.TextJson;

/// <summary>
/// An enumeration of all possible NBT chat component data sources.
/// </summary>
[JsonConverter(typeof(NbtDataSourceConverter))]
public enum NbtDataSource
{
    /// <summary>
    /// Indicates that the NBT value is read from a block entity.
    /// </summary>
    Block,
    /// <summary>
    /// Indicates that the NBT value is read from an entity.
    /// </summary>
    Entity,
    /// <summary>
    /// Indicates that the NBT value is read from a storage file.
    /// </summary>
    Storage
}