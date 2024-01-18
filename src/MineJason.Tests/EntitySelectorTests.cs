namespace MineJason.Tests;

using MineJason.Data;
using MineJason.Data.Selectors;

public class EntitySelectorTests
{
    [Test]
    public void ToString_NoArguments()
    {
        var selector = new EntitySelector(EntitySelectorKind.AllPlayers);
        
        Assert.That(selector.ToString(), Is.EqualTo("@a"));
    }
    
    [Test]
    public void ToString_Distance()
    {
        var selector = new EntitySelector(EntitySelectorKind.AllPlayers)
        {
            Distance = new DistanceRange()
            {
                Max = 2
            }
        };

        Assert.That(selector.ToString(), Is.EqualTo("@a[distance=..2]"));
    }
    
    [Test]
    public void ToString_DistanceWithCoords()
    {
        var selector = new EntitySelector(EntitySelectorKind.AllPlayers)
        {
            Distance = new DistanceRange()
            {
                Max = 2
            },
            Position = new Vector3D(21d, 32d, 1050d)
        };

        Assert.That(selector.ToString(), Is.EqualTo("@a[x=21,y=32,z=1050,distance=..2]"));
    }
    
    [Test]
    public void ToString_Diagonal()
    {
        var selector = new EntitySelector(EntitySelectorKind.AllPlayers)
        {
            DiagonalRange = new Vector3D(3d, 3d, 3d)
        };

        Assert.That(selector.ToString(), Is.EqualTo("@a[dx=3,dy=3,dz=3]"));
    }
    
    [Test]
    public void ToString_DiagonalWithCoords()
    {
        var selector = new EntitySelector(EntitySelectorKind.AllPlayers)
        {
            Position = new Vector3D(25d, 100d, 1032d),
            DiagonalRange = new Vector3D(3d, 4d, 5d)
        };

        Assert.That(selector.ToString(), Is.EqualTo("@a[x=25,y=100,z=1032,dx=3,dy=4,dz=5]"));
    }

    [Test]
    public void ToString_GameMode_Exact()
    {
        var selector = new EntitySelector(EntitySelectorKind.AllPlayers);
        selector.GameMode.MatchExact(GameMode.Creative);
        
        Assert.That(selector.ToString(),
            Is.EqualTo("@a[gamemode=creative]"));
    }

    [Test]
    public void ToString_GameMode_Exclude()
    {
        var selector = new EntitySelector(EntitySelectorKind.AllPlayers);
        selector.GameMode.MatchExclude(GameMode.Adventure, GameMode.Spectator);
        
        Assert.That(selector.ToString(),
            Is.EqualTo("@a[gamemode=!adventure,gamemode=!spectator]"));
    }
    
    [Test]
    public void ToString_Name_Exact()
    {
        var selector = new EntitySelector(EntitySelectorKind.AllPlayers)
        {
            Name =
            {
                Include = "Exact_Name"
            }
        };

        Assert.That(selector.ToString(),
            Is.EqualTo("@a[name=Exact_Name]"));
    }

    [Test]
    public void ToString_Name_Exclude()
    {
        var selector = new EntitySelector(EntitySelectorKind.AllPlayers);
        selector.Name.Exclude.Add("Exclude_Me");
        selector.Name.Exclude.Add("Nothing");
        
        Assert.That(selector.ToString(),
            Is.EqualTo("@a[name=!Exclude_Me,name=!Nothing]"));
    }
    
    [Test]
    public void ToString_Scores()
    {
        var selector = new EntitySelector(EntitySelectorKind.AllPlayers)
        {
            Scores =
            {
                { "obj0", new IntegralRange(null, 2) },
                { "obj1", new IntegralRange(null, 15) }
            }
        };

        Assert.That(selector.ToString(), Is.EqualTo("@a[scores={obj0=..2,obj1=..15}]"));
    }

    [Test]
    public void Formatter_Parse_Scores()
    {
        var collection = new ScoreboardRangeCollection();
        EntitySelectorStringFormatter.ParseScores("{obj0=..3,obj1=..250}", collection);
        
        Assert.That(collection.ToString(), Is.EqualTo("{obj0=..3,obj1=..250}"));
    }
    
    [Test]
    public void Formatter_Parse_ThreeDifferentScores()
    {
        var collection = new ScoreboardRangeCollection();
        EntitySelectorStringFormatter.ParseScores("{obj0=..3,obj1=250,obj2=2..}", collection);
        
        Assert.That(collection.ToString(), Is.EqualTo("{obj0=..3,obj1=250,obj2=2..}"));
    }
    
    [Test]
    public void Formatter_Parse_AllScores()
    {
        var collection = new ScoreboardRangeCollection();
        EntitySelectorStringFormatter.ParseScores("{obj0=..3,obj1=250,obj2=2..,obj3=20..25}", collection);
        
        Assert.That(collection.ToString(), Is.EqualTo("{obj0=..3,obj1=250,obj2=2..,obj3=20..25}"));
    }

    [Test]
    public void Formatter_Parse_DistanceRange()
    {
        Assert.That(EntitySelectorStringFormatter.ParseDistanceRange("..150.5"),
            Is.EqualTo(DistanceRange.MatchRange(null, 150.5D)));
    }
    
    [Test]
    public void Formatter_Parse_DistanceRangeBoth()
    {
        Assert.That(EntitySelectorStringFormatter.ParseDistanceRange("22.5..150.5"),
            Is.EqualTo(DistanceRange.MatchRange(22.5D, 150.5D)));
    }
    
    [Test]
    public void Formatter_Parse_DistanceExact()
    {
        Assert.That(EntitySelectorStringFormatter.ParseDistanceRange("111.2"),
            Is.EqualTo(DistanceRange.MatchExact(111.2D)));
    }

    [Test]
    public void ScoreboardValueRange_MinMax()
    {
        Assert.That(EntitySelectorParser.ParseScoresRange("objective", "222..233"),
            Is.EqualTo((IScoreboardRange)new ScoreboardRangeMatch("objective", new IntegralRange(222, 233))));
    }
    
    [Test]
    public void ScoreboardValueRange_Min()
    {
        Assert.That(EntitySelectorParser.ParseScoresRange("objective", "255.."),
            Is.EqualTo((IScoreboardRange)new ScoreboardRangeMatch("objective", new IntegralRange(255, null))));
    }
    
    [Test]
    public void ScoreboardValueRange_Max()
    {
        Assert.That(EntitySelectorParser.ParseScoresRange("objective", "..205"),
            Is.EqualTo((IScoreboardRange)new ScoreboardRangeMatch("objective", new IntegralRange(null, 205))));
    }
    
    [Test]
    public void ScoreboardValueRange_Exact()
    {
        Assert.That(EntitySelectorParser.ParseScoresRange("objective", "377"),
            Is.EqualTo((IScoreboardRange)new ScoreboardExactMatch("objective", 377)));
    }

    [Test]
    public void Parser_Scoreboard()
    {
        var collection = new ScoreboardRangeCollection();
        EntitySelectorParser.ParseScoresValue("{ad=1..,ab=5,ac=..10}", collection);
        
        Assert.That(collection.ToString(),
            Is.EqualTo("{ad=1..,ab=5,ac=..10}"));
    }

    [Test]
    public void Parser_Teams_Include()
    {
        var teams = new TeamSelector();
        EntitySelectorParser.ParseTeamsValue("included", teams);
        
        Assert.That(teams.ToString(), Is.EqualTo("team=included"));
    }
    
    [Test]
    public void Parser_Teams_ExcludesMultiple()
    {
        var teams = new TeamSelector();
        EntitySelectorParser.ParseTeamsValue("!exclude", teams);
        EntitySelectorParser.ParseTeamsValue("!hello", teams);
        
        Assert.That(teams.ToString(), Is.EqualTo("team=!exclude,team=!hello"));
    }

    [Test]
    public void Parser_ParseWithName()
    {
        const string sampleString = "@a[name=SomeName]";

        Assert.That(EntitySelectorStringFormatter.ParseSelector(sampleString).ToString(),
            Is.EqualTo(sampleString));
    }
    
    [Test]
    public void Parser_ParseWithNameExcludes()
    {
        const string sampleString = "@a[name=!SomeNameOne,name=!SomeNameTwo]";

        Assert.That(EntitySelectorStringFormatter.ParseSelector(sampleString).ToString(),
            Is.EqualTo(sampleString));
    }
    
    [Test]
    public void Parser_ParseWithTagExcludes()
    {
        const string sampleString = "@a[tag=!SomeNameOne,tag=!SomeNameTwo]";

        Assert.That(EntitySelectorStringFormatter.ParseSelector(sampleString).ToString(),
            Is.EqualTo(sampleString));
    }
    
    [Test]
    public void Parser_ParseWithTagIncludesAndExcludes()
    {
        const string sampleString = "@a[tag=RealTagZero,tag=RealTagOne,tag=!SomeNameOne,tag=!SomeNameTwo]";

        Assert.That(EntitySelectorStringFormatter.ParseSelector(sampleString).ToString(),
            Is.EqualTo(sampleString));
    }
    
    [Test]
    public void Parser_ParseWithPositionAndDistance()
    {
        const string sampleString = "@a[x=250,y=110,z=2491,distance=..20]";

        Assert.That(EntitySelectorStringFormatter.ParseSelector(sampleString).ToString(),
            Is.EqualTo(sampleString));
    }
    
    [Test]
    public void Parser_ParseWithScores()
    {
        const string sampleString = "@a[x=250,y=110,z=2491,scores={objective0=100,objective1=90..250,objective2=..77,objective3=55..}]";

        Assert.That(EntitySelectorStringFormatter.ParseSelector(sampleString).ToString(),
            Is.EqualTo(sampleString));
    }
    
    [Test]
    public void Parser_ParseWithGameModeExcludes()
    {
        const string sampleString = "@a[gamemode=!adventure,gamemode=!creative]";

        Assert.That(EntitySelectorStringFormatter.ParseSelector(sampleString).ToString(),
            Is.EqualTo(sampleString));
    }
    
    [Test]
    public void Parser_ParseWithGameModeRequirement()
    {
        const string sampleString = "@a[gamemode=adventure]";

        Assert.That(EntitySelectorStringFormatter.ParseSelector(sampleString).ToString(),
            Is.EqualTo(sampleString));
    }
    
    [Test]
    public void Parser_ParseWithLevels()
    {
        const string sampleString = "@a[level=56..100]";

        Assert.That(EntitySelectorStringFormatter.ParseSelector(sampleString).ToString(),
            Is.EqualTo(sampleString));
    }
    
    [Test]
    public void Parser_ParseWithNoArguments()
    {
        const string sampleString = "@a";

        Assert.That(EntitySelectorStringFormatter.ParseSelector(sampleString).ToString(),
            Is.EqualTo(sampleString));
    }
    
    [Test]
    public void Parser_ParseWithXRotationRange()
    {
        const string sampleString = "@a[x_rotation=-180..180]";

        Assert.That(EntitySelectorStringFormatter.ParseSelector(sampleString).ToString(),
            Is.EqualTo(sampleString));
    }
    
    [Test]
    public void Parser_ParseWithYRotationRange()
    {
        const string sampleString = "@a[y_rotation=-180..180]";

        Assert.That(EntitySelectorStringFormatter.ParseSelector(sampleString).ToString(),
            Is.EqualTo(sampleString));
    }
    
    [Test]
    public void Parser_ParseWithType()
    {
        const string sampleString = "@a[type=player]";

        Assert.That(EntitySelectorStringFormatter.ParseSelector(sampleString).ToString(),
            Is.EqualTo("@a[type=minecraft:player]"));
    }
    
    [Test]
    public void Parser_ParseWithTypeComplete()
    {
        const string sampleString = "@a[type=minecraft:pig]";

        Assert.That(EntitySelectorStringFormatter.ParseSelector(sampleString).ToString(),
            Is.EqualTo(sampleString));
    }
    
    [Test]
    public void Parser_ParseWithNbtCondition()
    {
        const string sampleString = "@a[nbt={Condition:1b}]";

        Assert.That(EntitySelectorStringFormatter.ParseSelector(sampleString).ToString(),
            Is.EqualTo(sampleString));
    }
    
    [Test]
    public void Parser_ParseWithMultipleNbtConditions()
    {
        const string sampleString = "@a[nbt={Condition:1b},nbt=!{NonCondition:2b}]";

        Assert.That(EntitySelectorStringFormatter.ParseSelector(sampleString).ToString(),
            Is.EqualTo(sampleString));
    }
}