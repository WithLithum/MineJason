// Copyright (c) WithLithum & contributors 2023-2024. All rights reserved.
// Licensed under the GNU Lesser General Public License, either version 3 or
// (at your opinion) any later version.

namespace MineJason.Utilities;

using MineJason.Serialization.TextJson;
using System.Globalization;
using System.Text.Json;

internal static class SpecificValueUtil
{
    internal static string ToStringNeutral(this int value)
    {
        return value.ToString(CultureInfo.InvariantCulture);
    }

    internal static string ToStringNeutral(this IFormattable value)
    {
        return value.ToString(null, CultureInfo.InvariantCulture);
    }

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

    internal static string EscapeNbtString(string text)
    {
        return text.Replace("\'", "\\\'", StringComparison.Ordinal);
    }
    
    internal static string ToEscapedComponentString(ChatComponent component)
    {
        var json = JsonSerializer.Serialize(component, 
            typeof(ChatComponent), 
            MineJasonTextJsonContext.Default);

        json = json.Replace("\'", "\\\'", StringComparison.Ordinal);

        return $"'{json}'";
    }
}