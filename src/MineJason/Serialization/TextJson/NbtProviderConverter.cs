namespace MineJason.Serialization.TextJson;

using System.Text.Json;
using System.Text.Json.Serialization;
using MineJason.Data;

public class NbtProviderConverter : JsonConverter<NbtProvider>
{
    public override NbtProvider? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var value = reader.GetString();

        if (string.IsNullOrWhiteSpace(value))
        {
            throw new JsonException("Expected NBT");
        }

        return new NbtProvider(value);
    }

    public override void Write(Utf8JsonWriter writer, NbtProvider value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString());
    }
}