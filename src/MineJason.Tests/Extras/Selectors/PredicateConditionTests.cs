// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

using MineJason.Extras.Selectors.Matching.Predicates;

namespace MineJason.Tests.Extras.Selectors;

public class PredicateConditionTests
{
    [Fact]
    public void Parse_EmptyString_Fails()
    {
        // Arrange
        const string input = "";

        // Act
        var success = PredicateCondition.TryParse(input, out _);

        // Assert
        Assert.False(success);
    }

    [Fact]
    public void Parse_InvalidResourceLocation_Fails()
    {
        // Arrange
        const string input = "pre/fix:invalid_path";

        // Act
        var success = PredicateCondition.TryParse(input, out _);

        // Assert
        Assert.False(success);
    }

    [Fact]
    public void Parse_ExclamationPrefixed_NoMatch()
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
    public void Parse_PredicateId_DoMatch()
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
    public void ToString_DoMatch_ReturnsIdAsIs()
    {
        // Arrange
        var condition = new PredicateCondition(new ResourceLocation("custom", "matcher"),
            true);

        // Act
        var str = condition.ToString();

        Assert.Equal("custom:matcher", str);
    }

    [Fact]
    public void ToString_NoMatch_PrefixExclamationMark()
    {
        // Arrange
        var condition = new PredicateCondition(new ResourceLocation("custom", "mismatch"),
            false);

        // Act
        var str = condition.ToString();

        Assert.Equal("!custom:mismatch", str);
    }

    [Fact]
    public void Inversion_Instance_InvertsMatch()
    {
        // Arrange
        var subject = new PredicateCondition(new ResourceLocation("foo", "bar"),
            match: false);

        // Act
        var result = !subject;

        // Assert
        Assert.Multiple(() => Assert.Equal(subject.Predicate, result.Predicate),
            () => Assert.True(result.Match));
    }

    [Fact]
    public void Equals_SameValues_ReturnsTrue()
    {
        // Arrange
        var a = new PredicateCondition(
            predicate: new ResourceLocation("example", "predicate"),
            match: true);
        var b = new PredicateCondition(
            predicate: new ResourceLocation("example", "predicate"),
            match: true);

        // Act
        var result = a.Equals(b);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void Equals_DifferentPredicate_ReturnsFalse()
    {
        // Arrange
        var a = new PredicateCondition(
            predicate: new ResourceLocation("example", "predicate"),
            match: true);
        var b = new PredicateCondition(
            predicate: new ResourceLocation("example", "different"),
            match: true);

        // Act
        var result = a.Equals(b);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void Equals_DifferentMatch_ReturnsFalse()
    {
        // Arrange
        var a = new PredicateCondition(
            predicate: new ResourceLocation("example", "predicate"),
            match: true);
        var b = new PredicateCondition(
            predicate: new ResourceLocation("example", "predicate"),
            match: false);

        // Act
        var result = a.Equals(b);

        // Assert
        Assert.False(result);
    }
}