// Copyright (c) WithLithum & contributors 2023-2024. All rights reserved.
// Licensed under the GNU Lesser General Public License, either version 3 or
// (at your opinion) any later version.

namespace MineJason.Components.Builders;

/// <summary>
/// Provides services for building an NBT component.
/// </summary>
/// <typeparam name="T">The type of the NBT component.</typeparam>
public abstract class NbtComponentBuilder<T> : ChatComponentBuilder<T>
    where T : BaseNbtChatComponent
{
    private string? _path;
    private ChatComponent? _separator;
    private bool _interpret;
    
    /// <summary>
    /// Specifies the path to the NBT value.
    /// </summary>
    /// <param name="path">The path.</param>
    /// <returns>This instance.</returns>
    public NbtComponentBuilder<T> Path(string path)
    {
        _path = path;
        return this;
    }

    /// <summary>
    /// Specifies that the game will attempt to interpret the NBT value retrieved as raw JSON value.
    /// </summary>
    /// <param name="value">If <see langword="true"/>, the value is interpreted.</param>
    /// <returns>This instance.</returns>
    public NbtComponentBuilder<T> Interpret(bool value = true)
    {
        _interpret = value;
        return this;
    }

    /// <summary>
    /// Specifies the separator to display in case of multiple NBT values retrieved.
    /// </summary>
    /// <param name="separator">The separator.</param>
    /// <returns>This instance.</returns>
    public NbtComponentBuilder<T> Separator(ChatComponent separator)
    {
        _separator = separator;
        return this;
    }

    /// <inheritdoc />
    public override T Build()
    {
        if (_path == null)
        {
            throw new InvalidOperationException("Building not complete; path is missing");
        }

        var component = CreateComponent(_path);
        Apply(component);
        component.Interpret = _interpret;
        component.Separator = _separator;
        return component;
    }

    /// <summary>
    /// Creates the final component instance.
    /// </summary>
    /// <returns>The final component instance.</returns>
    protected abstract T CreateComponent(string path);
}