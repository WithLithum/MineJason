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
        var selector = new EntitySelector(EntitySelectorKind.AllPlayers)
        {
            GameMode = GameModeMatch.MatchExact(GameMode.Creative)
        };
        
        Assert.That(selector.ToString(),
            Is.EqualTo("@a[gamemode=creative]"));
    }

    [Test]
    public void ToString_GameMode_Exclude()
    {
        var selector = new EntitySelector(EntitySelectorKind.AllPlayers)
        {
            GameMode = GameModeMatch.MatchExclude(GameMode.Adventure, GameMode.Spectator)
        };
        
        Assert.That(selector.ToString(),
            Is.EqualTo("@a[gamemode=!adventure,gamemode=!spectator]"));
    }
    
    [Test]
    public void ToString_Name_Exact()
    {
        var selector = new EntitySelector(EntitySelectorKind.AllPlayers)
        {
            Name = NameMatch.MatchExact("Exact_Name")
        };
        
        Assert.That(selector.ToString(),
            Is.EqualTo("@a[name=Exact_Name]"));
    }

    [Test]
    public void ToString_Name_Exclude()
    {
        var selector = new EntitySelector(EntitySelectorKind.AllPlayers)
        {
            Name = NameMatch.MatchExclude("Exclude_Me", "Nothing")
        };
        
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
}