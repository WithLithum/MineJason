// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

using MineJason.Serialization.Utilities.Results;

namespace MineJason.Serialization.IO;

/// <summary>
/// Contains various useful methods for <see cref="IReadOnlyObjectLike{T}"/> and
/// <see cref="IWriteOnlyObjectLike{TElement}"/>.
/// </summary>
public static class ObjectLikeExtensions
{
    extension<T>(Result<IReadOnlyObjectLike<T>> obj)
    {
        /// <summary>
        /// Gets the value associated with the specified key. If the encapsulating
        /// <see cref="Result"/> did no indicate success, passes through the error.
        /// </summary>
        /// <param name="name">The key of the value to acquire.</param>
        /// <returns>The result of the operation.</returns>
        /// <seealso cref="IReadOnlyObjectLike{T}.Get(string)"/>
        public Result<T> Get(string name)
        {
            if (!obj.IsSuccess(out var result))
            {
                return obj.AsError();
            }

            return result.Get(name);
        }
    }
}
