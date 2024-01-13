namespace MineJason.Serialization.TextJson;

using System.Text.Json;
using System.Text.Json.Serialization;
using MineJason.Data;

/// <summary>
/// Provides conversion services to <see cref="EntitySelector"/>.
/// </summary>
public class EntitySelectorConverter : JsonConverter<EntitySelector>
{
    /// <inheritdoc />
    public override EntitySelector? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public override void Write(Utf8JsonWriter writer, EntitySelector value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString());
    }
}