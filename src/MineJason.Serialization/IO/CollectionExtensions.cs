// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

namespace MineJason.Serialization.IO;

using MineJason.Serialization.Utilities.Results;

public static class CollectionExtensions
{
    public static Result Add<T>(this ICollectionLikeWritable<T> collection,
        Result<T> value)
    {
        return !value.IsSuccess(out var v) ? value.AsError() : collection.Add(v);
    }

    public static Result Add<TValue, TElement>(this IArrayLikeWritable<TValue, TElement> array,
        Result<TValue> value)
    {
        return !value.IsSuccess(out var v) ? value.AsError() : array.Add(v);
    }

    public static Result Add<T>(this IWriteOnlyObjectLike<T> obj,
        string name,
        Result<T> value)
    {
        return !value.IsSuccess(out var v) ? value.AsError() : obj.Add(name, v);
    }

    public static Result Add<T>(this ICollection<T> collection,
        Result<T> value)
    {
        if (!value.IsSuccess(out var v))
        {
            return value.AsError();
        }

        collection.Add(v);
        return Result.Success();
    }
}