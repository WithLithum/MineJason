// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

namespace MineJason.Utilities;

using System.Runtime.CompilerServices;

internal static class ThrowHelper
{
    internal static void ThrowIfNegativeOrZero(int? value,
        [CallerArgumentExpression(nameof(value))] string? argName = null)
    {
        if (!value.HasValue)
        {
            return;
        }

        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(value.Value, argName);
    }
    
    internal static void ThrowIfNegative(int? value,
        [CallerArgumentExpression(nameof(value))] string? argName = null)
    {
        if (!value.HasValue)
        {
            return;
        }

        ArgumentOutOfRangeException.ThrowIfNegative(value.Value, argName);
    }
    
    internal static void ThrowIfGreaterThan(int? value,
        int other,
        [CallerArgumentExpression(nameof(value))] string? argName = null)
    {
        if (!value.HasValue)
        {
            return;
        }

        ArgumentOutOfRangeException.ThrowIfGreaterThan(value.Value, other, argName);
    }
}