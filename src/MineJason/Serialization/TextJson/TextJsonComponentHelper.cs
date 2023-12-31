using System.Text.Json;
using System.Text.Json.Serialization;
using MineJason.Events.Hover;

namespace MineJason.Serialization.TextJson;

internal static class TextJsonComponentHelper
{
    internal static readonly JsonSerializerOptions SerializerOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
    };
}