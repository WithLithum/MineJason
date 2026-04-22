// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using JetBrains.Annotations;
using MineJason.Serialization.TextJson;

namespace MineJason.Text;

/// <summary>
/// Represents a text component that displays literal text. This class cannot be inherited.
/// </summary>
[PublicAPI]
[JsonConverter(typeof(TextComponentConverter))]
public sealed record LiteralTextComponent : TextComponent, IEquatable<LiteralTextComponent>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="LiteralTextComponent"/> class.
    /// </summary>
    public LiteralTextComponent()
    {
    }

    /// <summary>
    /// Initializes a new instance of <see cref="LiteralTextComponent"/> with the specified text.
    /// </summary>
    /// <param name="text">The text.</param>
    [SetsRequiredMembers]
    public LiteralTextComponent(string text)
    {
        Text = text;
    }

    internal LiteralTextComponent(in TextComponentCreationInfo creationInfo)
        : base(creationInfo)
    {
    }

    /// <summary>
    /// Gets the text to display.
    /// </summary>
    public required string Text { get; init; }

    /// <inheritdoc />
    public override string ToString()
    {
        return $"({base.ToString()} | text{{{Text}}})";
    }
}