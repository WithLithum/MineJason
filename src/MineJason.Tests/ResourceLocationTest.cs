namespace MineJason.Tests;

public class ResourceLocationTest
{
    [Test]
    public void IsPathValid_EmptyDirectoryName()
    {
        Assert.That(!ResourceLocation.IsPathValid("a//b"));
    }

    [Test]
    public void IsPathValid_OkayDirectory()
    {
        Assert.That(ResourceLocation.IsPathValid("aa/bb/cc"));
    }

    [Test]
    public void IsPathValid_UpperCaseLetterInLowerCase()
    {
        Assert.That(!ResourceLocation.IsPathValid("aa/Bb/cc"));
    }

    [Test]
    public void IsPathValid_AllUpperCase()
    {
        Assert.That(!ResourceLocation.IsPathValid("AA/BB/CC"));
    }

    [Test]
    public void IsPathValid_UnlawfulSymbol()
    {
        Assert.That(!ResourceLocation.IsPathValid("AA/!!/CC"));
    }

    [Test]
    public void IsPathValid_NotBeFullId()
    {
        Assert.That(!ResourceLocation.IsPathValid("minecraft:short_grass"));
    }

    [Test]
    public void IsPathValid_RegularOkayPath()
    {
        Assert.That(ResourceLocation.IsPathValid("gameplay/features_set/oxygen/enable_oxygen"));
    }

    [Test]
    public void IsPathValid_Cyrillic()
    {
        Assert.That(!ResourceLocation.IsPathValid("a/кириллица/b"));
    }

    [Test]
    public void IsPathValid_Chinese()
    {
        Assert.That(!ResourceLocation.IsPathValid("aa/囧/bb"));
        // 这里是要IsPathValid别囧，直接返回false
    }
}