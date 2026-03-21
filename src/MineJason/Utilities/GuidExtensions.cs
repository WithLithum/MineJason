// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: LGPL-3.0-or-later

namespace MineJason.Utilities;
using System;
using System.Buffers.Binary;

/// <summary>
/// Supports the operation of <see cref="Guid"/> and conversion between Minecraft
/// and .NET representations.
/// </summary>
public static class GuidExtensions
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
    /// Gets the most significant bits of the specified GUID.
    /// </summary>
    /// <param name="id">The GUID.</param>
    /// <returns>The most significant bits as a <see cref="long"/>.</returns>
    public static long GetMostSignificantBits(this Guid id)
    {
        // From: libsignal-service-dotnet

        byte[] gb = id.ToByteArray();
        byte[] ms =
        [
            gb[6], gb[7], gb[4], gb[5], gb[0], gb[1], gb[2], gb[3]
        ];
        return BitConverter.ToInt64(ms, 0);
    }

    /// <summary>
    /// Gets the least significant bits of the specified Guid.
    /// </summary>
    /// <param name="id">The GUID.</param>
    /// <returns>The least significant bits as a <see cref="long"/>.</returns>
    public static long GetLeastSignificantBits(this Guid id)
    {
        // From: libsignal-service-dotnet

        byte[] gb = id.ToByteArray();
        byte[] ls =
        [
            gb[15], gb[14], gb[13], gb[12], gb[11], gb[10], gb[9], gb[8]
        ];

        return BitConverter.ToInt64(ls, 0);
    }

    /// <summary>
    /// Converts the specified GUID to its Minecraft four-integer array representation.
    /// </summary>
    /// <param name="most">The most significant bits of the GUID.</param>
    /// <param name="least">The least significant bits of the GUID.</param>
    /// <returns>The converted value.</returns>
    public static int[] GuidToMinecraft(long most, long least)
    {
        return [(int)(most >> 32), (int)most, (int)(least >> 32), (int)least];
    }

    /// <summary>
    /// Converts the specified GUID to its Minecraft four-integer array representation.
    /// </summary>
    /// <param name="value">The GUID to convert from.</param>
    /// <returns>The converted value.</returns>
    public static int[] ToMinecraft(this Guid value)
    {
        Span<byte> bytes = stackalloc byte[16];
        FlipUuid(value, bytes);

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
    /// Creates a <see cref="Guid"/> using the most and least significant bits.
    /// </summary>
    /// <param name="most">The most significant bits.</param>
    /// <param name="least">The least significant bits.</param>
    /// <returns>The created <see cref="Guid"/>.</returns>
    public static Guid CreateGuid(long most, long least)
    {
        // From: libsignal-service-dotnet

        byte[] ms = BitConverter.GetBytes(most);
        byte[] ls = BitConverter.GetBytes(least);

        byte[] guidBytes =
        [
                ms[4], ms[5], ms[6], ms[7], ms[2], ms[3], ms[0], ms[1],
                ls[7], ls[6], ls[5], ls[4], ls[3], ls[2], ls[1], ls[0]
        ];

        return new Guid(guidBytes);
    }

    /// <summary>
    /// Creates a <see cref="Guid"/> by using the Minecraft integer format as an input.
    /// </summary>
    /// <param name="array">The array.</param>
    /// <returns>The created <see cref="Guid"/>.</returns>
    [Obsolete("Use CreateGuid(int, int, int, int) instead.")]
    public static Guid CreateGuid(int[] array)
    {
        return CreateGuid(array[0], array[1], array[2], array[3]);
    }

    /// <summary>
    /// Converts the specified four-integer array representation of UUID to a new instance of the
    /// <see cref="Guid"/> representing the same value.
    /// </summary>
    /// <param name="i1">The first component.</param>
    /// <param name="i2">The second component.</param>
    /// <param name="i3">The third component.</param>
    /// <param name="i4">The fourth component.</param>
    /// <returns>The converted GUID instance.</returns>
    public static Guid CreateGuid(int i1, int i2, int i3, int i4)
    {
        Span<byte> buf = stackalloc byte[16];

        BinaryPrimitives.WriteInt32BigEndian(buf[..4], i1);
        BinaryPrimitives.WriteInt32BigEndian(buf[4..8], i2);
        BinaryPrimitives.WriteInt32BigEndian(buf[8..12], i3);
        BinaryPrimitives.WriteInt32BigEndian(buf[12..], i4);

        return new Guid(buf, true);
    }
}
