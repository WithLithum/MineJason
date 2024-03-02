// Copyright (c) WithLithum & contributors 2023-2024. All rights reserved.
// Licensed under the GNU Lesser General Public License, either version 3 or 
// (at your opinion) any later version.

namespace MineJason.Utilities;
using System;

/// <summary>
/// Provides extensions for GUIDs.
/// </summary>
public static class GuidExtensions
{
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
        return GuidToMinecraft(GetMostSignificantBits(value),
            GetLeastSignificantBits(value));
    }

    /// <summary>
    /// Creates a <see cref="Guid"/> using the most and least signifiant bits.
    /// </summary>
    /// <param name="most">The most signifiant bits.</param>
    /// <param name="least">The least signifiant bits.</param>
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
    public static Guid CreateGuid(int[] array)
    {
        return CreateGuid((long)array[0] << 32 | array[1] & 4294967295L, 
            (long)array[2] << 32 | array[3] & 4294967295L);
    }
}
