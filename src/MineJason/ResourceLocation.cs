namespace MineJason;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Represents a resource location (an identifier with a name space).
/// </summary>
public readonly struct ResourceLocation
{
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

    public override string ToString()
    {
        return $"{NameSpace}:{Path}";
    }

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
}
