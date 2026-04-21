// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: LGPL-3.0-or-later

using System.Text.Json.Serialization;
using MineJason.Extras.Serialization.Json;

namespace MineJason.Extras.Selectors;

/// <summary>
/// Selects an entity by its identifier.
/// </summary>
[JsonConverter(typeof(EntityGuidSelectorConverter))]
public class EntityGuidSelector : IEntitySelector
{
    /// <summary>
    /// Initializes a new instance of the <see cref="EntityGuidSelector"/> class.
    /// </summary>
    /// <param name="value">The identifier.</param>
    public EntityGuidSelector(Guid value)
    {
        Value = value;
    }

    /// <summary>
    /// Gets or sets the identifier to select.
    /// </summary>
    public Guid Value { get; set; }

    /// <summary>
    /// Converts this instance to its string representation.
    /// </summary>
    /// <returns>The string representation.</returns>
    public override string ToString()
    {
        return Value.ToString("D");
    }

    /// <summary>
    /// Converts the specified string representation of GUID to a GUID entity selector that selects
    /// the entity with that ID. A return value indicates whether the operation succeeded.
    /// </summary>
    /// <param name="value">The string to parse.</param>
    /// <param name="result">
    /// When this method returns, contains the result of successfully parsing <paramref name="value"/>
    /// or <see langword="null"/> on failure.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if <paramref name="value"/> was successfully parsed; otherwise,
    /// <see langword="false"/>.
    /// </returns>
    public static bool TryParse(string value, out EntityGuidSelector? result)
    {
        if (!Guid.TryParseExact(value, "D", out var guid))
        {
            result = null;
            return false;
        }

        result = new EntityGuidSelector(guid);
        return true;
    }
}