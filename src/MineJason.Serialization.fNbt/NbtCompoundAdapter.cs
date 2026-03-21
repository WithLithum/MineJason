// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

namespace MineJason.Serialization.fNbt;

using global::fNbt;
using MineJason.Serialization.IO;
using MineJason.Serialization.Utilities.Results;

public class NbtCompoundAdapter : IReadOnlyObjectLike<NbtTag>,
    IWriteOnlyObjectLike<NbtTag>
{
    private readonly NbtCompound _compound;

    public NbtCompoundAdapter(NbtCompound compound)
    {
        _compound = compound;
    }

    public bool ContainsKey(string name)
    {
        return _compound.Contains(name);
    }

    public IEnumerable<KeyValuePair<string, NbtTag>> EnumerateObject()
    {
        return _compound.Select(x => new KeyValuePair<string, NbtTag>(
            x.Name ?? "", x));
    }

    public Result<NbtTag> Get(string name)
    {
        var result = _compound.Get(name);
        if (result == null)
        {
            return Errors.NoSuchKey(name);
        }

        return result;
    }

    public Result Add(string key, NbtTag value)
    {
        if (value.Name != key)
        {
            return Result.Failure($"Mismatched tag key ('{value.Name}' vs '{key}')");
        }

        if (_compound.Contains(key))
        {
            return Errors.KeyAlreadyExists(key);
        }

        _compound.Add(value);
        return Result.Success();
    }

    public NbtTag GetContainer()
    {
        return _compound;
    }
}