// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

using MineJason.Data.Selectors;

namespace MineJason.Tests.Client.Selectors;

public class SelectorParseTest
{
    [Theory]
    [InlineData("@a",
        TestDisplayName = "No arguments")]
    [InlineData("@a[name=SomeName]", TestDisplayName = "Name includes")]
    [InlineData("@a[name=!SomeNameOne,name=!SomeNameTwo]",
        TestDisplayName = "Name excludes")]
    [InlineData("@a[tag=SomeNameOne,tag=SomeNameTwo]",
        TestDisplayName = "Tag includes")]
    [InlineData("@a[tag=!SomeNameOne,tag=!SomeNameTwo]",
        TestDisplayName = "Tag excludes")]
    [InlineData("@a[team=SomeNameOne]",
        TestDisplayName = "Team")]
    [InlineData("@a[team=!TeamA,team=!TeamB]",
        TestDisplayName = "Team excludes")]
    [InlineData("@a[type=minecraft:creeper]",
        TestDisplayName = "Type")]
    [InlineData("@a[type=!minecraft:creeper,type=!minecraft:skeleton]",
        TestDisplayName = "Type excludes")]
    [InlineData("@a[x=250,y=110,z=2491,distance=..20]",
        TestDisplayName = "Position and distance")]
    [InlineData("@a[scores={objective0=100,objective1=90..250,objective2=..77,objective3=55..}]",
        TestDisplayName = "Scores")]
    [InlineData("@a[gamemode=adventure]", TestDisplayName = "Game mode")]
    [InlineData("@a[gamemode=!adventure,gamemode=!creative]",
        TestDisplayName = "Game mode excludes")]
    [InlineData("@a[x=100,y=50,z=-100]", TestDisplayName = "With origin")]
    [InlineData("@a[x=100,y=50,z=-100,dx=15,dy=2,dz=12]",
        TestDisplayName = "Diagonal box")]
    [InlineData("@a[level=56..100]", TestDisplayName = "Levels")]
    [InlineData("@a[x_rotation=-180..180]", TestDisplayName = "X Rotation")]
    [InlineData("@a[y_rotation=-180..180]", TestDisplayName = "Y Rotation")]
    [InlineData("@a[nbt={Condition:1b}]", TestDisplayName = "NBT")]
    [InlineData("@a[nbt=!{Condition:1b}]", TestDisplayName = "NBT exclude")]
    [InlineData("@a[advancements={minecraft:adventure/kill_all_mobs={witch=true}}]",
        TestDisplayName = "Single advancement condition")]
    [InlineData("@a[advancements={minecraft:adventure/kill_all_mobs={witch=true},minecraft:adventure/kill_all_mobs={zombie=true}}]",
        TestDisplayName = "Multiple advancement conditions")]
    [InlineData("@a[advancements={minecraft:adventure/kill_all_mobs={witch=true,zombie=false,skeleton=true}}]",
        TestDisplayName = "Multiple advancement criterion")]
    [InlineData("@a[sort=nearest]", TestDisplayName = "Sort")]
    [InlineData("@a[predicate=custom:predicate]", TestDisplayName = "Predicate include")]
    [InlineData("@a[predicate=!custom:predicate]", TestDisplayName = "Predicate exclude")]
    [InlineData("@a[limit=10]", TestDisplayName = "Limit")]
    public void SelectorParser_EndToEnd_SameResult(string sample)
    { 
        // Act
        var parseResult = EntitySelectorStringFormatter.ParseSelector(sample).ToString();

        // Assert
        Assert.Equal(sample, parseResult);
    }
}