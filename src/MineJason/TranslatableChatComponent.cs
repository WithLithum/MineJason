// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: LGPL-3.0-or-later

namespace MineJason;

using JetBrains.Annotations;
using MineJason.Serialization.TextJson;
using MineJason.Text;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

/// <summary>
/// Represents a chat component that displays a translatable text.
/// </summary>
[PublicAPI]
[JsonConverter(typeof(TextComponentConverter))]
public sealed record TranslatableChatComponent : ChatComponent, IEquatable<TranslatableChatComponent>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="TranslatableChatComponent"/> class.
    /// </summary>
    public TranslatableChatComponent()
    {
    }

    /// <summary>
    /// Initializes a new instance of <see cref="TranslatableChatComponent"/> with the specified
    /// translation key, fallback text and arguments.
    /// </summary>
    /// <param name="translate">The translation key.</param>
    /// <param name="fallback">The text to display in case of the translation key is missing.</param>
    /// <param name="with">The arguments of the translation.</param>
    [SetsRequiredMembers]
    public TranslatableChatComponent(string translate,
        string? fallback = null,
        IReadOnlyList<ChatComponent>? with = null)
    {
        Translate = translate;
        Fallback = fallback;
        With = with;
    }

    internal TranslatableChatComponent(in TextComponentCreationInfo creationInfo)
        : base(creationInfo)
    {
    }

    /// <summary>
    /// Gets the translation key.
    /// </summary>
    public required string Translate { get; init; }

    /// <summary>
    /// Gets the fallback text.
    /// </summary>
    public string? Fallback { get; init; }

    /// <summary>
    /// Gets the arguments.
    /// </summary>
    public IReadOnlyList<ChatComponent>? With { get; init; }
}