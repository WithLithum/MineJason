namespace MineJason.Data;

using System.Text.Json.Serialization;
using MineJason.Serialization.TextJson;

[JsonConverter(typeof(NbtDataSourceConverter))]
public enum NbtDataSource
{
    Block,
    Entity,
    Storage
}