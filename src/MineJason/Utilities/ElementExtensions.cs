// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: LGPL-3.0-or-later

namespace MineJason.Utilities;

using MineJason.Serialization.IO;
using MineJason.Serialization.Utilities.Results;

internal static class ElementExtensions
{
    internal static Result<T> Get<T>(this Result<IReadOnlyObjectLike<T>> obj,
        string name)
    {
        if (!obj)
        {
            return obj.AsError();
        }

        return obj.Value!.Get(name);
    }
}