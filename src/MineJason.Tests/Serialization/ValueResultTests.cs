// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

using MineJason.Serialization.Utilities.Results;
using MineJason.Tests.Serialization.Utilities;

namespace MineJason.Tests.Serialization;

public class ValueResultTests
{
    [Fact]
    public void Value_FailureResult_IsNull()
    {
        // Arrange
        var subject = Result.Failure<string>("This is an error");
        
        // Act
        var value = subject.Value;
        
        // Assert
        Assert.Null(value);
    }
    
    [Fact]
    public void Value_SuccessResult_NotNull()
    {
        // Arrange
        var subject = Result.Success("Successful value!");
        
        // Act
        var value = subject.Value;
        
        // Assert
        Assert.NotNull(value);
    }
    
    [Fact]
    public void Success_Invoke_ReturnsSuccessResult()
    {
        // Act
        var result = Result.Success("Any value");

        // Assert
        Assert.Null(result.Error);
    }

    [Fact]
    public void Failure_Invoke_ReturnsFailureResult()
    {
        // Act
        var result = Result.Failure<string>("Any reason");

        // Assert
        Assert.NotNull(result.Error);
    }

    [Fact]
    public void Failure_NullReason_Throws()
    {
        // Arrange
        string? input = null;

        // Act
        var exception = Record.Exception(() => Result.Failure<string>(input!));

        // Assert
        Assert.IsType<ArgumentNullException>(exception);
    }

    [Fact]
    public void ConvertToVoid_SuccessResult_ReturnsSuccessful()
    {
        // Arrange
        var subject = Result.Success(123);

        // Act
        var result = (Result)subject;

        // Assert
        ResultAssert.Success(result);
    }

    [Fact]
    public void ConvertToVoid_ErroredResult_ReturnsErrored()
    {
        // Arrange
        var subject = Result.Failure<int>("This is error!");

        // Act
        var result = (Result)subject;

        // Assert
        ResultAssert.Failure(result);
    }

    [Fact]
    public void AsError_SuccessState_Throws()
    {
        // Arrange
        var subject = Result.Success(123);

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
        var subject = Result.Failure<int>(message);

        // Act
        var result = subject.AsError();

        // Assert
        Assert.Equal(message, result.Message);
    }

    [Fact]
    public void FromError_Instance_Succeed()
    {
        // Arrange
        const string message = "This is a record!";
        var subject = new ErrorRecord(message);

        // Act
        var result = (Result<string>)subject;

        // Assert
        Assert.Equal(message, result.Error);
    }

    [Fact]
    public void IsSuccess_ErrorIsNull_ReturnsTrue()
    {
        // Arrange
        Result<string> value = "Successful value!";

        // Act
        var result = value.IsSuccess();

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void IsSuccess_ErrorNotNull_ReturnsFalse()
    {
        // Arrange
        Result<string> value = Errors.NullDisallowed;

        // Act
        var result = value.IsSuccess();

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void IsSuccessWithValue_ErrorIsNull_ReturnsTrueAndValue()
    {
        // Arrange
        Result<string> value = "Some value";

        // Act
        var result = value.IsSuccess(out var output);

        // Assert
        Assert.True(result);
        Assert.Equal("Some value", output);
    }

    [Fact]
    public void IsSuccessWithValue_ErrorNotNull_ReturnsFalse()
    {
        // Arrange
        Result<string> value = Errors.NullDisallowed;

        // Act
        var result = value.IsSuccess(out var output);

        // Assert
        Assert.False(result);
        Assert.Null(output);
    }

    [Fact]
    public void Deconstruct_Success_ReturnsTrueAndProvideValue()
    {
        // Arrange
        const string input = "This is a value";
        var subject = Result.Success(input);

        // Act
        var flag = subject.Deconstruct(out var resultValue, out var resultStatus);

        // Assert
        Assert.Multiple(() => Assert.True(flag),
            () => Assert.Equal(input, resultValue),
            () => Assert.True(resultStatus.IsSuccess()));
    }

    [Fact]
    public void Deconstruct_Failure_ReturnsFalseAndProvideError()
    {
        // Arrange
        const string message = "This is a value";
        var subject = Result.Failure<string>(message);

        // Act
        var flag = subject.Deconstruct(out var resultValue, out var resultStatus);

        // Assert
        Assert.Multiple(() => Assert.False(flag),
            () => Assert.Null(resultValue),
            () => Assert.Equal(message, resultStatus.Error));
    }

    [Fact]
    public void BoolOperator_ErrorState_ReturnsFalse()
    {
        // Arrange
        var subject = Result.Failure<string>("This is an error");

        // Act
        bool result = subject;

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void BoolOperator_SuccessState_ReturnsTrue()
    {
        // Arrange
        var subject = Result.Success(32);

        // Act
        bool result = subject;

        // Assert
        Assert.True(result);
    }
}
