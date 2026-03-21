// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

namespace MineJason.Serialization.IO.Json;

using System.Text.Json;
using MineJason.Serialization.Utilities.Results;

public class JsonElementObjectAdapter : IReadOnlyObjectLike<JsonElement>
{
    private readonly JsonElement _element;

    public JsonElementObjectAdapter(JsonElement element)
    {
        if (element.ValueKind != JsonValueKind.Object)
        {
            throw new ArgumentException($"The specified element is not an object (it was {_element.ValueKind}).",
                nameof(element));
        }

        _element = element;
    }

    public bool ContainsKey(string name)
    {
        return _element.TryGetProperty(name, out _);
    }

    public IEnumerable<KeyValuePair<string, JsonElement>> EnumerateObject()
    {
        return _element.EnumerateObject()
            .Select(x => new KeyValuePair<string, JsonElement>(
                x.Name, x.Value));
    }

    public Result<JsonElement> Get(string name)
    {
        if (!_element.TryGetProperty(name, out var prop))
        {
            return Errors.NoSuchKey(name);
        }

        return prop;
    }
}