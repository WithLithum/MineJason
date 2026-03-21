// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

// Copyright (C) 2025-2026 WithLithum & contributors
// SPDX-License-Identifier: Apache-2.0

namespace MineJason.Serialization.IO.Json;

using System.Text.Json.Nodes;
using MineJason.Serialization.Utilities.Results;

public class JsonInt32ArrayProvider : IArrayLikeWritable<int, JsonNode>
{
    private readonly JsonArray _array = [];

    public Result Add(int value)
    {
        _array.Add((JsonNode)JsonValue.Create(value));
        return Result.Success();
    }

    public JsonNode GetContainer()
    {
        return _array;
    }
}