namespace MineJason.Serialization.TextJson;

using System.Text.Json;
using System.Text.Json.Serialization;
using MineJason.Data;

/// <summary>
/// Provides JSON conversion service for <see cref="NbtProvider"/>.
/// </summary>
public class NbtProviderConverter : JsonConverter<NbtProvider>
{
    /// <inheritdoc />
    public override NbtProvider? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var value = reader.GetString();

        if (string.IsNullOrWhiteSpace(value))
        {
            throw new JsonException("Expected NBT");
        }

        return new NbtProvider(value);
    }

    /// <inheritdoc />
    public override void Write(Utf8JsonWriter writer, NbtProvider value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString());
    }
}