// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

namespace MineJason.Serialization.Utilities.Results;

/// <summary>
/// Defines a state representation which may represent a success or a failure state.
/// </summary>
public interface IFailable
{
    /// <summary>
    /// Returns an instance of <see cref="ErrorRecord"/> representing the same error represented by
    /// this instance.
    /// </summary>
    /// <returns>The <see cref="ErrorRecord"/> which represents the same error.</returns>
    /// <exception cref="InvalidOperationException">
    /// The current instance does not represent an error state.
    /// </exception>
    ErrorRecord AsError();

    /// <summary>
    /// Determines whether the current instance indicates successful result.
    /// </summary>
    /// <returns>
    /// <see langword="true"/> if this instance indicates successful result; otherwise,
    /// <see langword="false"/>.
    /// </returns>
    bool IsSuccess();
}
