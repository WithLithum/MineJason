namespace MineJason.Serialization.TextJson;

using System.Text.Json;
using System.Text.Json.Serialization;
using MineJason.Data;

/// <summary>
/// Provides JSON conversion for <see cref="NbtDataSource"/>.
/// </summary>
public class NbtDataSourceConverter : JsonConverter<NbtDataSource>
{
    /// <inheritdoc />
    public override NbtDataSource Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var str = reader.GetString();

        if (string.IsNullOrWhiteSpace(str))
        {
            throw new JsonException("Expected string");
        }

        return str switch
        {
            "block" => NbtDataSource.Block,
            "entity" => NbtDataSource.Entity,
            "storage" => NbtDataSource.Storage,
            _ => throw new JsonException("Expected data source")
        };
    }

    /// <inheritdoc />
    public override void Write(Utf8JsonWriter writer, NbtDataSource value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString().ToLowerInvariant());
    }
}