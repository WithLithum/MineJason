// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: LGPL-3.0-or-later

namespace MineJason;

using JetBrains.Annotations;
using MineJason.Serialization.TextJson;
using MineJason.Text;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

/// <summary>
/// Represents a chat component that resolves to the value of the scoreboard entity for the specified
/// objective upon being presented to the user.
/// </summary>
[PublicAPI]
[JsonConverter(typeof(TextComponentConverter))]
public sealed record ScoreboardChatComponent : ChatComponent,
    IEquatable<ScoreboardChatComponent>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ScoreboardChatComponent"/> class.
    /// </summary>
    public ScoreboardChatComponent()
    {
    }

    /// <summary>
    /// Initializes a new instance of <see cref="ScoreboardChatComponent"/> with the specified
    /// data.
    /// </summary>
    /// <param name="data">The data.</param>
    [SetsRequiredMembers]
    public ScoreboardChatComponent(Data data)
    {
        Score = data;
    }

    internal ScoreboardChatComponent(in TextComponentCreationInfo creationInfo)
        : base(creationInfo)
    {
    }

    /// <summary>
    /// Contains information about the score holder and the objective.
    /// </summary>
    public sealed record Data
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Data"/> class.
        /// </summary>
        public Data()
        {
        }

        /// <summary>
        /// Initializes a new instance of <see cref="Data"/> with the specified score holder and
        /// objective.
        /// </summary>
        /// <param name="name">The name or selector pointing to the score holder.</param>
        /// <param name="objective">The name of the objective.</param>
        [SetsRequiredMembers]
        public Data(string name, string objective)
        {
            Name = name;
            Objective = objective;
        }

        /// <summary>
        /// Gets the name or selector to a score holder to display the score value of.
        /// </summary>
        public required string Name { get; init; }

        /// <summary>
        /// Gets the objective to display score value from.
        /// </summary>
        public required string Objective { get; init; }
    }

    /// <summary>
    /// Gets the scoreboard details.
    /// </summary>
    public required Data Score { get; init; }

    /// <inheritdoc />
    public override string ToString()
    {
        return $"score={{name={Score.Name},objective={Score.Objective}}}";
    }
}