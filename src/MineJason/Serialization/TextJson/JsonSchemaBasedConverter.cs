// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: LGPL-3.0-or-later

using System.Text.Json;
using System.Text.Json.Serialization;
using MineJason.Serialization.Schema;

namespace MineJason.Serialization.TextJson;

/// <summary>
/// Encodes or decodes the specified type using the specified schema.
/// </summary>
/// <param name="schema">The schema to use.</param>
/// <typeparam name="T">The type to decode.</typeparam>
public abstract class JsonSchemaBasedConverter<T>(IValueSchema<T> schema)
    : JsonConverter<T>
{
    /// <inheritdoc />
    public override T? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using var doc = JsonDocument.ParseValue(ref reader);

        var result = schema.Decode(doc.RootElement, SchemaCommons.JsonDecoder);
        return result.Error != null
            ? throw new JsonException($"Schema decode failed: {result.Error}")
            : result.Value;
    }

    /// <inheritdoc />
    public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
    {
        var result = schema.Encode(value, SchemaCommons.JsonEncoder);
        if (result.Error != null)
        {
            throw new JsonException($"Schema encode failed: {result.Error}");
        }
    
        result.Value!.WriteTo(writer, options);
    }
}