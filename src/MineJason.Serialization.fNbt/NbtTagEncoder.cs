// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

namespace MineJason.Serialization.fNbt;

using global::fNbt;
using MineJason.Serialization.IO;

public class NbtTagEncoder : IValueEncoder<NbtTag>
{
    public NbtTag CreateBoolean(bool value, string? elementName = null)
    {
        return new NbtByte(elementName, value ? (byte)1 : (byte)0);
    }

    public NbtTag CreateByte(byte value, string? elementName = null)
    {
        return new NbtByte(elementName, value);
    }

    public NbtTag CreateSByte(sbyte value, string? elementName = null)
    {
        return new NbtByte(elementName, unchecked((byte)value));
    }

    public NbtTag CreateUInt16(ushort value, string? elementName = null)
    {
        return new NbtShort(elementName, unchecked((short)value));
    }

    public NbtTag CreateInt16(short value, string? elementName = null)
    {
        return new NbtShort(elementName, value);
    }

    public NbtTag CreateUInt32(uint value, string? elementName = null)
    {
        return new NbtInt(elementName, unchecked((int)value));
    }

    public NbtTag CreateInt32(int value, string? elementName = null)
    {
        return new NbtInt(elementName, value);
    }

    public NbtTag CreateUInt64(ulong value, string? elementName = null)
    {
        return new NbtLong(elementName, unchecked((long)value));
    }

    public NbtTag CreateInt64(long value, string? elementName = null)
    {
        return new NbtLong(elementName, value);
    }

    public NbtTag CreateSingle(float value, string? elementName = null)
    {
        return new NbtFloat(elementName, value);
    }

    public NbtTag CreateDouble(double value, string? elementName = null)
    {
        return new NbtDouble(elementName, value);
    }

    public NbtTag CreateString(string value, string? elementName = null)
    {
        return new NbtString(elementName, value);
    }

    public IArrayLikeWritable<byte, NbtTag> CreateByteArray(string? elementName = null)
    {
        return new NbtByteArrayProvider(elementName);
    }

    public IArrayLikeWritable<int, NbtTag> CreateInt32Array(string? elementName = null)
    {
        return new NbtIntArrayProvider(elementName);
    }

    public IArrayLikeWritable<long, NbtTag> CreateInt64Array(string? elementName = null)
    {
        return new NbtLongArrayProvider(elementName);
    }

    public ICollectionLikeWritable<NbtTag> CreateCollection(string? elementName = null)
    {
        return new NbtListAdapter(new NbtList(elementName));
    }

    public IWriteOnlyObjectLike<NbtTag> CreateObjectLike(string? elementName = null)
    {
        return new NbtCompoundAdapter(new NbtCompound(elementName));
    }
}