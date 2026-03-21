// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

namespace MineJason.Serialization.Schema;

using System.Text.Json;
using MineJason.Serialization.IO;
using MineJason.Serialization.Utilities.Results;

public class StringEnumValueSchema<TEnum> : ValueSchema<TEnum>
    where TEnum : struct, Enum
{
    private readonly JsonNamingPolicy? _namingPolicy;
    private Dictionary<string, TEnum>? _lookup;

    public StringEnumValueSchema(JsonNamingPolicy? namingPolicy = null)
    {
        _namingPolicy = namingPolicy;
    }

    private void Initialize()
    {
        var values = Enum.GetValues<TEnum>();
        _lookup = new Dictionary<string, TEnum>(values.Length);

        foreach (var value in values)
        {
            var name = Enum.GetName(value);
            if (name == null)
            {
                continue;
            }

            if (_namingPolicy != null)
            {
                name = _namingPolicy.ConvertName(name);
            }

            _lookup.Add(name, value);
        }
    }

    public override Result<TElement> Encode<TElement>(TEnum value, IValueEncoder<TElement> encoder,
        string? elementName = null)
    {
        var name = Enum.GetName(value);
        if (name == null)
        {
            return Errors.Error("Unknown enum value");
        }

        if (_namingPolicy != null)
        {
            name = _namingPolicy.ConvertName(name);
        }

        return encoder.CreateString(name, elementName);
    }

    public override Result<TEnum> Decode<TElement>(TElement value, IValueDecoder<TElement> decoder)
    {
        if (_lookup == null)
        {
            Initialize();
        }

        if (!decoder.GetString(value)
            .Deconstruct(out var str,
            out var status))
        {
            return status.AsError();
        }

        if (!_lookup!.TryGetValue(str, out var enumValue))
        {
            return Errors.Error($"'{str}' doesn't match any known enum value");
        }

        return enumValue;
    }
}