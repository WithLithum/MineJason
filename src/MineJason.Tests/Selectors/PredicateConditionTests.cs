// Copyright (c) WithLithum & contributors 2023-2024. All rights reserved.
// Licensed under the GNU Lesser General Public License, either version 3 or
// (at your opinion) any later version.

namespace MineJason.Tests.Selectors;

using MineJason.Data.Selectors.Predicates;

public class PredicateConditionTests
{
    [Test]
    public void PredicateCondition_Parse_Correct_NotMatch()
    {
        if (!PredicateCondition.TryParse("!custom:predicate", out var result))
        {
            Assert.Fail("Check parser; something is wrong");
        }

        Assert.That(result, Is.EqualTo(new PredicateCondition(new ResourceLocation("custom", "predicate"),
            false)));
    }
    
    [Test]
    public void PredicateCondition_Parse_Correct_DoMatch()
    {
        if (!PredicateCondition.TryParse("custom:match", out var result))
        {
            Assert.Fail("Check parser; something is wrong");
        }

        Assert.That(result, Is.EqualTo(new PredicateCondition(new ResourceLocation("custom", "match"),
            true)));
    }

    [Test]
    public void PredicateCondition_ToString_DoMatch()
    {
        var condition = new PredicateCondition(new ResourceLocation("custom", "matcher"),
            true);
        
        Assert.That(condition.ToString(),
            Is.EqualTo("custom:matcher"));
    }
    
    [Test]
    public void PredicateCondition_ToString_DoNotMatch()
    {
        var condition = new PredicateCondition(new ResourceLocation("custom", "mismatch"),
            false);
        
        Assert.That(condition.ToString(),
            Is.EqualTo("!custom:mismatch"));
    }
}