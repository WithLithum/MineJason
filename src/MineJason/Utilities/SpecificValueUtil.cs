// Copyright (c) WithLithum & contributors 2023-2024. All rights reserved.
// Licensed under the GNU Lesser General Public License, either version 3 or
// (at your opinion) any later version.

namespace MineJason.Utilities;

internal static class SpecificValueUtil
{
    internal static bool TryParseLowerBoolean(string from, out bool result)
    {
        result = default;

        switch (from)
        {
            case "true" :
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