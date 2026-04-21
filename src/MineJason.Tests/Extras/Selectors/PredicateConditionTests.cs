// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

using MineJason.Extras.Selectors.Matching.Predicates;

namespace MineJason.Tests.Extras.Selectors;

public class PredicateConditionTests
{
    [Fact]
    public void PredicateCondition_Parse_Correct_NotMatch()
    {
        // Arrange
        const string parse = "!custom:predicate";

        // Act
        if (!PredicateCondition.TryParse(parse, out var result))
        {
            Assert.Fail("Check parser; something is wrong");
        }

        // Assert
        Assert.Equal(new PredicateCondition(new ResourceLocation("custom", "predicate"),
            false),
            result);
    }

    [Fact]
    public void PredicateCondition_Parse_Correct_DoMatch()
    {
        // Arrange
        const string parse = "custom:match";

        // Act
        if (!PredicateCondition.TryParse(parse, out var result))
        {
            Assert.Fail("Check parser; something is wrong");
        }

        // Assert
        Assert.Equal(new PredicateCondition(new ResourceLocation("custom", "match"),
                true),
            result);
    }

    [Fact]
    public void PredicateCondition_ToString_DoMatch()
    {
        // Arrange
        var condition = new PredicateCondition(new ResourceLocation("custom", "matcher"),
            true);

        // Act
        var str = condition.ToString();

        Assert.Equal("custom:matcher", str);
    }

    [Fact]
    public void PredicateCondition_ToString_DoNotMatch()
    {
        // Arrange
        var condition = new PredicateCondition(new ResourceLocation("custom", "mismatch"),
            false);

        // Act
        var str = condition.ToString();

        Assert.Equal("!custom:mismatch", str);
    }
}