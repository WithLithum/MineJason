// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

namespace MineJason.SNbt.Values;

using MineJason.SNbt.Parsing;

/// <summary>
/// Represents a generic NBT compound.
/// </summary>
/// <remarks>
/// <para>
/// It is recommended that you either implement a custom object implementing <see cref="ISNbtWritable"/>, or
/// use <see cref="SNbtObjectBuilder"/>.
/// </para>
/// <para>
/// This NBT value type is provided only for the purpose that the compound does not refer to a static-typed
/// object but rather a dynamic dictionary.
/// </para>
/// </remarks>
public sealed class SNbtCompound : Dictionary<string, ISNbtWritable>, ISNbtWritable
{
    /// <inheritdoc/>
    public SNbtTagType Type => SNbtTagType.Compound;
    
    /// <inheritdoc/>
    public void WriteTo(SNbtWriter writer)
    {
        writer.WriteBeginCompound();

        foreach (var value in this)
        {
            writer.WriteProperty(value.Key, value.Value);
        }

        writer.WriteEndCompound();
    }
}