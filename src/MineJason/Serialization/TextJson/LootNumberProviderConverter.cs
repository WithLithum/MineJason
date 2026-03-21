// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: LGPL-3.0-or-later

namespace MineJason.Serialization.TextJson;

using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using MineJason.Data.Loot.Numbers;

/// <summary>
/// Converts <see cref="ILootNumberProvider"/> and its implementation types to and from JSON.
/// </summary>
public class LootNumberProviderConverter : JsonConverter<ILootNumberProvider>
{
    private static readonly ReadOnlyDictionary<ResourceLocation, Type> SubTypes = new Dictionary<ResourceLocation, Type>()
    {
        { LootExactNumberProvider.Id, typeof(LootExactNumberProvider) },
        { LootUniformNumberProvider.Id, typeof(LootUniformNumberProvider) },
        { LootBinomialNumberProvider.Id, typeof(LootBinomialNumberProvider) },
        { LootScoreNumberProvider.Id, typeof(LootScoreNumberProvider) },
        { LootStorageNumberProvider.Id, typeof(LootStorageNumberProvider) }
    }.AsReadOnly();

    /// <inheritdoc />
    public override ILootNumberProvider Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var dom = JsonNode.Parse(ref reader);
        var root = dom!.Root;
        var rootKind = root.GetValueKind();

        // Simplified form of constant value
        if (rootKind == JsonValueKind.Number)
        {
            var number = root.AsValue()!.GetValue<float>();

            return new LootExactNumberProvider(number);
        }

        if (rootKind == JsonValueKind.Object)
        {
            var rootObj = root.AsObject();

            // Typed
            if (rootObj.TryGetPropertyValue("type", out var typeProperty))
            {
                if (typeProperty == null)
                {
                    throw new JsonException("Type cannot be null.");
                }

                return HandleTyped(root.AsObject(), typeProperty.AsValue());
            }

            // Shorthand for uniform distribution
            if (rootObj.TryGetPropertyValue("min", out var minProperty)
                && rootObj.TryGetPropertyValue("max", out var maxProperty)
                && minProperty != null
                && maxProperty != null)
            {
                return HandleSimpleUniform(minProperty.AsValue(), maxProperty.AsValue());
            }
        }

        throw new JsonException("Invalid number provider format.");
    }

    private static ILootNumberProvider HandleSimpleUniform(JsonValue minProperty, JsonValue maxProperty)
    {
        var min = minProperty.GetValue<float>();
        var max = maxProperty.GetValue<float>();

        return new LootUniformNumberProvider(min, max);
    }

    private static ILootNumberProvider HandleTyped(JsonObject root, JsonValue typeProperty)
    {
        var type = typeProperty.Deserialize<ResourceLocation>(MineJasonTextJsonContext.Default.ResourceLocation);
        if (!SubTypes.TryGetValue(type, out var finalType))
        {
            throw new JsonException("Unrecognised number provider type.");
        }

        // Remove type
        root.Remove("type");

        var result = root.Deserialize(finalType, MineJasonTextJsonContext.Default)!;
        if (result == null)
        {
            throw new JsonException("Number provider cannot be null.");
        }

        return (ILootNumberProvider)result;
    }

    /// <inheritdoc />
    public override void Write(Utf8JsonWriter writer, ILootNumberProvider value, JsonSerializerOptions options)
    {
        JsonSerializer.Serialize(writer, value, value.GetType(), MineJasonTextJsonContext.Default);
    }
}