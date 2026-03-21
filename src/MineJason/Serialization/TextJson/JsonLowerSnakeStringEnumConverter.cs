// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: LGPL-3.0-or-later

namespace MineJason.Serialization.TextJson;

using System.Text.Json;
using System.Text.Json.Serialization;

/// <summary>
/// Converts an enum from and to JSON.
/// </summary>
/// <typeparam name="T">The type of the enum to convert.</typeparam>
public class JsonLowerSnakeStringEnumConverter<T> : JsonStringEnumConverter<T>
    where T: struct, Enum
{
    /// <inheritdoc />
    public JsonLowerSnakeStringEnumConverter() : base(JsonNamingPolicy.SnakeCaseLower, false)
    {
    }
}