// Copyright (c) WithLithum & contributors 2023-2024. All rights reserved.
// Licensed under the GNU Lesser General Public License, either version 3 or
// (at your opinion) any later version.

namespace MineJason.Exceptions;

using System.Diagnostics.CodeAnalysis;
using System.Text;
using JetBrains.Annotations;

/// <summary>
/// An exception thrown when a target selector or a part of a target selector is in an invalid format.
/// </summary>
public class SelectorFormatException : Exception
{
    /// <summary>
    /// Initialises a new instance of the <see cref="SelectorFormatException"/> class.
    /// </summary>
    /// <param name="message">The message.</param>
    /// <param name="value">The value.</param>
    public SelectorFormatException(string message, string value) : base(message)
    {
        Value = value;
    }
    
    [PublicAPI]
    public string? Value { get; }

    [SuppressMessage("", "CA1305")]
    public override string ToString()
    {
        var builder = new StringBuilder();
        if (Value != null)
        {
            builder.Append($"!!! Offending value: {Value}");
            builder.AppendLine();
        }
        builder.AppendLine(base.ToString());
        return builder.ToString();
    }
}