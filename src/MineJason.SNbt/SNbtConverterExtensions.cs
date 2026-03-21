// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

namespace MineJason.SNbt;

using MineJason.SNbt.Codecs;

/// <summary>
/// Provides conversion between common values and string NBT values.
/// </summary>
public static class SNbtConverterExtensions
{
    /// <summary>
    /// Converts the specified value to NBT string.
    /// </summary>
    /// <param name="converter">The converter to convert from.</param>
    /// <param name="value">The value.</param>
    /// <typeparam name="T">The type to convert.</typeparam>
    /// <returns>The converted string.</returns>
    public static string ToSNbtString<T>(this ISNbtConverter converter, T value)
    {
        using var writer = new StringWriter();
        var nbtWriter = new SNbtWriter(writer);
        
        converter.WriteTo(value, nbtWriter);
        return writer.ToString();
    }
    
    /// <summary>
    /// Converts the specified value to NBT string.
    /// </summary>
    /// <param name="converter">The converter to convert from.</param>
    /// <param name="value">The value.</param>
    /// <returns>The converted string.</returns>
    public static string ToSNbtString(this ISNbtConverter converter, object value)
    {
        using var writer = new StringWriter();
        var nbtWriter = new SNbtWriter(writer);
        
        converter.WriteTo(value, nbtWriter);
        return writer.ToString();
    }
}