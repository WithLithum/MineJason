// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

using System.Text.Json;
using JetBrains.Annotations;
using MineJason.Serialization.TextJson;
using MineJason.Text;

namespace MineJason.Serialization;

/// <summary>
/// Provides methods to assist with serialization of chat components by using
/// System.Text.Json (in situations where a context is required).
/// </summary>
[PublicAPI]
[Obsolete("Use JsonSerializer instead. If a context is needed, create one with the default source generator.")]
public static class JsonComponentSerializer
{
    /// <summary>
    /// Converts the specified <see cref="TextComponent"/> to its JSON representation.
    /// </summary>
    /// <param name="component">The component to convert from.</param>
    /// <returns>The converted JSON.</returns>
    public static string Serialize(TextComponent component)
    {
        return JsonSerializer.Serialize(component, typeof(TextComponent), MineJasonTextJsonContext.Default);
    }

    /// <summary>
    /// Asynchronously converts the specified <see cref="TextComponent"/> to its JSON representation and writes it to the
    /// specified stream.
    /// </summary>
    /// <param name="utf8Json">The stream to write to.</param>
    /// <param name="component">The component to convert from.</param>
    /// <param name="cancellation">The cancellation token.</param>
    /// <returns>A task that represents the operation.</returns>
    public static async Task SerializeAsync(Stream utf8Json, TextComponent component, CancellationToken cancellation = default)
    {
        await JsonSerializer.SerializeAsync(utf8Json, component, typeof(TextComponent), MineJasonTextJsonContext.Default, cancellationToken: cancellation);
    }

    /// <summary>
    /// Reads the UTF-8 encoding text into a chat component. The stream will be read to completion.
    /// </summary>
    /// <param name="utf8Json">The stream to read from.</param>
    /// <returns>The chat component.</returns>
    public static TextComponent? Deserialize(Stream utf8Json)
    {
        return (TextComponent?)JsonSerializer.Deserialize(utf8Json, typeof(TextComponent), MineJasonTextJsonContext.Default);
    }

    /// <summary>
    /// Parses the text representing a chat component and converts it to the instance representation.
    /// </summary>
    /// <param name="json">The text.</param>
    /// <returns>The converted chat component.</returns>
    public static TextComponent? Deserialize(ReadOnlySpan<char> json)
    {
        return (TextComponent?)JsonSerializer.Deserialize(json, typeof(TextComponent), MineJasonTextJsonContext.Default);
    }

    /// <summary>
    /// Asynchronously reads the UTF-8 encoding text into a chat component. The stream will be read to completion.
    /// </summary>
    /// <param name="utf8Json">The stream to read from.</param>
    /// <param name="cancellation">The cancellation token.</param>
    /// <returns>The chat component.</returns>
    public static async Task<TextComponent?> DeserializeAsync(Stream utf8Json, CancellationToken cancellation = default)
    {
        return (TextComponent?)await JsonSerializer.DeserializeAsync(utf8Json, typeof(TextComponent), MineJasonTextJsonContext.Default,
            cancellation);
    }
}
