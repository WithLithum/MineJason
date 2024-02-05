// Copyright (c) WithLithum & contributors 2023-2024. All rights reserved.
// Licensed under the GNU Lesser General Public License, either version 3 or
// (at your opinion) any later version.

namespace MineJason.Tests.Models;

using MineJason.Data.Selectors.Advancements;

public class AdvancementConditionTests
{
    [Test]
    public void CriterionRule_Parse_Valid()
    {
        Assert.Multiple(() =>
        {
            Assert.That(CriterionRule.TryParse("criterion=true", out var result));
            Assert.That(result, Is.EqualTo(new CriterionRule("criterion", true)));
        });
    }
    
    [Test]
    public void CriterionRule_Parse_CaseSensitiveValue()
    {
        Assert.Multiple(() =>
        {
            Assert.That(!CriterionRule.TryParse("criterion=TrUe", out _));
        });
    }

    [Test]
    public void CriterionCondition_Parse_SuccessOnValid()
    {
        Assert.That(CriterionAdvancementCondition.TryParse("rigid=true,none=false", out _));
    }
    
    [Test]
    public void CriterionCondition_Parse_FailOnInvalid()
    {
        Assert.That(!CriterionAdvancementCondition.TryParse("rigid=true,,none=false", out _));
    }
    
    [Test]
    public void CriterionCondition_Parse_ValidValue()
    {
        const string parseValue = "rigid=true,none=false";
        
        if (!CriterionAdvancementCondition.TryParse(parseValue, out var result))
        {
            Assert.Fail("Parsing failed utterly. Check parser");
            return;
        }
        
        Assert.That(result.ToString(),
            Is.EqualTo(parseValue));
    }
}