// Copyright (c) WithLithum & contributors 2023-2024. All rights reserved.
// Licensed under the GNU Lesser General Public License, either version 3 or
// (at your opinion) any later version.

namespace MineJason;

using System;
using System.Text.Json.Serialization;
using MineJason.Serialization.TextJson;

/// <summary>
/// Represents a resource location (an identifier with a namespace).
/// </summary>
[JsonConverter(typeof(ResourceLocationConverter))]
public readonly struct ResourceLocation : IEquatable<ResourceLocation>
{
    /// <summary>
    /// Initialises a new instance of the <see cref="ResourceLocation"/> structure.
    /// </summary>
    /// <param name="nameSpace">The namespace.</param>
    /// <param name="path">The path.</param>
    /// <exception cref="ArgumentException">The namespace or path is invalid.</exception>
    public ResourceLocation(string nameSpace, string path)
    {
        if (!IsNamespaceValid(nameSpace))
        {
            throw new ArgumentException("Invalid namespace.", nameof(nameSpace));
        }

        if (!IsPathValid(path))
        {
            throw new ArgumentException("Invalid path.", nameof(path));
        }

        NameSpace = nameSpace;
        Path = path;
    }

    private ResourceLocation(string nameSpace, string path, bool fast)
    {
        if (!fast)
        {
            if (!IsNamespaceValid(nameSpace))
            {
                throw new ArgumentException("Invalid namespace.", nameof(nameSpace));
            }

            if (!IsPathValid(path))
            {
                throw new ArgumentException("Invalid path.", nameof(path));
            }
        }

        NameSpace = nameSpace;
        Path = path;
    }

    /// <summary>
    /// Gets or sets the namespace of this resource location.
    /// </summary>
    public string NameSpace { get; }

    /// <summary>
    /// Gets or sets the path of this resource location.
    /// </summary>
    public string Path { get; }

    /// <summary>
    /// Determines whether this instance represents a valid Minecraft: Java Edition resource location.
    /// </summary>
    /// <returns><see langword="true"/> if this resource location is valid; otherwise, <see langword="false"/>.</returns>
    public bool IsValid()
    {
        return IsNamespaceValid(NameSpace) && IsPathValid(Path);
    }

    /// <summary>
    /// Converts this <see cref="ResourceLocation"/> to its string representation.
    /// </summary>
    /// <returns>The string representation of this <see cref="ResourceLocation"/>.</returns>
    public override string ToString()
    {
        return $"{NameSpace}:{Path}";
    }

    /// <summary>
    /// Determines whether the specified namespace is valid.
    /// </summary>
    /// <param name="value">The namespace to check.</param>
    /// <returns><see langword="true"/> if the specified namespace is valid; otherwise, <see langword="false"/>.</returns>
    public static bool IsNamespaceValid(ReadOnlySpan<char> value)
    {
        foreach (var ch in value)
        {
            if (ch != '_' && ch != '-' && ch != '.' && !char.IsAsciiDigit(ch) && !char.IsAsciiLetterLower(ch))
            {
                return false;
            }
        }

        return true;
    }

    /// <summary>
    /// Determines whether the specified resource path is valid.
    /// </summary>
    /// <param name="value">The resource path to check.</param>
    /// <returns><see langword="true"/> if the specified resource path is valid; otherwise, <see langword="false"/>.</returns>
    public static bool IsPathValid(ReadOnlySpan<char> value)
    {
        var lastWasSlash = false;

        foreach (var ch in value)
        {
            if (ch == '/')
            {
                if (lastWasSlash)
                {
                    return false;
                }

                lastWasSlash = true;
                continue;
            }
            lastWasSlash = false;

            if (ch != '_' && ch != '-' && ch != '.' && ch != '/' && !char.IsAsciiDigit(ch) && !char.IsAsciiLetterLower(ch))
            {
                return false;
            }
        }

        return true;
    }

    /// <summary>
    /// Attempts to convert the string representation provided to the instance presentation of a
    /// <see cref="ResourceLocation"/>.
    /// </summary>
    /// <param name="from">The string to parse.</param>
    /// <param name="result">The parsing result.</param>
    /// <param name="allowMinecraftDefault">If <see langword="true"/>, the namespace <c>minecraft</c> will be used if the namespace is absent.</param>
    /// <returns><see langword="true"/> if the conversion is successful; otherwise, <see langword="false"/>.</returns>
    public static bool TryParse(string from, out ResourceLocation result, bool allowMinecraftDefault = true)
    {
        var split = from.Split(':');
        result = default;

        if (split.Length == 1 && allowMinecraftDefault)
        {
            if (!IsPathValid(from)) return false;
            result = new ResourceLocation("minecraft", from, true);
            return true;
        }

        if (split.Length != 2)
        {
            return false;
        }

        if (!IsNamespaceValid(split[0])) return false;
        if (!IsPathValid(split[1])) return false;

        result = new ResourceLocation(split[0], split[1], true);
        return true;
    }

    /// <inheritdoc />
    public bool Equals(ResourceLocation other)
    {
        return NameSpace == other.NameSpace && Path == other.Path;
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        return obj is ResourceLocation other && Equals(other);
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        return HashCode.Combine(NameSpace, Path);
    }

    /// <summary>
    /// Determines whether the instance to the <paramref name="left"/> is equivalent to the instance to the <paramref name="right"/>.
    /// </summary>
    /// <param name="left">The instance to the left.</param>
    /// <param name="right">The instance to the right.</param>
    /// <returns><see langword="true"/> if the instance to the left is equivalent to the instance to the <paramref name="right"/>; otherwise, <see langword="false"/>.</returns>
    public static bool operator ==(ResourceLocation left, ResourceLocation right)
    {
        return left.Equals(right);
    }

    /// <summary>
    /// Determines whether the instance to the <paramref name="left"/> is not equivalent to the instance to the <paramref name="right"/>.
    /// </summary>
    /// <param name="left">The instance to the left.</param>
    /// <param name="right">The instance to the right.</param>
    /// <returns><see langword="true"/> if the instance to the left is not equivalent to the instance to the <paramref name="right"/>; otherwise, <see langword="false"/>.</returns>
    public static bool operator !=(ResourceLocation left, ResourceLocation right)
    {
        return !(left == right);
    }
}
