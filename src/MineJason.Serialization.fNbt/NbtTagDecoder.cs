// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

namespace MineJason.Serialization.fNbt;

using global::fNbt;
using MineJason.Serialization.IO;
using MineJason.Serialization.Utilities.Results;

public class NbtTagDecoder : IValueDecoder<NbtTag>
{
    public Result<bool> GetBoolean(NbtTag from)
    {
        if (from.TagType != NbtTagType.Byte)
        {
            return Errors.TypeMismatch(NbtTagType.Byte, from.TagType);
        }

        return from.ByteValue >= 1;
    }

    public Result<byte> GetByte(NbtTag from)
    {
        return from.TagType != NbtTagType.Byte
            ? Errors.TypeMismatch(NbtTagType.Byte, from.TagType)
            : from.ByteValue;
    }

    public Result<sbyte> GetSByte(NbtTag from)
    {
        return from.TagType != NbtTagType.Byte
            ? Errors.TypeMismatch(NbtTagType.Byte, from.TagType)
            : unchecked((sbyte)from.ByteValue);
    }

    public Result<ushort> GetUInt16(NbtTag from)
    {
        return from.TagType != NbtTagType.Short
            ? Errors.TypeMismatch(NbtTagType.Short, from.TagType)
            : unchecked((ushort)from.ShortValue);
    }

    public Result<short> GetInt16(NbtTag from)
    {
        return from.TagType != NbtTagType.Short
            ? Errors.TypeMismatch(NbtTagType.Short, from.TagType)
            : from.ShortValue;
    }

    public Result<uint> GetUInt32(NbtTag from)
    {
        return from.TagType != NbtTagType.Int
            ? Errors.TypeMismatch(NbtTagType.Int, from.TagType)
            : unchecked((uint)from.ShortValue);
    }

    public Result<int> GetInt32(NbtTag from)
    {
        return from.TagType != NbtTagType.Int
            ? Errors.TypeMismatch(NbtTagType.Int, from.TagType)
            : from.IntValue;
    }

    public Result<ulong> GetUInt64(NbtTag from)
    {
        return from.TagType != NbtTagType.Long
            ? Errors.TypeMismatch(NbtTagType.Long, from.TagType)
            : unchecked((ulong)from.LongValue);
    }

    public Result<long> GetInt64(NbtTag from)
    {
        return from.TagType != NbtTagType.Long
            ? Errors.TypeMismatch(NbtTagType.Long, from.TagType)
            : from.LongValue;
    }

    public Result<float> GetSingle(NbtTag from)
    {
        return from.TagType != NbtTagType.Float
            ? Errors.TypeMismatch(NbtTagType.Float, from.TagType)
            : from.FloatValue;
    }

    public Result<double> GetDouble(NbtTag from)
    {
        return from.TagType != NbtTagType.Double
            ? Errors.TypeMismatch(NbtTagType.Double, from.TagType)
            : from.DoubleValue;
    }

    public Result<string> GetString(NbtTag from)
    {
        return from.TagType != NbtTagType.String
            ? Errors.TypeMismatch(NbtTagType.String, from.TagType)
            : from.StringValue;
    }

    public Result<IEnumerable<byte>> GetByteArray(NbtTag from)
    {
        return from.TagType != NbtTagType.ByteArray
            ? Errors.TypeMismatch(NbtTagType.ByteArray, from.TagType)
            : from.ByteArrayValue;
    }

    public Result<IEnumerable<int>> GetInt32Array(NbtTag from)
    {
        return from.TagType != NbtTagType.IntArray
            ? Errors.TypeMismatch(NbtTagType.IntArray, from.TagType)
            : from.IntArrayValue;
    }

    public Result<IEnumerable<long>> GetInt64Array(NbtTag from)
    {
        return from.TagType != NbtTagType.LongArray
            ? Errors.TypeMismatch(NbtTagType.LongArray, from.TagType)
            : from.LongArrayValue;
    }

    public Result<IEnumerable<NbtTag>> GetCollection(NbtTag from)
    {
        if (from is not NbtList list)
        {
            return Errors.TypeMismatch(NbtTagType.List, from.TagType);
        }

        return list;
    }

    public Result<IReadOnlyObjectLike<NbtTag>> GetObjectLike(NbtTag from)
    {
        return from.TagType != NbtTagType.Compound
            ? Errors.TypeMismatch(NbtTagType.Compound, from.TagType)
            : new NbtCompoundAdapter((NbtCompound)from);
    }
}