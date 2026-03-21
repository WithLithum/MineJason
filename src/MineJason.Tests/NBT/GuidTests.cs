// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

using MineJason.SNbt.Values.Guids;
using MineJason.SNbt;

namespace MineJason.Tests.NBT;

public class GuidTests
{
    [Fact]
    public void ToUniversalBytes_ValidGuid_ConvertsCorrectly()
    {
        // Arrange
        var guid = new Guid("df3e1abd-1465-4ffd-ab20-503d94a9605f");

        // Act
        var result = GuidHelper.ToUniversalBytes(guid);

        // Assert
        Assert.Equal(
            [0xdf, 0x3e, 0x1a, 0xbd,
            0x14, 0x65,
            0x4f, 0xfd,
            0xab, 0x20,
            0x50, 0x3d, 0x94, 0xa9, 0x60, 0x5f],
            result);
    }

    [Fact]
    public void ToIntArray_Guid_ConvertsCorrectly()
    {
        // Arrange
        var guid = new Guid("fa8fecb2-209c-4a3e-ada6-f634344bb4cf");

        // Act
        var result = GuidHelper.ToIntArray(guid);

        // Assert
        Assert.Equal([-91231054, 547113534, -1381566924, 877376719], result);
    }

    [Fact]
    public void WriteGuid_Guid_ConvertsCorrectly()
    {
        // Arrange
        var guid = new Guid("fa8fecb2-209c-4a3e-ada6-f634344bb4cf");
        var sw = new StringWriter();

        // Act
        using (var writer = new SNbtWriter(sw))
        {
            GuidHelper.WriteGuid(guid, writer);
        }

        // Assert
        Assert.Equal("[I;-91231054,547113534,-1381566924,877376719]", sw.ToString());
    }
}
