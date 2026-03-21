// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

using MineJason.Serialization.Utilities.Results;

namespace MineJason.Tests.Serialization;

public class VoidResultTests
{
    [Fact]
    public void Success_Invoke_ReturnsSuccessResult()
    {
        // Act
        var result = Result.Success();

        // Assert
        Assert.Null(result.Error);
    }

    [Fact]
    public void Failure_Invoke_ReturnsFailureResult()
    {
        // Act
        var result = Result.Failure("Any reason");

        // Assert
        Assert.NotNull(result.Error);
    }

    [Fact]
    public void Failure_NullReason_Throws()
    {
        // Arrange
        string? input = null;

        // Act
        var exception = Record.Exception(() => Result.Failure(input!));

        // Assert
        Assert.IsType<ArgumentNullException>(exception);
    }

    [Fact]
    public void AsError_SuccessfulState_Throws()
    {
        // Arrange
        var subject = Result.Success();

        // Act
        var exception = Record.Exception(() => subject.AsError());

        // Assert
        Assert.IsType<InvalidOperationException>(exception);
    }

    [Fact]
    public void AsError_FailureState_Succeed()
    {
        // Arrange
        const string message = "This is another error!";
        var subject = Result.Failure(message);

        // Act
        var result = subject.AsError();

        // Assert
        Assert.Equal(message, result.Message);
    }

    [Fact]
    public void FromError_Instance_Succeed()
    {
        // Arrange
        const string message = "This is a type-less record!";
        var subject = new ErrorRecord(message);

        // Act
        var result = (Result)subject;

        // Assert
        Assert.Equal(message, result.Error);
    }

    [Fact]
    public void IsSuccess_ErrorIsNull_ReturnsTrue()
    {
        // Arrange
        var value = Result.Success();

        // Act
        var result = value.IsSuccess();

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void IsSuccess_ErrorNotNull_ReturnsFalse()
    {
        // Arrange
        var value = Result.Failure("This is an error");

        // Act
        var result = value.IsSuccess();

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void BoolOperator_ErrorState_ReturnsFalse()
    {
        // Arrange
        var value = Result.Failure("RUN! IT'S THE BEAST!");

        // Act
        bool result = value;

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void BoolOperator_SuccessState_ReturnsTrue()
    {
        // Arrange
        var value = Result.Success();

        // Act
        bool result = value;

        // Assert
        Assert.True(result);
    }

}
