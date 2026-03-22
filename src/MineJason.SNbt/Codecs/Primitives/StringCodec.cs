// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

using MineJason.SNbt.Parsing;

namespace MineJason.SNbt.Codecs.Primitives;

/// <inheritdoc />
[Obsolete("Use Serialization instead.")]
public class StringCodec : ISNbtCodec<string>
{
    /// <inheritdoc />
    public string Read(SNbtBasicTokenReader reader)
    {
        var type = reader.PeekTokenType();
        // ReSharper disable once SwitchExpressionHandlesSomeKnownEnumValuesWithExceptionInDefault
        // Other arms are prohibited here
        return type switch
        {
            BasicTokenType.UnquotedWord => reader.ReadUnquoteWord().Value,
            BasicTokenType.DoubleQuotedString => reader.ReadDoubleQuotedString(),
            BasicTokenType.SingleQuotedString => reader.ReadSingleQuotedString(),
            _ => throw new FormatException("Expected string token but got something else")
        };
    }

    /// <inheritdoc />
    public void Write(string value, SNbtWriter writer)
    {
        writer.WriteStringValue(value);
    }
}