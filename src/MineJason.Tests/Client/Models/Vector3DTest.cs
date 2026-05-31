// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

using System.Numerics;
using MineJason.Data;

namespace MineJason.Tests.Client.Models;

public class Vector3DTest
{
    [Fact]
    public void Constructor_Vector3_SameValues()
    {
        // Arrange
        var input = new Vector3(1f, 1f, 1f);

        // Act
        var result = new Vector3D(input);

        // Assert
        Assert.Multiple(
            () => Assert.Equal(1d, result.X),
            () => Assert.Equal(1d, result.Y),
            () => Assert.Equal(1d, result.Z));
    }

    [Fact]
    public void Constructor_SingleValue_FillsAll()
    {
        // Arrange
        const double input = 1.25d;

        // Act
        var result = new Vector3D(input);

        // Assert
        Assert.Multiple(
            () => Assert.Equal(input, result.X),
            () => Assert.Equal(input, result.Y),
            () => Assert.Equal(input, result.Z));
    }

    [Fact]
    public void Equals_SameValues_ReturnsTrue()
    {
        // Arrange
        var a = new Vector3D(1.0, 2.0, 3.0);
        var b = new Vector3D(1.0, 2.0, 3.0);

        // Act
        var result = a.Equals(b);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void Equals_DifferentValues_ReturnsFalse()
    {
        // Arrange
        var a = new Vector3D(1.0, 2.0, 3.0);
        var b = new Vector3D(4.0, 5.0, 6.0);

        // Act
        var result = a.Equals(b);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void Equals_DifferentSingleValues_ReturnsTrue()
    {
        // Arrange
        var a = new Vector3D(1.0, 2.0, 3.0);
        var b = new Vector3(4f, 5f, 6f);

        // Act
        var result = a.Equals(b);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void Equals_SameSingleValues_ReturnsTrue()
    {
        // Arrange
        var a = new Vector3D(1.0, 2.0, 3.0);
        var b = new Vector3(1f, 2f, 3f);

        // Act
        var result = a.Equals(b);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void OpEquality_SameValues_True()
    {
        // Arrange
        var a = new Vector3D(3d, 5d, 7d);
        var b = new Vector3D(3d, 5d, 7d);

        // Act
        var result = a == b;

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void OpEquality_DifferentValues_False()
    {
        // Arrange
        var a = new Vector3D(3d, 5d, 7d);
        var b = new Vector3D(300d, 500d, 700d);

        // Act
        var result = a == b;

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void OpInequality_DifferentValues_True()
    {
        // Arrange
        var a = new Vector3D(3d, 5d, 7d);
        var b = new Vector3D(300d, 500d, 700d);

        // Act
        var result = a != b;

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void OpInequality_SameValues_False()
    {
        // Arrange
        var a = new Vector3D(3d, 5d, 7d);
        var b = new Vector3D(3d, 5d, 7d);

        // Act
        var result = a != b;

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void Operator_Add_Correct()
    {
        // Arrange
        var vector = new Vector3D(3d, 5d, 7d);
        var other = new Vector3D(1d, 2d, 3d);

        // Act
        var result = Vector3D.Add(vector, other);

        // Assert
        Assert.Multiple(
            () => Assert.Equal(4d, result.X),
            () => Assert.Equal(7d, result.Y),
            () => Assert.Equal(10d, result.Z));
    }

    [Fact]
    public void Operator_Subtract_Correct()
    {
        // Arrange
        var vector = new Vector3D(10d, 11d, 12d);
        var other = new Vector3D(5d, 1d, 6d);

        // Act
        var result = Vector3D.Subtract(vector, other);

        // Assert
        Assert.Multiple(
            () => Assert.Equal(5d, result.X),
            () => Assert.Equal(10d, result.Y),
            () => Assert.Equal(6d, result.Z));
    }

    [Fact]
    public void Operator_MultiplyNumber_Correct()
    {
        // Arrange
        var vector = new Vector3D(10d, 20d, 30d);
        const double input = 2d;

        // Act
        var result = vector * input;

        // Assert
        Assert.Multiple(
            () => Assert.Equal(20d, result.X),
            () => Assert.Equal(40d, result.Y),
            () => Assert.Equal(60d, result.Z));
    }

    [Fact]
    public void Operator_MultiplyVector_Correct()
    {
        // Arrange
        var vector = new Vector3D(10d, 20d, 2d);
        var other = new Vector3D(2d, 5d, 6d);

        // Act
        var result = Vector3D.Multiply(vector, other);

        // Assert
        Assert.Multiple(
            () => Assert.Equal(20d, result.X),
            () => Assert.Equal(100d, result.Y),
            () => Assert.Equal(12d, result.Z));
    }

    [Fact]
    public void Operator_DivideNumber_Correct()
    {
        // Arrange
        var vector = new Vector3D(10d, 20d, 30d);
        const double input = 10d;

        // Act
        var result = Vector3D.Divide(vector, input);

        // Assert
        Assert.Multiple(
            () => Assert.Equal(1d, result.X),
            () => Assert.Equal(2d, result.Y),
            () => Assert.Equal(3d, result.Z));
    }

    [Fact]
    public void Operator_DivideVector_Correct()
    {
        // Arrange
        var vector = new Vector3D(10d, 20d, 100d);
        var other = new Vector3D(2d, 10d, 2d);

        // Act
        var result = Vector3D.Divide(vector, other);

        // Assert
        Assert.Multiple(
            () => Assert.Equal(5d, result.X),
            () => Assert.Equal(2d, result.Y),
            () => Assert.Equal(50d, result.Z));
    }

    [Fact]
    public void Operator_Negate_Correct()
    {
        // Arrange
        var vector = new Vector3D(1d, 2d, 10d);

        // Act
        var result = Vector3D.Negate(vector);

        // Assert
        Assert.Multiple(
            () => Assert.Equal(-1d, result.X),
            () => Assert.Equal(-2d, result.Y),
            () => Assert.Equal(-10d, result.Z));
    }

    [Fact]
    public void Operator_Clamp_Correct()
    {
        // Arrange
        var vector = new Vector3D(-2d, 20d, 3d);
        var min = new Vector3D(1d);
        var max = new Vector3D(5d);

        // Act
        var result = Vector3D.Clamp(vector, min, max);

        // Assert
        Assert.Multiple(
            () => Assert.Equal(1d, result.X),
            () => Assert.Equal(5d, result.Y),
            () => Assert.Equal(3d, result.Z));
    }

    [Fact]
    public void Min_SmallerAndBigger_LeftWins()
    {
        // Arrange
        var smaller = new Vector3D(5d);
        var bigger = new Vector3D(10d);

        // Act
        var result = Vector3D.Min(smaller, bigger);

        // Assert
        Assert.Equal(smaller, result);
    }

    [Fact]
    public void Min_BiggerAndSmaller_RightWins()
    {
        // Arrange
        var bigger = new Vector3D(10d);
        var smaller = new Vector3D(5d);

        // Act
        var result = Vector3D.Min(bigger, smaller);

        // Assert
        Assert.Equal(smaller, result);
    }

    [Fact]
    public void Max_BiggerAndSmaller_LeftWins()
    {
        // Arrange
        var bigger = new Vector3D(10d);
        var smaller = new Vector3D(5d);

        // Act
        var result = Vector3D.Max(bigger, smaller);

        // Assert
        Assert.Equal(bigger, result);
    }

    [Fact]
    public void Max_SmallerAndBigger_RightWins()
    {
        // Arrange
        var smaller = new Vector3D(5d);
        var bigger = new Vector3D(10d);

        // Act
        var result = Vector3D.Max(smaller, bigger);

        // Assert
        Assert.Equal(bigger, result);
    }

    [Fact]
    public void ImplicitCast_FromVector3_SameValues()
    {
        // Arrange
        var input = new Vector3(1f, 1f, 1f);

        // Act
        Vector3D result = input;

        // Assert
        Assert.Multiple(
            () => Assert.Equal(1d, result.X),
            () => Assert.Equal(1d, result.Y),
            () => Assert.Equal(1d, result.Z));
    }
}
