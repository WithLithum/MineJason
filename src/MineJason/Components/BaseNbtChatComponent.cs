// Copyright (c) WithLithum & contributors 2023-2024. All rights reserved.
// Licensed under the GNU Lesser General Public License, either version 3 or 
// (at your opinion) any later version.

namespace MineJason.Components;

using MineJason.Data;
using System;
using System.Text.Json.Serialization;

/// <summary>
/// Represents an NBT chat component.
/// </summary>
public abstract class BaseNbtChatComponent : ChatComponent, IEquatable<BaseNbtChatComponent>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BaseNbtChatComponent"/> class.
    /// </summary>
    /// <param name="source">The data source.</param>
    /// <param name="path">The NBT path.</param>
    protected BaseNbtChatComponent(NbtDataSource source, string path) : base("nbt")
    {
        DataSource = source;
        Path = path;
    }

    /// <summary>
    /// Gets the type of the data source.
    /// </summary>
    [JsonPropertyName("source")]
    public NbtDataSource DataSource { get; }

    /// <summary>
    /// Gets or sets the path to the NBT value to interpert.
    /// </summary>
    [JsonPropertyName("nbt")]
    public string Path { get; set; }

    /// <summary>
    /// Gets or stes a value indicating whether to attempt to interpert the NBT value retrieved
    /// as a raw JSON text component.
    /// </summary>
    [JsonPropertyName("interpert")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public bool Interpert { get; set; }

    /// <inheritdoc/>
    public override bool Equals(ChatComponent? other)
    {
        return base.Equals(other);
    }

    /// <inheritdoc/>
    public virtual bool Equals(BaseNbtChatComponent? other)
    {
        return other is not null
            && base.Equals(other)
            && other.DataSource == this.DataSource
            && other.Path == this.Path
            && other.Interpert == this.Interpert;
    }

    /// <inheritdoc/>
    public override bool Equals(object? obj)
    {
        return Equals(obj as BaseNbtChatComponent);
    }

    /// <summary>
    /// Generates the hash code of this instance.
    /// </summary>
    /// <returns>The hash code.</returns>
    public override int GetHashCode()
    {
        return HashCode.Combine(base.GetHashCode(), DataSource.GetHashCode(), Path.GetHashCode());
    }
}
