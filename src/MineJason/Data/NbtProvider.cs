namespace MineJason.Data;

using System.Text.Json.Serialization;
using MineJason.Serialization.TextJson;

/// <summary>
/// Represents a NBT data value. This value should not be created manually,
/// but rather be created by an NBT library adapter.
/// </summary>
/// <param name="raw">The raw NBT string.</param>
[JsonConverter(typeof(NbtProviderConverter))]
public class NbtProvider(string raw)
{
    public override string ToString() => raw;
}