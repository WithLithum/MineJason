// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

using MineJason.SNbt.Parsing;
using MineJason.SNbt.Values;

namespace MineJason.SNbt.Codecs;

/// <summary>
/// Converts <see cref="T"/> to or from string NBT.
/// </summary>
/// <remarks>
/// <para>
/// Codecs are new ways to convert a value to or from string NBT, without the need to encapsulate them into an
/// implementation of <see cref="ISNbtWritable"/> interface. This helps with implementing types and collections where it
/// also needed to be serialized with other frameworks.
/// </para>
/// <para>
/// For a codec to be considered valid for a type, it must:
/// <list type="bullet">
///     <item>Be of exactly such type (no assignable or inherited). Codecs cannot be inherited.</item>
///     <item>Have a public, parameterless constructor.</item>
/// </list>
/// </para>
/// </remarks>
[Obsolete("Use Serialization instead.")]
public interface ISNbtCodec<T>
{
    /// <summary>
    /// Reads a NBT value from the reader and converts it to <typeparamref name="T"/>.
    /// </summary>
    /// <param name="reader">The reader.</param>
    /// <returns>The converted value.</returns>
    T Read(SNbtBasicTokenReader reader);

    /// <summary>
    /// Writes the specified value as a NBT value to the specified writer.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <param name="writer">The writer.</param>
    void Write(T value, SNbtWriter writer);
}
