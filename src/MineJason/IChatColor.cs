using System.Text.Json.Serialization;
using MineJason.Serialization.TextJson;

namespace MineJason;

[JsonConverter(typeof(ChatColorConverter))]
public interface IChatColor
{
    /// <summary>
    /// Generates the value of the <c>color</c> field.
    /// </summary>
    /// <returns>The value of the <c>color</c> field of the text.</returns>
    string GenerateColorText();
}