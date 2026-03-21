// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

namespace MineJason.Serialization.fNbt;

using global::fNbt;
using MineJason.Serialization.IO;
using MineJason.Serialization.Utilities.Results;

public class NbtIntArrayProvider : IArrayLikeWritable<int, NbtTag>
{
    private readonly List<int> _list = [];
    private readonly string? _elementName;

    public NbtIntArrayProvider(string? elementName)
    {
        _elementName = elementName;
    }

    public Result Add(int value)
    {
        _list.Add(value);
        return Result.Success();
    }

    public NbtTag GetContainer()
    {
        return new NbtIntArray(_elementName, _list.ToArray());
    }
}