// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

namespace MineJason.Serialization.IO.Json;

using System.Text.Json.Nodes;
using MineJason.Serialization.Utilities.Results;

public class JsonArrayAdapter : ICollectionLikeWritable<JsonNode>
{
    private readonly JsonArray _array;

    public JsonArrayAdapter(JsonArray array)
    {
        _array = array;
    }

    public Result Add(JsonNode value)
    {
        _array.Add(value);
        return Result.Success();
    }

    public JsonNode GetContainer()
    {
        return _array;
    }
}