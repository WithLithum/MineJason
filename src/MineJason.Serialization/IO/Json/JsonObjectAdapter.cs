// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

namespace MineJason.Serialization.IO.Json;

using System.Text.Json.Nodes;
using MineJason.Serialization.Utilities.Results;

public class JsonObjectAdapter : IReadOnlyObjectLike<JsonNode>,
    IWriteOnlyObjectLike<JsonNode>
{
    private readonly JsonObject _o;

    public JsonObjectAdapter(JsonObject o)
    {
        _o = o;
    }

    public bool ContainsKey(string name)
    {
        return _o.ContainsKey(name);
    }

    public IEnumerable<KeyValuePair<string, JsonNode>> EnumerateObject()
    {
        return _o!;
    }

    public Result<JsonNode> Get(string name)
    {
        if (!_o.TryGetPropertyValue(name, out var value)
            || value == null)
        {
            return Errors.NoSuchKey(name);
        }

        return value;
    }

    public Result Add(string key, JsonNode value)
    {
        if (_o.ContainsKey(key))
        {
            return Errors.KeyAlreadyExists(key);
        }

        _o.Add(key, value);
        return Result.Success();
    }

    public JsonNode GetContainer()
    {
        return _o;
    }
}