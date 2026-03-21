// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

namespace MineJason.Serialization.Utilities.Results;

using JetBrains.Annotations;

/// <summary>
/// Helps with the creation of various operation results.
/// </summary>
public static class Errors
{
    /// <summary>
    /// A shared instance of <see cref="Result"/> that indicates the value returned by something
    /// is empty in a circumstance where empty results are disallowed.
    /// </summary>
    public static readonly ErrorRecord EmptyDisallowed = new("Value cannot be empty");

    /// <summary>
    /// A shared instance of <see cref="ErrorRecord"/> that indicates the value returned by something
    /// is <see langword="null"/>, in a circumstance where <see langword="null"/> results are
    /// disallowed.
    /// </summary>
    public static readonly ErrorRecord NullDisallowed = new("Value cannot be null");

    /// <summary>
    /// Returns a new instance of <see cref="Result"/> that contains the specified error message.
    /// </summary>
    /// <param name="reason">The error message.</param>
    /// <returns>The created instance.</returns>
    public static ErrorRecord Error(string reason)
    {
        return new ErrorRecord(reason);
    }

    /// <summary>
    /// Returns a new instance of <see cref="Result"/> that indicates the specified key does not
    /// exist.
    /// </summary>
    /// <param name="key">The key that does not exist.</param>
    /// <returns>The created instance of <see cref="Result"/>.</returns>
    public static ErrorRecord NoSuchKey(string key)
    {
        return new ErrorRecord($"The specified key '{key}' does not exist");
    }

    /// <summary>
    /// Returns a new instance of <see cref="Result"/> that indicates the specified property is
    /// required but missing.
    /// </summary>
    /// <param name="name">The property that is missing.</param>
    /// <returns>The created instance.</returns>
    public static ErrorRecord MissingProperty(string name)
    {
        return new ErrorRecord($"Property '{name}' is required but missing");
    }

    /// <summary>
    /// Returns a new instance of <see cref="Result"/> indicating an error causing by adding
    /// an entry to a key-value collection whilst the key of that entry already exists in the
    /// collection.
    /// </summary>
    /// <param name="key">The key.</param>
    /// <returns>An instance of <see cref="Result"/>.</returns>
    public static ErrorRecord KeyAlreadyExists(string key)
    {
        return new ErrorRecord($"The specified key '{key}' already exists");
    }

    /// <summary>
    /// Returns a new instance of <see cref="Result"/> that indicates a value is of a type that
    /// is not an expected or allowed type.
    /// </summary>
    /// <param name="expected">The expected type.</param>
    /// <param name="actual">The actual type encountered.</param>
    /// <returns>The created instance.</returns>
    [PublicAPI]
    public static ErrorRecord TypeMismatch(string expected, string actual)
    {
        return new ErrorRecord($"Expected '{expected}' but found '{actual}'");
    }

    /// <summary>
    /// Returns a new instance of <see cref="Result"/> that indicates a value is of a type that
    /// is not an expected or allowed type.
    /// </summary>
    /// <param name="expected">The expected type.</param>
    /// <param name="actual">The actual type encountered.</param>
    /// <typeparam name="TEnum">The type of the enum that defines the types.</typeparam>
    /// <returns>The created instance.</returns>
    public static ErrorRecord TypeMismatch<TEnum>(TEnum expected, TEnum actual)
        where TEnum : struct, Enum
    {
        return TypeMismatch(expected.ToString("G"), actual.ToString("G"));
    }

    /// <summary>
    /// Returns a new instance of <see cref="Result"/> that indicates a number is not of an
    /// accepted or expected type.
    /// </summary>
    /// <param name="expectedType">The expected type.</param>
    /// <returns>The created instance.</returns>
    public static ErrorRecord NumberTypeMismatch(string expectedType)
    {
        return new ErrorRecord($"Expected number of type '{expectedType}'");
    }
}