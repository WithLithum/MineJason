// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

namespace MineJason.Data.Nbt;

using System.Text.Json.Serialization;
using JetBrains.Annotations;
using MineJason.Serialization.TextJson;

/// <summary>
/// Provides raw string NBT data, without validation or other features.
/// </summary>
/// <remarks>
/// <para>
/// The <see cref="RawNbtDataProvider"/> class is an implementation of <see cref="INbtDataProvider"/> to be used
/// when there are no other <see cref="INbtDataProvider"/> available or for NBT data that were deserialized.
/// </para>
/// <para>
/// Unless absolutely necessary, user should not use this instance as a provider for NBT data. 
/// </para>
/// </remarks>
[PublicAPI]
[JsonConverter(typeof(RawNbtProviderConverter))]
public sealed class RawNbtDataProvider : INbtDataProvider, IEquatable<RawNbtDataProvider>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="RawNbtDataProvider"/> class.
    /// </summary>
    /// <param name="data">The data to provide.</param>
    public RawNbtDataProvider(string data)
    {
        Data = data;
    }
    
    /// <summary>
    /// Gets the data contained in this instance.
    /// </summary>
    public string Data { get; }

    /// <inheritdoc />
    public string GetRawNbt()
    {
        return Data;
    }

    /// <inheritdoc />
    public bool Equals(RawNbtDataProvider? other)
    {
        if (other is null)
        {
            return false;
        }

        if (ReferenceEquals(this, other))
        {
            return true;
        }

        return Data == other.Data;
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        return ReferenceEquals(this, obj) || obj is RawNbtDataProvider other && Equals(other);
    }

    /// <summary>
    /// Gets the hash code of this instance.
    /// </summary>
    /// <returns>The hash code.</returns>
    public override int GetHashCode()
    {
        return Data.GetHashCode();
    }
}