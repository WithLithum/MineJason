// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

namespace MineJason.Serialization.Utilities.Results;

/// <summary>
/// Indicates the result of an operation.
/// </summary>
/// <remarks>
/// <note type="caution">
/// Do not check if <see cref="Result"/> represents a successful result by comparing reference
/// equality. <see cref="Result"/> is a <i>value type</i> and cannot be reference compared.
/// </note>
/// <para>
/// Whether the operation was successful is determined by the absence of an error message. For
/// convenience, <see cref="IsSuccess"/> method can be used instead. 
/// </para>
/// <para>
/// Do not use <see cref="Result"/> for reporting an error that is due to a faulty encoder or
/// decoder, or API misuse. Always throw an exception for these cases.
/// </para>
/// </remarks>
public readonly struct Result : IFailable
{
    private Result(string? error)
    {
        Error = error;
    }

    /// <summary>
    /// Gets the error message. The absence of an error message indicates the operation completed
    /// successfully.
    /// </summary>
    /// <value>
    /// The error message. If <see langword="null"/>, there is no error.
    /// </value>
    public string? Error { get; }

    /// <inheritdoc />
    public bool IsSuccess()
    {
        return Error == null;
    }

    /// <summary>
    /// Returns an instance of <see cref="Result"/> that indicate success.
    /// </summary>
    /// <returns>A successful result.</returns>
    public static Result Success()
    {
        return new Result(null);
    }

    /// <summary>
    /// Returns an instance of <see cref="Result{T}"/>, indicating success and containing the
    /// specified value.
    /// </summary>
    /// <typeparam name="T">The type of the value.</typeparam>
    /// <param name="value">The value.</param>
    /// <returns>The result containing the value.</returns>
    /// <exception cref="ArgumentNullException">The specified value is <see langword="null"/>.</exception>
    public static Result<T> Success<T>(T value)
    {
        ArgumentNullException.ThrowIfNull(value);

        return Result<T>.CreateSuccessInternal(value);
    }

    /// <summary>
    /// Returns a new instance of <see cref="Result"/> that indicates failure.
    /// </summary>
    /// <param name="message">The message.</param>
    /// <returns>An errored result.</returns>
    /// <exception cref="ArgumentException">The specified message is empty.</exception>
    /// <exception cref="ArgumentNullException">The specified message is <see langword="null"/>.</exception>
    public static Result Failure(string message)
    {
        ArgumentException.ThrowIfNullOrEmpty(message);

        return new Result(message);
    }

    /// <summary>
    /// Returns a new instance of <see cref="Result{T}"/> that indicates failure.
    /// </summary>
    /// <param name="message">The message.</param>
    /// <remarks>
    /// <para>
    /// As of .NET 10, C# <see href="https://github.com/dotnet/csharplang/discussions/92">cannot
    /// infer generic type from target</see>, so most if not all uses of this method requires
    /// manually specifying the type of the result.
    /// </para>
    /// <para>
    /// The recommended method for returning a failure result is to use the <see cref="Errors"/>
    /// utility class which creates <see cref="ErrorRecord"/> instances that can be implicitly
    /// converted to <see cref="Result{T}"/>, without the need for specifying result type.
    /// </para>
    /// </remarks>
    /// <returns>An errored result.</returns>
    /// <exception cref="ArgumentException">The specified message is empty.</exception>
    /// <exception cref="ArgumentNullException">The specified message is <see langword="null"/>.</exception>
    /// <seealso cref="Errors.Error(string)"/>
    public static Result<T> Failure<T>(string message)
    {
        ArgumentException.ThrowIfNullOrEmpty(message);

        return Result<T>.CreateFailureInternal(message);
    }

    /// <inheritdoc />
    public ErrorRecord AsError()
    {
        return new ErrorRecord(Error
            ?? throw new InvalidOperationException("The current instance does not represent error state."));
    }

    /// <summary>
    /// Determines whether the specified <see cref="Result"/> represents a successful result.
    /// </summary>
    /// <param name="value">The result value to check.</param>
    public static implicit operator bool(Result value)
    {
        return value.Error == null;
    }

    /// <summary>
    /// Implicitly converts the specified <see cref="ErrorRecord"/> to a new instance of
    /// <see cref="Result"/> representing the error contained within the former.
    /// </summary>
    /// <param name="error">The error to convert.</param>
    public static implicit operator Result(ErrorRecord error)
    {
        return new Result(error.Message);
    }
}