namespace MineJason.Serialization;

using System.Text.Json;
using JetBrains.Annotations;

/// <summary>
/// Provides methods to assist with serialization of chat components by using
/// System.Text.Json.
/// </summary>
[PublicAPI]
[Obsolete("Serialize directly with usual methods instead.")]
public static class JsonComponentSerializer
{
    /// <summary>
    /// Converts the specified <see cref="ChatComponent"/> to its JSON representation.
    /// </summary>
    /// <param name="component">The component to convert from.</param>
    /// <returns>The converted JSON.</returns>
    public static string Serialize(ChatComponent component)
    {
        return JsonSerializer.Serialize(component, ChatComponent.SerializerOptions);
    }

    /// <summary>
    /// Asynchronously converts the specified <see cref="ChatComponent"/> to its JSON representation and writes it to the
    /// specified stream.
    /// </summary>
    /// <param name="utf8Json">The stream to write to.</param>
    /// <param name="component">The component to convert from.</param>
    /// <param name="cancellation">The cancellation token.</param>
    /// <returns>A task that represents the operation.</returns>
    public static async Task SerializeAsync(Stream utf8Json, ChatComponent component, CancellationToken cancellation = default)
    {
        await JsonSerializer.SerializeAsync(utf8Json, component, ChatComponent.SerializerOptions, cancellation);
    }

    /// <summary>
    /// Reads the UTF-8 encoding text into a chat component. The stream will be read to completion.
    /// </summary>
    /// <param name="utf8Json">The stream to read from.</param>
    /// <returns>The chat component.</returns>
    public static ChatComponent? Deserialize(Stream utf8Json)
    {
        return JsonSerializer.Deserialize<ChatComponent>(utf8Json);
    }

    /// <summary>
    /// Parses the text representing a chat component and converts it to the instance representation.
    /// </summary>
    /// <param name="json">The text.</param>
    /// <returns>The converted chat component.</returns>
    public static ChatComponent? Deserialize(ReadOnlySpan<char> json)
    {
        return JsonSerializer.Deserialize<ChatComponent>(json);
    }

    /// <summary>
    /// Asynchronously reads the UTF-8 encoding text into a chat component. The stream will be read to completion.
    /// </summary>
    /// <param name="utf8Json">The stream to read from.</param>
    /// <param name="cancellation">The cancellation token.</param>
    /// <returns>The chat component.</returns>
    public static async Task<ChatComponent?> DeserializeAsync(Stream utf8Json, CancellationToken cancellation = default)
    {
        return await JsonSerializer.DeserializeAsync<ChatComponent>(utf8Json, ChatComponent.SerializerOptions,
            cancellation);
    }
}
