// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

using MineJason.Extras.Selectors.Matching.Advancements;

namespace MineJason.Tests.Extras.Selectors;

public class AdvancementConditionTests
{
    [Fact]
    public void CriterionRule_Parse_Valid()
    {
        // Arrange
        const string parse = "criterion=true";

        // Act
        var pass = CriterionRule.TryParse(parse, out var result);

        // Assert
        Assert.True(pass);
        Assert.Equal(new CriterionRule("criterion", true), result);
    }

    [Fact]
    public void CriterionRule_Parse_CaseSensitiveValue()
    {
        // Arrange
        const string parse = "criterion=TrUe";

        // Act
        var pass = CriterionRule.TryParse(parse, out var result);

        // Assert
        Assert.False(pass);
    }

    [Fact]
    public void CriterionCondition_Parse_SuccessOnValid()
    {
        // Arrange
        const string parse = "{rigid=true,none=false}";

        // Act
        var pass = CriterionAdvancementCondition.TryParse(parse, out _);

        // Assert
        Assert.True(pass);
    }

    [Fact]
    public void CriterionCondition_Parse_FailOnInvalid()
    {
        // Arrange
        const string parse = "{rigid=true,,none=false}";

        // Act
        var pass = CriterionAdvancementCondition.TryParse(parse, out _);

        // Assert
        Assert.False(pass);
    }

    [Fact]
    public void CriterionCondition_Parse_ValidValue()
    {
        // Arrange
        const string parseValue = "{rigid=true,none=false}";

        // Act
        var passed = CriterionAdvancementCondition.TryParse(parseValue, out var result);

        // Assert
        Assert.True(passed);
        Assert.Equal(parseValue, result?.ToString());
    }
}