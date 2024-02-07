namespace MineJason.Tests;

using MineJason.Data.Selectors;

public class SelectorParserTests
{
    [Test]
    public void ParsePairSet()
    {
        Assert.That(EntitySelectorParser.ParsePairSet("scores={a=b,b=c},level=2"),
            Is.EquivalentTo(new List<string>
            {
                "scores={a=b,b=c}",
                "level=2"
            }));
    }

    [Test]
    public void ParseSinglePair()
    {
        EntitySelectorParser.ParsePair("scores={a=b,b=c}", out var name, out var value);
        
        Assert.Multiple(() =>
        {
            Assert.That(name, Is.EqualTo("scores"));
            Assert.That(value, Is.EqualTo("{a=b,b=c}"));
        });
    }
    
    
    [Test]
    public void SetWithDoubleBracing()
    {
        Assert.DoesNotThrow(() =>
        {
            EntitySelectorParser.ParsePairSet("advancements={adventure/kill_all_mobs={witch=true}}");
        });
    }
    
    [Test]
    public void SetWithDoubleBracingAndMultiples()
    {
        Assert.DoesNotThrow(() =>
        {
            EntitySelectorParser.ParsePairSet("advancements={adventure/kill_all_mobs={witch=true},adventure/kill_all_mobs={zombie=false}}");
        });
    }
}