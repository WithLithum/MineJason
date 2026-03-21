// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

namespace MineJason.SNbt.Codecs;

using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using MineJason.SNbt.Parsing;
using MineJason.SNbt.Values;

/// <summary>
/// Converts <typeparamref name="T"/> to NBT.
/// </summary>
/// <typeparam name="T">The NBT to convert to.</typeparam>
/// <remarks>
/// Although it is recommended that you use a codec paired with a non-<see cref="ISNbtWritable"/> object whenever possible,
/// porting old code to use these can be challenging. This codec can be helpful if you need to integrate old code written using
/// <see cref="ISNbtWritable"/> with new code that uses <see cref="ISNbtCodec{T}"/> system.
/// </remarks>
/// <example>
/// This example depicts how to use <see cref="SNbtWritableCodec{T}"/> to add codec support for an existing
/// <see cref="ISNbtWritable"/> implementor.
/// <code lang="csharp">
/// [SNbtCodec(typeof(SNbtWritableCodec&lt;SNbtWritableCodec&gt;))]
/// public class Writable : ISNbtWritable
/// {
///     public Writable(string name, string value)
///     {
///         Name = name;
///         Value = value;
///     }
/// 
///     public string Name { get; set; }
///     public string Value { get; set; }
///
///     public void WriteTo(SNbtWriter writer)
///     {
///         writer.WriteStringValue($"name-{Name},value-{Value}")
///     }
/// }
///
/// var converter = new SNbtConverter();
/// var result = converter.ToSNbtString(new Writable("tryName", "valueEnd"));
///
/// Console.WriteLine(result);
/// // Result: "name-tryName,value-valueEnd"
/// </code>
/// </example>
[PublicAPI]
public class SNbtWritableCodec<T> : ISNbtCodec<T>
    where T: ISNbtWritable
{
    /// <summary>
    /// Unsupported operation.
    /// </summary>
    /// <param name="reader">The reader.</param>
    /// <returns>Does not return.</returns>
    /// <exception cref="NotSupportedException">Operation is not supported.</exception>
    [DoesNotReturn]
    public T Read(SNbtBasicTokenReader reader)
    {
        throw new NotSupportedException();
    }

    /// <inheritdoc />
    public void Write(T value, SNbtWriter writer)
    {
        value.WriteTo(writer);
    }
}