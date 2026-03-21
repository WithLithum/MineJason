// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

namespace MineJason.Serialization.fNbt;

using global::fNbt;
using MineJason.Serialization.IO;
using MineJason.Serialization.Utilities.Results;

public class NbtByteArrayProvider : IArrayLikeWritable<byte, NbtTag>
{
    private readonly List<byte> _list = [];
    private readonly string? _elementName;

    public NbtByteArrayProvider(string? elementName)
    {
        _elementName = elementName;
    }

    public Result Add(byte value)
    {
        _list.Add(value);
        return Result.Success();
    }

    public NbtTag GetContainer()
    {
        return new NbtByteArray(_elementName, _list.ToArray());
    }
}