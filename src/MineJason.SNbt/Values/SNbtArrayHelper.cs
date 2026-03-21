// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

namespace MineJason.SNbt.Values;

internal static class SNbtArrayHelper
{
    public static void WriteArray<T>(char specifier, IEnumerable<T> enumerable, SNbtWriter writer, char? suffix = null)
        where T : IFormattable
    {
        writer.WriteBeginArray(specifier);
        
        foreach (var value in enumerable)
        {
            writer.WriteComma();

            if (suffix.HasValue)
            {
                writer.WriteFormattable(value, suffix.Value);
            }
            else
            {
                writer.WriteFormattable(value);   
            }
        }
        
        writer.WriteEndList();
    }
    
    [Obsolete("Use ISNbtWritable methods instead.")]
    public static string ToSNbtString<T>(char specifier, IEnumerable<T> enumerable)
        where T : IFormattable
    {
        var stream = new StringWriter();
        using (var writer = new SNbtWriter(stream))
        {
            WriteArray(specifier, enumerable, writer);
        }

        return stream.ToString();
    }
}