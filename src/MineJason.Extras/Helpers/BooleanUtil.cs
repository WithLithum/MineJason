// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

namespace MineJason.Extras.Helpers;

internal static class BooleanUtil
{
    internal static bool TryParseLowerBoolean(in ReadOnlySpan<char> from, out bool result)
    {
        result = default;

        switch (from)
        {
            case "true":
                result = true;
                break;
            case "false":
                result = false;
                break;
            default:
                return false;
        }

        return true;
    }

    internal static string ToLowerBooleanString(bool value)
    {
        return value ? "true" : "false";
    }
}
