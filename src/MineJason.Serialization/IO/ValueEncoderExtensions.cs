// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

namespace MineJason.Serialization.IO;

using MineJason.Serialization.Utilities.Results;

/// <summary>
/// Provides extension methods to <see cref="IValueEncoder{T}"/> implementations.
/// </summary>
public static class ValueEncoderExtensions
{
    public static Result<T> CreateBoolean<T>(this IValueEncoder<T> encoder,
        Result<bool> value,
        string? elementName)
    {
        if (!value)
        {
            return value.AsError();
        }

        return encoder.CreateBoolean(value.Value, elementName);
    }

    public static Result<T> CreateByte<T>(this IValueEncoder<T> encoder,
        Result<byte> value,
        string? elementName)
    {
        if (!value)
        {
            return value.AsError();
        }

        return encoder.CreateByte(value.Value, elementName);
    }

    public static Result<T> CreateSByte<T>(this IValueEncoder<T> encoder,
        Result<sbyte> value,
        string? elementName)
    {
        if (!value)
        {
            return value.AsError();
        }

        return encoder.CreateSByte(value.Value, elementName);
    }

    public static Result<T> CreateUInt16<T>(this IValueEncoder<T> encoder,
        Result<ushort> value,
        string? elementName)
    {
        if (!value)
        {
            return value.AsError();
        }

        return encoder.CreateUInt16(value.Value, elementName);
    }

    public static Result<T> CreateInt16<T>(this IValueEncoder<T> encoder,
        Result<short> value,
        string? elementName)
    {
        if (!value)
        {
            return value.AsError();
        }

        return encoder.CreateInt16(value.Value, elementName);
    }

    public static Result<T> CreateUInt32<T>(this IValueEncoder<T> encoder,
        Result<uint> value,
        string? elementName)
    {
        if (!value)
        {
            return value.AsError();
        }

        return encoder.CreateUInt32(value.Value, elementName);
    }

    public static Result<T> CreateInt32<T>(this IValueEncoder<T> encoder,
        Result<int> value,
        string? elementName)
    {
        if (!value)
        {
            return value.AsError();
        }

        return encoder.CreateInt32(value.Value, elementName);
    }

    public static Result<T> CreateUInt64<T>(this IValueEncoder<T> encoder,
        Result<ulong> value,
        string? elementName)
    {
        if (!value)
        {
            return value.AsError();
        }

        return encoder.CreateUInt64(value.Value, elementName);
    }

    public static Result<T> CreateInt64<T>(this IValueEncoder<T> encoder,
        Result<long> value,
        string? elementName)
    {
        if (!value)
        {
            return value.AsError();
        }

        return encoder.CreateInt64(value.Value, elementName);
    }

    public static Result<T> CreateSingle<T>(this IValueEncoder<T> encoder,
        Result<float> value,
        string? elementName)
    {
        if (!value)
        {
            return value.AsError();
        }

        return encoder.CreateSingle(value.Value, elementName);
    }

    public static Result<T> CreateDouble<T>(this IValueEncoder<T> encoder,
        Result<double> value,
        string? elementName)
    {
        if (!value)
        {
            return value.AsError();
        }

        return encoder.CreateDouble(value.Value, elementName);
    }

    public static Result<T> CreateString<T>(this IValueEncoder<T> encoder,
        Result<string> value,
        string? elementName)
    {
        if (!value)
        {
            return value.AsError();
        }

        return encoder.CreateString(value.Value!, elementName);
    }
}