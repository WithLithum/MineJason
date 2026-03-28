// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: LGPL-3.0-or-later

using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using JetBrains.Annotations;
using MineJason.Serialization.TextJson;

namespace MineJason.Text;

/// <summary>
/// Represents a text component that resolves to a scoreboard value. This class cannot be
/// inherited.
/// </summary>
/// <remarks>
/// This type of component is resolved on the server side and will display nothing if resolved on
/// the client as-is.
/// </remarks>
[PublicAPI]
[JsonConverter(typeof(TextComponentConverter))]
public sealed record ScoreTextComponent : TextComponent,
    IEquatable<ScoreTextComponent>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ScoreTextComponent"/> class.
    /// </summary>
    public ScoreTextComponent()
    {
    }

    /// <summary>
    /// Initializes a new instance of <see cref="ScoreTextComponent"/> with the specified
    /// data.
    /// </summary>
    /// <param name="data">The data.</param>
    [SetsRequiredMembers]
    public ScoreTextComponent(Data data)
    {
        Score = data;
    }

    internal ScoreTextComponent(in TextComponentCreationInfo creationInfo)
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