// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

namespace MineJason.Serialization.Utilities;

using System.Reflection;
using System.Runtime.CompilerServices;

internal static class ReflectionHelper
{
    internal static bool IsReadOnlyStruct(Type type)
    {
        return type.IsValueType && type.GetCustomAttributes<IsReadOnlyAttribute>().Any();
    }
}