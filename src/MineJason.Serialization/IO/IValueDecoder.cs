// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

namespace MineJason.Serialization.IO;

using MineJason.Serialization.Utilities.Results;

/// <summary>
/// Defines a service that converts the specified element to their CLR types.
/// </summary>
/// <typeparam name="T">The element type.</typeparam>
public interface IValueDecoder<T>
{
    Result<bool> GetBoolean(T from);

    Result<byte> GetByte(T from);
    Result<sbyte> GetSByte(T from);

    Result<ushort> GetUInt16(T from);
    Result<short> GetInt16(T from);

    Result<uint> GetUInt32(T from);
    Result<int> GetInt32(T from);

    Result<ulong> GetUInt64(T from);
    Result<long> GetInt64(T from);

    Result<float> GetSingle(T from);
    Result<double> GetDouble(T from);

    Result<string> GetString(T from);

    Result<IEnumerable<byte>> GetByteArray(T from);
    Result<IEnumerable<int>> GetInt32Array(T from);
    Result<IEnumerable<long>> GetInt64Array(T from);

    Result<IEnumerable<T>> GetCollection(T from);
    Result<IReadOnlyObjectLike<T>> GetObjectLike(T from);
}