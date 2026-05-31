// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

using MineJason.Data;
using MineJason.Extras.Selectors;
using MineJason.Extras.Selectors.Matching;

namespace MineJason.Tests.Extras.Selectors;

public class EntitySelectorTests
{
    [Fact]
    public void ToString_NoArguments()
    {
        // Arrange
        var selector = new EntitySelector(EntitySelectorKind.AllPlayers);

        // Act
        var result = selector.ToString();

        // Assert
        Assert.Equal("@a", result);
    }

    [Fact]
    public void ToString_Distance()
    {
        // Arrange
        var selector = new EntitySelector(EntitySelectorKind.AllPlayers)
        {
            Distance = new DistanceRange(null, 2)
        };

        // Act
        var result = selector.ToString();

        // Assert
        Assert.Equal("@a[distance=..2]", result);
    }

    [Fact]
    public void ToString_DistanceWithCoords()
    {
        // Arrange
        var selector = new EntitySelector(EntitySelectorKind.AllPlayers)
        {
            Distance = new DistanceRange(null, 2),
            Position = new Vector3D(21d, 32d, 1050d)
        };

        // Act
        var result = selector.ToString();

        // Assert
        Assert.Equal("@a[x=21,y=32,z=1050,distance=..2]", result);
    }

    [Fact]
    public void ToString_Diagonal()
    {
        // Arrange
        var selector = new EntitySelector(EntitySelectorKind.AllPlayers)
        {
            DiagonalRange = new Vector3D(3d, 3d, 3d)
        };

        // Act
        var result = selector.ToString();

        // Assert
        Assert.Equal("@a[dx=3,dy=3,dz=3]", result);
    }

    [Fact]
    public void ToString_DiagonalWithCoords()
    {
        // Arrange
        var selector = new EntitySelector(EntitySelectorKind.AllPlayers)
        {
            Position = new Vector3D(25d, 100d, 1032d),
            DiagonalRange = new Vector3D(3d, 4d, 5d)
        };

        // Act
        var result = selector.ToString();

        // Assert
        Assert.Equal("@a[x=25,y=100,z=1032,dx=3,dy=4,dz=5]", result);
    }

    [Fact]
    public void ToString_GameMode_Exact()
    {
        // Arrange
        var selector = new EntitySelector(EntitySelectorKind.AllPlayers);
        selector.GameMode.MatchExact(GameMode.Creative);

        // Act
        var result = selector.ToString();

        // Assert
        Assert.Equal("@a[gamemode=creative]", result);
    }

    [Fact]
    public void ToString_GameMode_Exclude()
    {
        // Arrange
        var selector = new EntitySelector(EntitySelectorKind.AllPlayers);
        selector.GameMode.MatchExclude(GameMode.Adventure, GameMode.Spectator);

        // Act
        var result = selector.ToString();

        // Assert
        Assert.Equal("@a[gamemode=!adventure,gamemode=!spectator]", result);
    }

    [Fact]
    public void ToString_Name_Exact()
    {
        // Arrange
        var selector = new EntitySelector(EntitySelectorKind.AllPlayers);
        selector.Name.Add("Exact_Name");

        // Act
        var result = selector.ToString();

        // Assert
        Assert.Equal("@a[name=Exact_Name]", result);
    }

    [Fact]
    public void ToString_Name_Exclude()
    {
        // Arrange
        var selector = new EntitySelector(EntitySelectorKind.AllPlayers);
        selector.Name.Add("Exclude_Me", false);
        selector.Name.Add("Nothing", false);

        // Act
        var result = selector.ToString();

        // Assert
        Assert.Equal("@a[name=!Exclude_Me,name=!Nothing]", result);
    }

    [Fact]
    public void ToString_Scores()
    {
        // Arrange
        var selector = new EntitySelector(EntitySelectorKind.AllPlayers)
        {
            Scores =
            {
                { "obj0", new IntegralRange(null, 2) },
                { "obj1", new IntegralRange(null, 15) }
            }
        };

        // Act
        var result = selector.ToString();

        // Assert
        Assert.Equal("@a[scores={obj0=..2,obj1=..15}]", result);
    }

    [Fact]
    public void Formatter_Parse_Scores()
    {
        // Arrange
        var collection = new ScoreboardRangeCollection();
        EntitySelectorStringFormatter.ParseScores("{obj0=..3,obj1=..250}", collection);

        // Act
        var result = collection.ToString();

        // Assert
        Assert.Equal("{obj0=..3,obj1=..250}", result);
    }

    [Fact]
    public void Formatter_Parse_ThreeDifferentScores()
    {
        // Arrange
        var collection = new ScoreboardRangeCollection();
        EntitySelectorStringFormatter.ParseScores("{obj0=..3,obj1=250,obj2=2..}", collection);

        // Act
        var result = collection.ToString();

        // Assert
        Assert.Equal("{obj0=..3,obj1=250,obj2=2..}", result);
    }

    [Fact]
    public void Formatter_Parse_AllScores()
    {
        // Arrange
        var collection = new ScoreboardRangeCollection();
        EntitySelectorStringFormatter.ParseScores("{obj0=..3,obj1=250,obj2=2..,obj3=20..25}", collection);

        // Act
        var result = collection.ToString();

        // Assert
        Assert.Equal("{obj0=..3,obj1=250,obj2=2..,obj3=20..25}", result);
    }

    [Fact]
    public void Formatter_Parse_DistanceRange()
    {
        // Arrange
        const string range = "..150.5";

        // Act
        var result = EntitySelectorStringFormatter.ParseDistanceRange(range);

        // Assert
        Assert.Equal(DistanceRange.MatchRange(null, 150.5D),
            result);
    }

    [Fact]
    public void Formatter_Parse_DistanceRangeBoth()
    {
        // Arrange
        const string range = "22.5..150.5";

        // Act
        var result = EntitySelectorStringFormatter.ParseDistanceRange(range);

        // Assert
        Assert.Equal(DistanceRange.MatchRange(22.5D, 150.5D),
            result);
    }

    [Fact]
    public void Formatter_Parse_DistanceExact()
    {
        // Arrange
        const string range = "111.2";

        // Act
        var result = EntitySelectorStringFormatter.ParseDistanceRange(range);

        // Assert
        Assert.Equal(DistanceRange.MatchExact(111.2D),
            result);
    }

    [Fact]
    public void ScoreboardValueRange_MinMax()
    {
        // Arrange
        const string objective = "objective";
        const string value = "222..233";

        // Act
        var range = EntitySelectorParser.ParseScoresRange(objective, value);

        // Assert
        Assert.Equal(new ScoreboardRangeMatch("objective", new IntegralRange(222, 233)),
            range);
    }

    [Fact]
    public void ScoreboardValueRange_Min()
    {
        // Arrange
        const string objective = "objective";
        const string value = "255..";

        // Act
        var range = EntitySelectorParser.ParseScoresRange(objective, value);

        // Assert
        Assert.Equal(new ScoreboardRangeMatch("objective", new IntegralRange(255, null)),
            range);
    }

    [Fact]
    public void ScoreboardValueRange_Max()
    {
        // Arrange
        const string objective = "objective";
        const string value = "..205";

        // Act
        var range = EntitySelectorParser.ParseScoresRange(objective, value);

        // Assert
        Assert.Equal(new ScoreboardRangeMatch("objective", new IntegralRange(null, 205)),
            range);
    }

    [Fact]
    public void ScoreboardValueRange_Exact()
    {
        // Arrange
        const string objective = "objective";
        const string value = "377";

        // Act
        var range = EntitySelectorParser.ParseScoresRange(objective, value);

        // Assert
        Assert.Equal(new ScoreboardExactMatch("objective", 377),
            range);
    }

    [Fact]
    public void Parser_Scoreboard()
    {
        // Arrange
        const string value = "{ad=1..,ab=5,ac=..10}";
        var collection = new ScoreboardRangeCollection();

        // Act
        EntitySelectorParser.ParseScoresValue(value, collection);

        // Assert
        Assert.Equal(value, collection.ToString());
    }

    [Fact]
    public void Parser_Teams_Include()
    {
        // Arrange
        var teams = new TeamSelector();
        const string teamName = "included";

        // Act
        EntitySelectorParser.ParseTeamsValue(teamName, teams);

        Assert.Equal("team=included", teams.ToString());
    }

    [Fact]
    public void Parser_Teams_ExcludesMultiple()
    {
        // Arrange
        var teams = new TeamSelector();

        // Act
        EntitySelectorParser.ParseTeamsValue("!exclude", teams);
        EntitySelectorParser.ParseTeamsValue("!hello", teams);
        var result = teams.ToString();

        // Assert
        Assert.Equal("team=!exclude,team=!hello", result);
    }

    [Fact]
    public void Parser_AdvancementIncomplete()
    {
        // Arrange
        const string sampleString = "@a[advancements={minecraft:adventure/kill_all_mobs}]";

        // Act
        var exception = Record.Exception(() => EntitySelectorStringFormatter.ParseSelector(sampleString).ToString());

        // Assert
        Assert.IsType<FormatException>(exception);
    }

    [Fact]
    public void Factory_AllPlayers_CorrectKind()
    {
        // Act
        var result = EntitySelector.AllPlayers()
            .Build();

        // Assert
        Assert.Equal(EntitySelectorKind.AllPlayers, result.Kind);
    }

    [Fact]
    public void Factory_NearestPlayer_CorrectKind()
    {
        // Act
        var result = EntitySelector.NearestPlayer()
            .Build();

        // Assert
        Assert.Equal(EntitySelectorKind.NearestPlayer, result.Kind);
    }

    [Fact]
    public void Factory_RandomPlayer_CorrectKind()
    {
        // Act
        var result = EntitySelector.RandomPlayer()
            .Build();

        // Assert
        Assert.Equal(EntitySelectorKind.RandomPlayer, result.Kind);
    }

    [Fact]
    public void Factory_AllEntities_CorrectKind()
    {
        // Act
        var result = EntitySelector.AllEntities()
            .Build();

        // Assert
        Assert.Equal(EntitySelectorKind.AllEntities, result.Kind);
    }

    [Fact]
    public void Factory_Executor_CorrectKind()
    {
        // Act
        var result = EntitySelector.Executor()
            .Build();

        // Assert
        Assert.Equal(EntitySelectorKind.Executor, result.Kind);
    }

    [Fact]
    public void Factory_NearestEntity_CorrectKind()
    {
        // Act
        var result = EntitySelector.NearestEntity()
            .Build();

        // Assert
        Assert.Equal(EntitySelectorKind.NearestEntity, result.Kind);
    }

    [Fact]
    public void Builder_Origin_Produces()
    {
        // Act
        var result = EntitySelector.NearestEntity()
            .Position(new Vector3D(25d, 0d, 25d))
            .Build();

        // Assert
        Assert.Equal("@n[x=25,y=0,z=25]", result.ToString());
    }

    [Fact]
    public void Builder_DistanceExact_Produces()
    {
        // Act
        var result = EntitySelector.NearestEntity()
            .Distance(10)
            .Build();

        // Assert
        Assert.Equal("@n[distance=10]", result.ToString());
    }

    [Fact]
    public void Builder_DistanceRange_Produces()
    {
        // Act
        var result = EntitySelector.NearestEntity()
            .Distance(20, 100)
            .Build();

        // Assert
        Assert.Equal("@n[distance=20..100]", result.ToString());
    }

    [Fact]
    public void Builder_HasName_Produces()
    {
        // Act
        var result = EntitySelector.NearestEntity()
            .HasName("Steve")
            .Build();

        // Assert
        Assert.Equal("@n[name=Steve]", result.ToString());
    }

    [Fact]
    public void Builder_ExcludeName_Produces()
    {
        // Act
        var result = EntitySelector.NearestEntity()
            .ExcludeName("Alex")
            .Build();

        // Assert
        Assert.Equal("@n[name=!Alex]", result.ToString());
    }

    [Fact]
    public void Builder_HasLevel_Produces()
    {
        // Act
        var result = EntitySelector.NearestEntity()
            .HasLevel(new IntegralRange(20, 30))
            .Build();

        // Assert
        Assert.Equal("@n[level=20..30]", result.ToString());
    }

    [Fact]
    public void Builder_InTeam_Produces()
    {
        // Act
        var result = EntitySelector.NearestEntity()
            .InTeam("red")
            .Build();

        // Assert
        Assert.Equal("@n[team=red]", result.ToString());
    }

    [Fact]
    public void Builder_ExcludeTeam_Produces()
    {
        // Act
        var result = EntitySelector.NearestEntity()
            .ExcludeTeam("blue")
            .Build();

        // Assert
        Assert.Equal("@n[team=!blue]", result.ToString());
    }

    [Fact]
    public void Builder_IncludeTag_Produces()
    {
        // Act
        var result = EntitySelector.NearestEntity()
            .IncludeTag("a_tag")
            .Build();

        // Assert
        Assert.Equal("@n[tag=a_tag]", result.ToString());
    }

    [Fact]
    public void Builder_ExcludeTag_Produces()
    {
        // Act
        EntitySelector? result = EntitySelector.NearestEntity()
            .ExcludeTag("b_tag")
            .Build();

        // Assert
        Assert.Equal("@n[tag=!b_tag]", result.ToString());
    }

    [Fact]
    public void Builder_Diagonal_Produces()
    {
        // Act
        EntitySelector? result = EntitySelector.NearestEntity()
            .DiagonalRange(new Vector3D(30.5d, 3d, 25d))
            .Build();

        // Assert
        Assert.Equal("@n[dx=30.5,dy=3,dz=25]", result.ToString());
    }

    [Fact]
    public void Builder_Limit_Produces()
    {
        // Act
        EntitySelector? result = EntitySelector.AllEntities()
            .Limit(10)
            .Build();

        // Assert
        Assert.Equal("@e[limit=10]", result.ToString());
    }

    [Fact]
    public void Builder_SortBy_Produces()
    {
        // Act
        EntitySelector? result = EntitySelector.AllEntities()
            .SortBy(EntitySelectorSortMode.Nearest)
            .Build();

        // Assert
        Assert.Equal("@e[sort=nearest]", result.ToString());
    }

    [Fact]
    public void Builder_MatchType_Produces()
    {
        // Act
        EntitySelector? result = EntitySelector.AllEntities()
            .MatchType(new ResourceLocation("minecraft", "bat"))
            .Build();

        // Assert
        Assert.Equal("@e[type=minecraft:bat]", result.ToString());
    }

    [Fact]
    public void Builder_ExcludeType_Produces()
    {
        // Act
        EntitySelector? result = EntitySelector.AllEntities()
            .ExcludeType(new ResourceLocation("minecraft", "armor_stand"))
            .Build();

        // Assert
        Assert.Equal("@e[type=!minecraft:armor_stand]", result.ToString());
    }
}