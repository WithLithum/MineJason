// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: LGPL-3.0-or-later

namespace MineJason.Serialization.TextJson;

using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Text.Json.Serialization;

/// <summary>
/// Provides conversion for resource locations.
/// </summary>
public class ResourceLocationConverter : JsonConverter<ResourceLocation>
{
    /// <inheritdoc />
    public override ResourceLocation Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var str = reader.GetString();

        if (string.IsNullOrWhiteSpace(str))
        {
            throw new JsonException("Expected resource location");
        }

        if (!ResourceLocation.TryParse(str, out var result))
        {
            throw new JsonException("Invalid resource location");
        }

        return result;
    }

    /// <inheritdoc/>
    public override ResourceLocation ReadAsPropertyName(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var str = reader.GetString();

        if (string.IsNullOrWhiteSpace(str))
        {
            throw new JsonException("Expected resource location");
        }

        if (!ResourceLocation.TryParse(str, out var result))
        {
            throw new JsonException("Invalid resource location");
        }

        return result;
    }

    /// <inheritdoc />
    public override void Write(Utf8JsonWriter writer, ResourceLocation value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString());
    }

    /// <inheritdoc/>
    public override void WriteAsPropertyName(Utf8JsonWriter writer, [DisallowNull] ResourceLocation value, JsonSerializerOptions options)
    {
        writer.WritePropertyName(value.ToString());
    }
}
