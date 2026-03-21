// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

namespace MineJason.SNbt.Values.Guids;

using System;
using System.Buffers.Binary;

/// <summary>
/// Provides methods to assist working with GUID.
/// </summary>
public static class GuidHelper
{
    private static void FlipUuid(Span<byte> bytes)
    {
        if (bytes.Length != 16)
        {
            throw new ArgumentException("The specified span is not of the length of an UUID.");
        }

        // Flip bytes in a rapid fashion
        // Segment 1

        var a = bytes[0];
        var b = bytes[1];
        var c = bytes[2];
        var d = bytes[3];

        bytes[0] = d;
        bytes[1] = c;
        bytes[2] = b;
        bytes[3] = a;

        // Segment 2

        var e = bytes[4];
        var f = bytes[5];

        bytes[4] = f;
        bytes[5] = e;

        // Segment 3

        var g = bytes[6];
        var h = bytes[7];

        bytes[6] = h;
        bytes[7] = g;
    }

    private static void FlipUuid(Guid from, Span<byte> destination)
    {
        if (!from.TryWriteBytes(destination))
        {
            throw new ArgumentException("The specified GUID cannot be written into buffer.",
                nameof(from));
        }

        FlipUuid(destination);
    }

    /// <summary>
    /// Writes a <see cref="Guid"/> to the specified writer.
    /// </summary>
    /// <param name="guid">The GUID to write.</param>
    /// <param name="writer">The writer to write to.</param>
    public static void WriteGuid(Guid guid, SNbtWriter writer)
    {
        Span<byte> bytes = stackalloc byte[16];
        FlipUuid(guid, bytes);

        var b1 = bytes[..4];
        var b2 = bytes[4..8];
        var b3 = bytes[8..12];
        var b4 = bytes[12..16];

        writer.WriteBeginArray('I');
        writer.WriteComma();
        writer.WriteValue(BinaryPrimitives.ReadInt32BigEndian(b1));
        writer.WriteComma();
        writer.WriteValue(BinaryPrimitives.ReadInt32BigEndian(b2));
        writer.WriteComma();
        writer.WriteValue(BinaryPrimitives.ReadInt32BigEndian(b3));
        writer.WriteComma();
        writer.WriteValue(BinaryPrimitives.ReadInt32BigEndian(b4));
        writer.WriteEndList();
    }

    /// <summary>
    /// Converts the specified <see cref="Guid"/> to an array of four integers.
    /// </summary>
    /// <param name="guid">The GUID to write.</param>
    /// <returns>An array of four integers representing the GUID.</returns>
    public static int[] ToIntArray(Guid guid)
    {
        Span<byte> bytes = stackalloc byte[16];
        FlipUuid(guid, bytes);

        var a = bytes[..4];
        var b = bytes[4..8];
        var c = bytes[8..12];
        var d = bytes[12..];

        return [ BinaryPrimitives.ReadInt32BigEndian(a),
            BinaryPrimitives.ReadInt32BigEndian(b),
            BinaryPrimitives.ReadInt32BigEndian(c),
            BinaryPrimitives.ReadInt32BigEndian(d)];
    }

    /// <summary>
    /// Converts the specified <see cref="Guid"/> to its Universally Unique Identifier
    /// binary representation.
    /// </summary>
    /// <param name="guid">The GUID bytes to convert from.</param>
    /// <returns>The converted bytes.</returns>
    public static byte[] ToUniversalBytes(Guid guid)
    {
        Span<byte> b = stackalloc byte[16];
        FlipUuid(guid, b);

        return b.ToArray();
    }

    /// <summary>
    /// Converts the specified binary representation of <see cref="Guid"/> to its Universally Unique Identifier
    /// binary representation.
    /// </summary>
    /// <param name="bytes">The GUID bytes to convert from.</param>
    /// <returns>The converted bytes.</returns>
    public static byte[] ToUniversalBytes(ReadOnlySpan<byte> bytes)
    {
        if (bytes.Length < 16)
        {
            throw new ArgumentException("The specified span must be exactly 16 bytes long.", nameof(bytes));
        }

        Span<byte> b = stackalloc byte[16];
        bytes.CopyTo(b);
        FlipUuid(b);

        return b.ToArray();
    }

    /// <summary>
    /// Converts the specified UUID to <see cref="Guid"/>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This method is deprecated in favour of the new .NET 8 constructor 
    /// <see cref="Guid(ReadOnlySpan{byte}, bool)"/>, which supports directly reading Java binary
    /// formatted UUIDs.
    /// </para>
    /// </remarks>
    /// <param name="bytes">The UUID to convert from.</param>
    /// <returns>The converted GUID.</returns>
    [Obsolete("Use 'new Guid(bytes, bool bigEndian)' instead.")]
    public static Guid FromUniversalBytes(ReadOnlySpan<byte> bytes)
    {
        if (bytes.Length != 16)
        {
            throw new ArgumentException("The specified GUID value array is not a 128-bit array.",
                nameof(bytes));
        }

        return new Guid(bytes, true);
    }

    /// <summary>
    /// Converts the specified components of a Minecraft four-integer UUID representation to GUID.
    /// </summary>
    /// <param name="i1">The first component.</param>
    /// <param name="i2">The second component.</param>
    /// <param name="i3">The third component.</param>
    /// <param name="i4">The fourth component.</param>
    /// <returns>The converted GUID.</returns>
    public static Guid FromMinecraft(int i1, int i2, int i3, int i4)
    {
        Span<byte> buf = stackalloc byte[16];

        BinaryPrimitives.WriteInt32BigEndian(buf[..4], i1);
        BinaryPrimitives.WriteInt32BigEndian(buf[4..8], i2);
        BinaryPrimitives.WriteInt32BigEndian(buf[8..12], i3);
        BinaryPrimitives.WriteInt32BigEndian(buf[12..], i4);

        return new Guid(buf, true);
    }

    /// <summary>
    /// Converts the specified Minecraft four-integer array UUID to GUID.
    /// </summary>
    /// <param name="uuid">The UUID to convert from. Must be an array of four <see cref="int"/>s.</param>
    /// <returns>The converted GUID.</returns>
    /// <exception cref="ArgumentException">The length of the specified UUID array is not 4.</exception>
    public static Guid FromMinecraft(int[] uuid)
    {
        if (uuid.Length != 4)
        {
            throw new ArgumentException("The UUID array length is not 4.", nameof(uuid));
        }

        return FromMinecraft(uuid[0], uuid[1], uuid[2], uuid[3]);
    }
}