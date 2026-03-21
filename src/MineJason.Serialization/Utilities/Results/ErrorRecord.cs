// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

using System.Diagnostics.CodeAnalysis;

namespace MineJason.Serialization.Utilities.Results;

/// <summary>
/// Represents an errored operation result.
/// </summary>
/// <remarks>
/// <para>
/// Unlike <see cref="Result"/>, which <i>may</i> represent a successful state, this type cannot
/// be used to do so, and as such, is a perfect surrogate for transferring error state.
/// </para>
/// <para>
/// The primary use of this type is to enable ideomatic coding for handing error result.
/// </para>
/// </remarks>
/// <seealso cref="Result"/>
/// <seealso cref="Result{T}"/>
public readonly record struct ErrorRecord
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ErrorRecord"/> struct.
    /// </summary>
    /// <param name="message">The message.</param>
    [SetsRequiredMembers]
    public ErrorRecord(string message)
    {
        Message = message;
    }

    /// <summary>
    /// Gets the error message.
    /// </summary>
    public required string Message { get; init; }
}
