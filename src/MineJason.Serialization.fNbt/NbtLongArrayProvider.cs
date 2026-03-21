// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

namespace MineJason.Serialization.fNbt;

using global::fNbt;
using MineJason.Serialization.IO;
using MineJason.Serialization.Utilities.Results;

public class NbtLongArrayProvider : IArrayLikeWritable<long, NbtTag>
{
    private readonly List<long> _list = [];
    private readonly string? _elementName;

    public NbtLongArrayProvider(string? elementName)
    {
        _elementName = elementName;
    }

    public Result Add(long value)
    {
        _list.Add(value);
        return Result.Success();
    }

    public NbtTag GetContainer()
    {
        return new NbtLongArray(_elementName, _list.ToArray());
    }
}