// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

using MineJason.Extras.Selectors;

namespace MineJason.Tests.Extras.Selectors;

public class SelectorParserTests
{
    [Fact]
    public void ParsePairSet()
    {
        const string parse = "scores={a=b,b=c},level=2";

        // Act
        var result = EntitySelectorParser.ParsePairSet(parse);

        // Assert
        Assert.Equal(["scores={a=b,b=c}",
            "level=2"], result);
    }

    [Theory]
    [InlineData("advancements={adventure/kill_all_mobs={witch=true},adventure/kill_all_mobs={zombie=false}}")]
    [InlineData("advancements={adventure/kill_all_mobs={witch=true}}")]
    [InlineData("scores={a=b,b=c}")]
    public void PairResolver_VariousPairs_ParsesCorrectly(string input)
    {
        // Act
        var pairSet = EntitySelectorParser.ParsePairSet(input);

        // Assert
        Assert.Equal([input], pairSet);
    }
}