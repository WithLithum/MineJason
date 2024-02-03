// Copyright (c) WithLithum & contributors 2023-2024. All rights reserved.
// Licensed under the GNU Lesser General Public License, either version 3 or 
// (at your opinion) any later version.

namespace MineJason.Tests.Models;

using MineJason.Data.Coordinates;

public class AnyBlockPositionTests
{
    [Test]
    public void AnyBlockPosition_LocalCoordinate()
    {
        var position = new AnyBlockPosition(new LocalBlockPosition(22, 23, 24));

        Assert.That(position.ToString(),
            Is.EqualTo("^22 ^23 ^24"));
    }

    [Test]
    public void AnyBlockPosition_LocalCoordinate_HaveZero()
    {
        var position = new AnyBlockPosition(new LocalBlockPosition(22, 0, 24));

        Assert.That(position.ToString(),
            Is.EqualTo("^22 ^ ^24"));
    }

    [Test]
    public void AnyBlockPosition_WorldCoordinate_Absolute()
    {
        var position = new AnyBlockPosition(new BlockPosition(150, 22, 137));

        Assert.That(position.ToString(),
            Is.EqualTo("150 22 137"));
    }

    [Test]
    public void AnyBlockPosition_WorldCoordinate_Relative()
    {
        var position = new AnyBlockPosition(new BlockPosition(12, 0, 1, true));

        Assert.That(position.ToString(),
            Is.EqualTo("~12 ~ ~1"));
    }
}
