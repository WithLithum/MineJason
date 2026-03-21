// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

namespace MineJason.Serialization.IO;

using MineJason.Serialization.Utilities.Results;

/// <summary>
/// Provides extension methods to <see cref="IValueDecoder{T}"/> implementations.
/// </summary>
public static class ValueDecoderExtensions
{
    public static Result<bool> GetBoolean<T>(this IValueDecoder<T> decoder,
        Result<T> element)
    {
        if (!element)
        {
            return element.AsError();
        }

        return decoder.GetBoolean(element.Value!);
    }

    public static Result<byte> GetByte<T>(this IValueDecoder<T> decoder,
        Result<T> element)
    {
        if (!element)
        {
            return element.AsError();
        }

        return decoder.GetByte(element.Value!);
    }

    public static Result<sbyte> GetSByte<T>(this IValueDecoder<T> decoder,
        Result<T> element)
    {
        if (!element)
        {
            return element.AsError();
        }

        return decoder.GetSByte(element.Value!);
    }

    public static Result<ushort> GetUInt16<T>(this IValueDecoder<T> decoder,
        Result<T> element)
    {
        if (!element)
        {
            return element.AsError();
        }

        return decoder.GetUInt16(element.Value!);
    }

    public static Result<short> GetInt16<T>(this IValueDecoder<T> decoder,
        Result<T> element)
    {
        if (!element)
        {
            return element.AsError();
        }

        return decoder.GetInt16(element.Value!);
    }

    public static Result<uint> GetUInt32<T>(this IValueDecoder<T> decoder,
        Result<T> element)
    {
        if (!element)
        {
            return element.AsError();
        }

        return decoder.GetUInt32(element.Value!);
    }

    public static Result<int> GetInt32<T>(this IValueDecoder<T> decoder,
        Result<T> element)
    {
        if (!element)
        {
            return element.AsError();
        }

        return decoder.GetInt32(element.Value!);
    }

    public static Result<ulong> GetUInt64<T>(this IValueDecoder<T> decoder,
        Result<T> element)
    {
        if (!element)
        {
            return element.AsError();
        }

        return decoder.GetUInt64(element.Value!);
    }

    public static Result<long> GetInt64<T>(this IValueDecoder<T> decoder,
        Result<T> element)
    {
        if (!element)
        {
            return element.AsError();
        }

        return decoder.GetInt64(element.Value!);
    }

    public static Result<float> GetSingle<T>(this IValueDecoder<T> decoder,
        Result<T> element)
    {
        if (!element)
        {
            return element.AsError();
        }

        return decoder.GetSingle(element.Value!);
    }

    public static Result<double> GetDouble<T>(this IValueDecoder<T> decoder,
        Result<T> element)
    {
        if (!element)
        {
            return element.AsError();
        }

        return decoder.GetDouble(element.Value!);
    }

    public static Result<string> GetString<T>(this IValueDecoder<T> decoder,
        Result<T> element)
    {
        if (!element)
        {
            return element.AsError();
        }

        return decoder.GetString(element.Value!);
    }

    public static Result<IEnumerable<byte>> GetByteArray<T>(this IValueDecoder<T> decoder,
        Result<T> element)
    {
        if (!element)
        {
            return element.AsError();
        }

        return decoder.GetByteArray(element.Value!);
    }

    public static Result<IEnumerable<int>> GetInt32Array<T>(this IValueDecoder<T> decoder,
        Result<T> element)
    {
        if (!element)
        {
            return element.AsError();
        }

        return decoder.GetInt32Array(element.Value!);
    }

    public static Result<IEnumerable<long>> GetInt64Array<T>(this IValueDecoder<T> decoder,
        Result<T> element)
    {
        if (!element)
        {
            return element.AsError();
        }

        return decoder.GetInt64Array(element.Value!);
    }

    public static Result<IEnumerable<T>> GetCollection<T>(this IValueDecoder<T> decoder,
        Result<T> element)
    {
        if (!element)
        {
            return element.AsError();
        }

        return decoder.GetCollection(element.Value!);
    }

    public static Result<IReadOnlyObjectLike<T>> GetObjectLike<T>(
        this IValueDecoder<T> decoder,
        Result<T> element)
    {
        if (!element)
        {
            return element.AsError();
        }

        return decoder.GetObjectLike(element.Value!);
    }
}