// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: LGPL-3.0-or-later

using MineJason.Text.Builders.Utilities;

namespace MineJason.Text.Builders;

/// <summary>
/// Provides services for building an NBT component.
/// </summary>
/// <typeparam name="T">The type of the NBT component.</typeparam>
public abstract class NbtComponentBuilder<T> : TextComponentBuilder<T>
    where T : NbtTextComponent
{
    private string? _path;
    private TextComponent? _separator;
    private bool _interpret;
    private bool _plain;

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
    /// Specifies that the Minecraft client should not add styling to the formatted NBT value.
    /// </summary>
    /// <param name="value">
    /// If <see langword="true"/>, the formatted NBT value is not styled.
    /// </param>
    /// <returns>The current instance.</returns>
    public NbtComponentBuilder<T> Plain(bool value = true)
    {
        _plain = value;
        return this;
    }

    /// <summary>
    /// Specifies the separator to display in case of multiple NBT values retrieved.
    /// </summary>
    /// <param name="separator">The separator.</param>
    /// <returns>This instance.</returns>
    public NbtComponentBuilder<T> Separator(TextComponent separator)
    {
        _separator = separator;
        return this;
    }

    /// <summary>
    /// Throws <see cref="InvalidOperationException"/> if the required base values are not yet
    /// submitted.
    /// </summary>
    protected NbtTextComponentCreationInfo CreateNBTData()
    {
        if (_path == null)
        {
            throw new InvalidOperationException("Path is required.");
        }

        return new NbtTextComponentCreationInfo
        {
            Path = _path,
            Separator = _separator,
            Interpret = _interpret,
            Plain = _plain
        };
    }
}