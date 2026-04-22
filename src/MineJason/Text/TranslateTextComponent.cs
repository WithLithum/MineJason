// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using JetBrains.Annotations;
using MineJason.Serialization.TextJson;

namespace MineJason.Text;

/// <summary>
/// Represents a text component that resolves to the translated text of the specified translation
/// key.
/// </summary>
/// <remarks>
/// <para>
/// Translation resolution happens on the client, and a resource pack is required to translate the
/// text if the translation key is not present in the vanilla game.
/// </para>
/// <para>
/// For this reason, it is wise to use the <see cref="Fallback"/> property to provide a fallback
/// text, if your translation key is not a vanilla key.
/// </para>
/// </remarks>
[PublicAPI]
[JsonConverter(typeof(TextComponentConverter))]
public sealed record TranslateTextComponent : TextComponent, IEquatable<TranslateTextComponent>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="TranslateTextComponent"/> class.
    /// </summary>
    public TranslateTextComponent()
    {
    }

    /// <summary>
    /// Initializes a new instance of <see cref="TranslateTextComponent"/> with the specified
    /// translation key, fallback text and arguments.
    /// </summary>
    /// <param name="translate">The translation key.</param>
    /// <param name="fallback">The text to display in case of the translation key is missing.</param>
    /// <param name="with">The arguments of the translation.</param>
    [SetsRequiredMembers]
    public TranslateTextComponent(string translate,
        string? fallback = null,
        IReadOnlyList<TextComponent>? with = null)
    {
        Translate = translate;
        Fallback = fallback;
        With = with;
    }

    internal TranslateTextComponent(in TextComponentCreationInfo creationInfo)
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
    public IReadOnlyList<TextComponent>? With { get; init; }
}