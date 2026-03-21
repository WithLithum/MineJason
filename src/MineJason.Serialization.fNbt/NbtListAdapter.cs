// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

namespace MineJason.Serialization.fNbt;

using global::fNbt;
using MineJason.Serialization.IO;
using MineJason.Serialization.Utilities.Results;

public class NbtListAdapter : ICollectionLikeWritable<NbtTag>
{
    private readonly NbtList _list;

    public NbtListAdapter(NbtList list)
    {
        _list = list;
    }

    public Result Add(NbtTag value)
    {
        if (_list.ListType != value.TagType)
        {
            return Result.Failure($"List type ('{value.TagType}') and " +
                                $"tag type ('{_list.ListType}') mismatch");
        }

        _list.Add(value);
        return Result.Success();
    }

    public NbtTag GetContainer()
    {
        return _list;
    }
}