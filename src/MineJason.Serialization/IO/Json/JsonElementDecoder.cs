// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

namespace MineJason.Serialization.IO.Json;

using System.Text.Json;
using MineJason.Serialization.Utilities.Results;

public class JsonElementDecoder : IValueDecoder<JsonElement>
{
    public Result<bool> GetBoolean(JsonElement from)
    {
        return from.ValueKind switch
        {
            JsonValueKind.True => true,
            JsonValueKind.False => false,
            _ => Errors.TypeMismatch("Boolean", from.ValueKind.ToString())
        };
    }

    public Result<byte> GetByte(JsonElement from)
    {
        if (from.ValueKind != JsonValueKind.Number)
        {
            return Errors.TypeMismatch(JsonValueKind.Number, from.ValueKind);
        }

        return !from.TryGetByte(out var result)
            ? Errors.NumberTypeMismatch(nameof(Byte))
            : result;
    }

    public Result<sbyte> GetSByte(JsonElement from)
    {
        if (from.ValueKind != JsonValueKind.Number)
        {
            return Errors.TypeMismatch(JsonValueKind.Number, from.ValueKind);
        }

        return !from.TryGetSByte(out var result)
            ? Errors.NumberTypeMismatch(nameof(SByte))
            : result;
    }

    public Result<ushort> GetUInt16(JsonElement from)
    {
        if (from.ValueKind != JsonValueKind.Number)
        {
            return Errors.TypeMismatch(JsonValueKind.Number, from.ValueKind);
        }

        return !from.TryGetUInt16(out var result)
            ? Errors.NumberTypeMismatch(nameof(UInt16))
            : result;
    }

    public Result<short> GetInt16(JsonElement from)
    {
        if (from.ValueKind != JsonValueKind.Number)
        {
            return Errors.TypeMismatch(JsonValueKind.Number, from.ValueKind);
        }

        return !from.TryGetInt16(out var result)
            ? Errors.NumberTypeMismatch(nameof(Int16))
            : result;
    }

    public Result<uint> GetUInt32(JsonElement from)
    {
        if (from.ValueKind != JsonValueKind.Number)
        {
            return Errors.TypeMismatch(JsonValueKind.Number, from.ValueKind);
        }

        return !from.TryGetUInt32(out var result)
            ? Errors.NumberTypeMismatch(nameof(UInt32))
            : result;
    }

    public Result<int> GetInt32(JsonElement from)
    {
        if (from.ValueKind != JsonValueKind.Number)
        {
            return Errors.TypeMismatch(JsonValueKind.Number, from.ValueKind);
        }

        return !from.TryGetInt32(out var result)
            ? Errors.NumberTypeMismatch(nameof(Int32))
            : result;
    }

    public Result<ulong> GetUInt64(JsonElement from)
    {
        if (from.ValueKind != JsonValueKind.Number)
        {
            return Errors.TypeMismatch(JsonValueKind.Number, from.ValueKind);
        }

        return !from.TryGetUInt64(out var result)
            ? Errors.NumberTypeMismatch(nameof(UInt64))
            : result;
    }

    public Result<long> GetInt64(JsonElement from)
    {
        if (from.ValueKind != JsonValueKind.Number)
        {
            return Errors.TypeMismatch(JsonValueKind.Number, from.ValueKind);
        }

        return !from.TryGetInt64(out var result)
            ? Errors.NumberTypeMismatch(nameof(Int64))
            : result;
    }

    public Result<float> GetSingle(JsonElement from)
    {
        if (from.ValueKind != JsonValueKind.Number)
        {
            return Errors.TypeMismatch(JsonValueKind.Number, from.ValueKind);
        }

        return !from.TryGetSingle(out var result)
            ? Errors.NumberTypeMismatch(nameof(Single))
            : result;
    }

    public Result<double> GetDouble(JsonElement from)
    {
        if (from.ValueKind != JsonValueKind.Number)
        {
            return Errors.TypeMismatch(JsonValueKind.Number, from.ValueKind);
        }

        return !from.TryGetDouble(out var result)
            ? Errors.NumberTypeMismatch(nameof(Double))
            : result;
    }

    public Result<string> GetString(JsonElement from)
    {
        if (from.ValueKind != JsonValueKind.String)
        {
            return Errors.TypeMismatch(JsonValueKind.String, from.ValueKind);
        }

        var result = from.GetString();
        if (result == null)
        {
            return Errors.NullDisallowed;
        }

        return result;
    }

    #region Arrays

    public Result<IEnumerable<byte>> GetByteArray(JsonElement from)
    {
        if (from.ValueKind != JsonValueKind.Array)
        {
            return Errors.TypeMismatch(JsonValueKind.Array, from.ValueKind);
        }

        var list = new List<byte>(from.GetArrayLength());
        foreach (var item in from.EnumerateArray())
        {
            if (item.ValueKind != JsonValueKind.Number)
            {
                return Errors.TypeMismatch("Number",
                    from.ValueKind.ToString());
            }

            if (!item.TryGetByte(out var value))
            {
                return Errors.NumberTypeMismatch(nameof(Byte));
            }

            list.Add(value);
        }

        return list.AsReadOnly();
    }

    public Result<IEnumerable<int>> GetInt32Array(JsonElement from)
    {
        if (from.ValueKind != JsonValueKind.Array)
        {
            return Errors.TypeMismatch(JsonValueKind.Array, from.ValueKind);
        }

        var list = new List<int>(from.GetArrayLength());
        foreach (var item in from.EnumerateArray())
        {
            if (item.ValueKind != JsonValueKind.Number)
            {
                return Errors.TypeMismatch("Number", from.ValueKind.ToString());
            }

            if (!item.TryGetInt32(out var value))
            {
                return Errors.NumberTypeMismatch(nameof(Int32));
            }

            list.Add(value);
        }

        return list.AsReadOnly();
    }

    public Result<IEnumerable<long>> GetInt64Array(JsonElement from)
    {
        if (from.ValueKind != JsonValueKind.Array)
        {
            return Errors.TypeMismatch(JsonValueKind.Array, from.ValueKind);
        }

        var list = new List<long>(from.GetArrayLength());
        foreach (var item in from.EnumerateArray())
        {
            if (item.ValueKind != JsonValueKind.Number)
            {
                return Errors.TypeMismatch(JsonValueKind.Number, from.ValueKind);
            }

            if (!item.TryGetInt64(out var value))
            {
                return Errors.NumberTypeMismatch(nameof(Int64));
            }

            list.Add(value);
        }

        return list.AsReadOnly();
    }

    #endregion

    public Result<IEnumerable<JsonElement>> GetCollection(JsonElement from)
    {
        if (from.ValueKind != JsonValueKind.Array)
        {
            return Errors.TypeMismatch(JsonValueKind.Array, from.ValueKind);
        }

        return from.EnumerateArray();
    }

    public Result<IReadOnlyObjectLike<JsonElement>> GetObjectLike(JsonElement from)
    {
        if (from.ValueKind != JsonValueKind.Object)
        {
            return Errors.TypeMismatch(JsonValueKind.Object, from.ValueKind);
        }

        return new JsonElementObjectAdapter(from);
    }
}