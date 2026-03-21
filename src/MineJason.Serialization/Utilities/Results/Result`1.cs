// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

namespace MineJason.Serialization.Utilities.Results;

using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

/// <summary>
/// Encapsulates a result value, or an error reason.
/// </summary>
/// <remarks>
/// <para>
/// A <see cref="Result{T}"/> may represent two states: a <i>success</i> state, and an
/// <i>errored</i> state.
/// </para>
/// <para>
/// Instances representing the <i>errored</i> state will have a non-<see langword="null" />
/// <see cref="Error"/> and the <see cref="Value"/> value will be absent; those representing a
/// <i>success</i> state, in reverse, will have a <see cref="Value"/> value and a
/// <see langword="null"/> <see cref="Error"/> value.
/// </para>
/// <para>
/// Callers should always treat <see cref="Result{T}"/> instances with a <see cref="Error"/>
/// value as an instance in <i>errored</i> state and should presume that the <see cref="Value"/>
/// value is unwise to use.
/// </para>
/// <para>
/// <see cref="Result{T}"/> instances should not be used to report invalid usages of API, but
/// only for malformed or invalid data. An appropriate exception should be thrown if the cause of
/// failure to encode or decode was due to a malfunctioning decoder or improper API usage.
/// </para>
/// </remarks>
/// <typeparam name="T">The type of the result value.</typeparam>
/// <seealso cref="Result"/>
public readonly record struct Result<T> : IFailable
{
    private Result(string? error, T? result)
    {
        Error = error;
        Value = result;
    }

    /// <summary>
    /// Gets the error message.
    /// </summary>
    /// <value>
    /// The error message. If <see langword="null"/>, the operation has completed successfully.
    /// </value>
    public string? Error { get; }

    /// <summary>
    /// Gets the associated result value.
    /// </summary>
    /// <remarks>
    /// <para>
    /// If <see cref="Error"/> is not <see langword="null"/>, do not use the value of this property
    /// even if it is not <see langword="null"/> (that is, when <see cref="IsSuccess()"/> returns
    /// <see langword="false"/>).
    /// </para>
    /// <para>
    /// A convinence method, <see cref="IsSuccess(out T?)"/>, is provided for ideomatic and safe
    /// retrival of the value.
    /// </para>
    /// </remarks>
    /// <value>
    /// The result value. This value is <i>usually</i> <see langword="null"/> if
    /// <see cref="Error"/> is not <see langword="null"/>.
    /// </value>
    public T? Value { get; }

    /// <inheritdoc />
    public bool IsSuccess()
    {
        return Error == null;
    }

    /// <summary>
    /// Determines whether the current instance indicates successful result. If it does, provides
    /// the result value.
    /// </summary>
    /// <param name="result">The result value.</param>
    /// <returns>
    /// <see langword="true"/> if <see cref="Error"/> is <see langword="null"/>; otherwise,
    /// <see langword="false"/>.
    /// </returns>
    public bool IsSuccess([NotNullWhen(true)] out T? result)
    {
        if (Error != null)
        {
            result = default;
            return false;
        }

        result = Value!;
        return true;
    }

    /// <summary>
    /// Deconstructs this instance into a result value and a value-less <see cref="Result"/>.
    /// </summary>
    /// <param name="result">The result, or <see langword="null"/> if this instance indicates failure.</param>
    /// <param name="status">The status.</param>
    /// <returns>
    /// <see langword="true"/> if <see cref="Error"/> is <see langword="null"/>; otherwise,
    /// <see langword="false"/>.
    /// </returns>
    public bool Deconstruct([NotNullWhen(true)] out T? result,
        out Result status)
    {
        var success = Error == null;
        status = (Result)this;

        result = success ? Value : default;
        return success;
    }

    /// <inheritdoc />
    public ErrorRecord AsError()
    {
        if (Error == null)
        {
            throw new InvalidOperationException("This DataResult does not represent an error state.");
        }

        return new ErrorRecord(Error);
    }

    /// <summary>
    /// Executes and returns the result of the specific map function if this instance represents
    /// a successfu result.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Inline (lambda) closures are instantiated every time it is executed, which comes with an
    /// overhead and heap allocation. In most cases, it is recommended to use
    /// <see cref="IsSuccess(out T?)"/> for ideomatic value retrival.
    /// </para>
    /// </remarks>
    /// <param name="func">The mapping function.</param>
    /// <returns>
    /// If this instance indicate success, the result from executing the specific mapping function;
    /// or, if this instance indicates failure, returns the current instance.
    /// </returns>
    public Result<T> Map(Func<T, Result<T>> func)
    {
        if (Error != null)
        {
            return this;
        }

        return func.Invoke(Value!);
    }

    /// <summary>
    /// Executes and returns the result of the specific map function if this instance represents
    /// a successfu result.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Inline (lambda) closures are instantiated every time it is executed, which comes with an
    /// overhead and heap allocation. In most cases, it is recommended to use
    /// <see cref="IsSuccess(out T?)"/> for ideomatic value retrival.
    /// </para>
    /// </remarks>
    /// <typeparam name="TOut">The type of the resulting value.</typeparam>
    /// <param name="func">The mapping function.</param>
    /// <returns>
    /// If this instance indicate success, the result from executing the specific mapping function;
    /// or, if this instance indicates failure, a new instance of <see cref="Result{T}"/> that
    /// represents the same failure state.
    /// </returns>
    public Result<TOut> Map<TOut>(Func<T, Result<TOut>> func)
    {
        if (Error != null)
        {
            return AsError();
        }

        return func.Invoke(Value!);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static Result<T> CreateSuccessInternal(T value)
    {
        return new Result<T>(null, value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static Result<T> CreateFailureInternal(string message)
    {
        return new Result<T>(message, default);
    }

    /// <summary>
    /// Converts the specified value to a successful <see cref="Result{T}"/> containing the
    /// specified value.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>The conversion result.</returns>
    public static implicit operator Result<T>(T value)
    {
        return new Result<T>(null, value);
    }

    /// <summary>
    /// Implicitly converts the current instance to a new <see cref="Result{T}"/> of the
    /// <see cref="object"/> type.
    /// </summary>
    /// <param name="value">The result to convert from.</param>
    public static implicit operator Result<object>(Result<T> value)
    {
        return new Result<object>(value.Error, value.Value);
    }

    /// <summary>
    /// Implicitly converts the specified <see cref="ErrorRecord"/> to an error-representing
    /// <see cref="Result{T}"/>.
    /// </summary>
    /// <param name="error">The error to convert from.</param>
    public static implicit operator Result<T>(in ErrorRecord error)
    {
        return new Result<T>(error.Message, default);
    }

    /// <summary>
    /// Converts the current instance to an instance of <see cref="Result"/>, representing the same
    /// state but without the value.
    /// </summary>
    /// <param name="result">The instance to convert.</param>
    public static explicit operator Result(in Result<T> result)
    {
        return !result.IsSuccess() ? Result.Failure(result.Error!) : Result.Success();
    }

    /// <summary>
    /// Returns whether the specified <see cref="Result{T}"/> was successful.
    /// </summary>
    /// <param name="result">The result.</param>
    public static implicit operator bool(Result<T> result)
    {
        return result.Error == null;
    }

    /// <summary>
    /// Returns whether the specified <see cref="Result{T}"/> was errored.
    /// </summary>
    /// <param name="result">The result.</param>
    /// <returns><see langword="true"/> if the result was successful; otherwise, <see langword="false"/>.</returns>
    public static bool operator !(Result<T> result)
    {
        return result.Error != null;
    }
}