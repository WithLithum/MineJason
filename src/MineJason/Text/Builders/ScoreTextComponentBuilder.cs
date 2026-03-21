// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: LGPL-3.0-or-later

namespace MineJason.Text.Builders;

using MineJason.Components.Builders;

/// <summary>
/// Constructs a <see cref="ScoreboardChatComponent"/> with support for fluent syntax.
/// </summary>
public class ScoreTextComponentBuilder : ChatComponentBuilder<ScoreboardChatComponent>
{
    private string? _name;
    private string? _objective;

    /// <summary>
    /// Sets the name associated with the score holder to query.
    /// </summary>
    /// <param name="name">The name of the score holder.</param>
    /// <returns>This instance.</returns>
    public ScoreTextComponentBuilder Name(string name)
    {
        _name = name;
        return this;
    }

    /// <summary>
    /// Sets the name of the objective to query.
    /// </summary>
    /// <param name="objective">The objective.</param>
    /// <returns>The instance.</returns>
    public ScoreTextComponentBuilder Objective(string objective)
    {
        _objective = objective;
        return this;
    }

    /// <inheritdoc />
    public override ScoreboardChatComponent Build()
    {
        if (_name == null || _objective == null)
        {
            throw new InvalidOperationException("Name and objective fields are required.");
        }

        var creationInfo = CreateData();
        return new ScoreboardChatComponent(creationInfo)
        {
            Score = new ScoreboardChatComponent.Data(_name, _objective)
        };
    }
}
