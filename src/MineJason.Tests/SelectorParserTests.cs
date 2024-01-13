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
}