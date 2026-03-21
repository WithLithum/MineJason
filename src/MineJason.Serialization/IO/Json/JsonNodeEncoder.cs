// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

namespace MineJason.Serialization.IO.Json;

using System.Text.Json.Nodes;

public class JsonNodeEncoder : IValueEncoder<JsonNode>
{
    public JsonNode CreateBoolean(bool value, string? elementName = null)
    {
        return JsonValue.Create(value);
    }

    public JsonNode CreateByte(byte value, string? elementName = null)
    {
        return JsonValue.Create(value);
    }

    public JsonNode CreateSByte(sbyte value, string? elementName = null)
    {
        return JsonValue.Create(value);
    }

    public JsonNode CreateUInt16(ushort value, string? elementName = null)
    {
        return JsonValue.Create(value);
    }

    public JsonNode CreateInt16(short value, string? elementName = null)
    {
        return JsonValue.Create(value);
    }

    public JsonNode CreateUInt32(uint value, string? elementName = null)
    {
        return JsonValue.Create(value);
    }

    public JsonNode CreateInt32(int value, string? elementName = null)
    {
        return JsonValue.Create(value);
    }

    public JsonNode CreateUInt64(ulong value, string? elementName = null)
    {
        return JsonValue.Create(value);
    }

    public JsonNode CreateInt64(long value, string? elementName = null)
    {
        return JsonValue.Create(value);
    }

    public JsonNode CreateSingle(float value, string? elementName = null)
    {
        return JsonValue.Create(value);
    }

    public JsonNode CreateDouble(double value, string? elementName = null)
    {
        return JsonValue.Create(value);
    }

    public JsonNode CreateString(string value, string? elementName = null)
    {
        return JsonValue.Create(value);
    }

    public IArrayLikeWritable<byte, JsonNode> CreateByteArray(string? elementName = null)
    {
        return new JsonByteArrayProvider();
    }

    public IArrayLikeWritable<int, JsonNode> CreateInt32Array(string? elementName = null)
    {
        return new JsonInt32ArrayProvider();
    }

    public IArrayLikeWritable<long, JsonNode> CreateInt64Array(string? elementName = null)
    {
        return new JsonInt64ArrayProvider();
    }

    public ICollectionLikeWritable<JsonNode> CreateCollection(string? elementName = null)
    {
        var array = new JsonArray();
        return new JsonArrayAdapter(array);
    }

    public IWriteOnlyObjectLike<JsonNode> CreateObjectLike(string? elementName = null)
    {
        return new JsonObjectAdapter(new JsonObject());
    }
}