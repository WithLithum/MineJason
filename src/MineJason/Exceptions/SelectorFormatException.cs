// Copyright (c) WithLithum & contributors 2023-2024. All rights reserved.
// Licensed under the GNU Lesser General Public License, either version 3 or
// (at your opinion) any later version.

namespace MineJason.Exceptions;

using System.Diagnostics.CodeAnalysis;
using System.Text;
using JetBrains.Annotations;
using MineJason.Data.Selectors;

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

    /// <inheritdoc/>
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

    internal static SelectorFormatException ExitBraceWhileOutside(EntitySelectorPairSetResolver resolver)
    {
        return new SelectorFormatException("Cannot exit brace while outside of brace.", resolver.From);
    }

    internal static SelectorFormatException BraceInvalidInKeys(EntitySelectorPairSetResolver resolver)
    {
        return new SelectorFormatException("Cannot specify braces in the 'key' part of a pair.", resolver.From);
    }

    internal static SelectorFormatException EqualSignWhileInValue(EntitySelectorPairSetResolver resolver)
    {
        return new SelectorFormatException("Equal sign is invalid while inside values", resolver.From);
    }

    internal static SelectorFormatException InvalidCharacter(EntitySelectorPairSetResolver resolver, char offendingChar)
    {
        return new SelectorFormatException($"Invalid character: {offendingChar} at position {resolver.Cursor}", resolver.From);
    }

    internal static SelectorFormatException InvalidPredicateCondition(string value)
    {
        return new SelectorFormatException("Invalid predicate condition notation.", value);
    }
}