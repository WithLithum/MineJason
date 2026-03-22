// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

using MineJason.Utilities;

namespace MineJason.Tests.Client;

public class GuidExtensionsTest
{
    [Fact]
    public void CreateGuid_MostLeast_AssembleCorrectly()
    {
        // Arrange
        var most = -9073613827017520239L;
        var least = -7887279703237394335L;

        // Act
        var result = GuidExtensions.CreateGuid(most, least);

        // Assert
        Assert.Equal(new Guid("82140c4c-638c-4b91-928a-c0e604630061"),
            result);
    }

    [Fact]
    public void CreateGuid_MinecraftArray_AssembleCorrectly()
    {
        // Arrange
        var i1 = 440389001;
        var i2 = 1555384731;
        var i3 = -1499203857;
        var i4 = 225140588;

        // Act
        var result = GuidExtensions.CreateGuid(i1, i2, i3, i4);

        // Assert
        Assert.Equal(new Guid("1a3fcd89-5cb5-499b-a6a3-f6ef0d6b5f6c"),
            result);
    }

    [Fact]
    public void GetMostSignificantBits_GivenGuid_ReturnsCorrectly()
    {
        // Arrange
        var guid = new Guid("475550ec-5ce6-4d36-817e-484e538a69fe");

        // Act
        var msb = guid.GetMostSignificantBits();

        // Assert
        Assert.Equal(5140103525814390070, msb);
    }

    [Fact]
    public void GetLeastSignificantBits_GivenGuid_ReturnsCorrectly()
    {
        // Arrange
        var guid = new Guid("6ba20320-b47b-455d-bc12-b6e26447d4ab");

        // Act
        var lsb = guid.GetLeastSignificantBits();

        // Assert
        Assert.Equal(-4894648761537014613, lsb);
    }
}
