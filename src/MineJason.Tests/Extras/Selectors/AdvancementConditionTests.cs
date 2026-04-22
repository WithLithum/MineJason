// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

using MineJason.Extras.Selectors.Matching.Advancements;

namespace MineJason.Tests.Extras.Selectors;

public class AdvancementConditionTests
{
    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void BoolConstructor_BooleanValue_InitializeCorrectly(bool input)
    {
        // Act
        var result = new BooleanAdvancementCondition(input);

        // Assert
        Assert.Equal(input, result.Value);
    }

    [Fact]
    public void BoolInversion_TrueMatch_ReturnsFalseMatch()
    {
        // Arrange
        var input = new BooleanAdvancementCondition(true);

        // Act
        var inverted = !input;

        // Assert
        Assert.False(inverted.Value);
    }

    [Fact]
    public void BoolEquals_SameValues_ReturnsTrue()
    {
        // Arrange
        var a = new BooleanAdvancementCondition(true);
        var b = new BooleanAdvancementCondition(true);

        // Act
        var result = a.Equals(b);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void BoolEquals_DifferentValues_ReturnsTrue()
    {
        // Arrange
        var a = new BooleanAdvancementCondition(true);
        var b = new BooleanAdvancementCondition(false);

        // Act
        var result = a.Equals(b);

        // Assert
        Assert.False(result);
    }

    [Theory]
    [InlineData(true, "true")]
    [InlineData(false, "false")]
    public void BoolToString_GivenValue_FormatsCorrectly(bool value, string expected)
    {
        // Arrange
        var condition = new BooleanAdvancementCondition(value);

        // Act
        var result = condition.ToString();

        // Assert
        Assert.Equal(expected, result);
    }

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
        var pass = CriterionRule.TryParse(parse, out var _);

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