// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: LGPL-3.0-or-later

namespace MineJason;

using JetBrains.Annotations;
using MineJason.Serialization.TextJson;
using MineJason.Text;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

/// <summary>
/// Represents a text component that displays a literal text.
/// </summary>
[PublicAPI]
[JsonConverter(typeof(TextComponentConverter))]
public sealed record TextChatComponent : ChatComponent, IEquatable<TextChatComponent>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="TextChatComponent"/> class.
    /// </summary>
    public TextChatComponent()
    {
    }

    /// <summary>
    /// Initializes a new instance of <see cref="TextChatComponent"/> with the specified text.
    /// </summary>
    /// <param name="text">The text.</param>
    [SetsRequiredMembers]
    public TextChatComponent(string text)
    {
        Text = text;
    }

    internal TextChatComponent(in TextComponentCreationInfo creationInfo)
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