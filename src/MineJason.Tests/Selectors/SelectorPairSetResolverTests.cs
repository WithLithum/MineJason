namespace MineJason.Tests.Selectors;

using MineJason.Data.Selectors;

public class SelectorPairSetResolverTests
{
    [Test]
    public void Resolver_Success_FourBraces()
    {
        var resolver = new EntitySelectorPairSetResolver("key={{{{Value}}}}");

        Assert.DoesNotThrow(() => resolver.RunToEnd());
    }
}
