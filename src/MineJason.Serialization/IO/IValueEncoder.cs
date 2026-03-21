// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

namespace MineJason.Serialization.IO;

/// <summary>
/// Defines a service that converts the CLR primitive types into the specified element type.
/// </summary>
/// <remarks>
/// An encoder implementation must be able to convert the defined CLR primitive types. If needed,
/// the encoder may store unsigned types as bitwise equivalent signed values (or vice versa). In
/// such case, the decoder must be able to convert those values back to unsigned values.
/// </remarks>
/// <typeparam name="T">The element type to convert to.</typeparam>
public interface IValueEncoder<T>
{
    T CreateBoolean(bool value, string? elementName = null);

    T CreateByte(byte value, string? elementName = null);

    T CreateSByte(sbyte value, string? elementName = null);

    T CreateUInt16(ushort value, string? elementName = null);

    T CreateInt16(short value, string? elementName = null);

    T CreateUInt32(uint value, string? elementName = null);

    T CreateInt32(int value, string? elementName = null);

    T CreateUInt64(ulong value, string? elementName = null);

    T CreateInt64(long value, string? elementName = null);

    T CreateSingle(float value, string? elementName = null);

    T CreateDouble(double value, string? elementName = null);

    T CreateString(string value, string? elementName = null);

    IArrayLikeWritable<byte, T> CreateByteArray(string? elementName = null);
    IArrayLikeWritable<int, T> CreateInt32Array(string? elementName = null);
    IArrayLikeWritable<long, T> CreateInt64Array(string? elementName = null);

    ICollectionLikeWritable<T> CreateCollection(string? elementName = null);
    IWriteOnlyObjectLike<T> CreateObjectLike(string? elementName = null);
}