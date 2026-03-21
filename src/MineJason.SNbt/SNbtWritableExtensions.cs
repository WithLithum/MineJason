// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

using MineJason.SNbt.Values;

namespace MineJason.SNbt;

/// <summary>
/// Provides extensions to <see cref="ISNbtWritable"/> implementations.
/// </summary>
public static class SNbtWritableExtensions
{
    /// <summary>
    /// Converts the value to its string representation.
    /// </summary>
    /// <param name="writable">The value instance.</param>
    /// <returns>The converted value.</returns>
    public static string ToSNbtString(this ISNbtWritable writable)
    {
        var stringWriter = new StringWriter();
        using (var nbtWriter = new SNbtWriter(stringWriter))
        {
            writable.WriteTo(nbtWriter);   
        }

        return stringWriter.ToString();
    }
}