// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

using JetBrains.Annotations;
using MineJason.SNbt.Parsing;
using MineJason.SNbt.Values;

namespace MineJason.SNbt;

/// <summary>
/// Provides fluent-syntax building for string NBT compounds.
/// </summary>
/// <remarks>
/// <para>
/// Using this builder when implementing <see cref="ISNbtWritable"/> may come with additional unnecessary
/// overhead. It is recommended that you use <see cref="SNbtWriter"/> when doing so instead.
/// </para>
/// </remarks>
[PublicAPI]
public class SNbtObjectBuilder : ISNbtWritable
{
    private readonly SNbtCompound _compound = new();

    /// <inheritdoc/>
    public SNbtTagType Type => SNbtTagType.Compound;

    /// <summary>
    /// Appends a property.
    /// </summary>
    /// <param name="key">The key of the property.</param>
    /// <param name="value">The value of the property.</param>
    /// <returns></returns>
    public SNbtObjectBuilder Property(string key, ISNbtWritable value)
    {
        _compound.Add(key, value);
        return this;
    }

    /// <inheritdoc/>
    public void WriteTo(SNbtWriter writer)
    {
        _compound.WriteTo(writer);
    }
}