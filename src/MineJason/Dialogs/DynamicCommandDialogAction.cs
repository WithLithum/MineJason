// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

namespace MineJason.Dialogs;

using System.Diagnostics.CodeAnalysis;

/// <summary>
/// Represents a dialog action that interprets function macros in the specified value with the
/// values retrieved from input controls prior to executing the interpreted macro as a command.
/// </summary>
public sealed record DynamicCommandDialogAction : DialogAction,
    IEquatable<DynamicCommandDialogAction>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DynamicCommandDialogAction"/> class.
    /// </summary>
    public DynamicCommandDialogAction()
    {
    }

    /// <summary>
    /// Initializes a new instance of <see cref="DynamicCommandDialogAction"/> with the specified
    /// template.
    /// </summary>
    /// <param name="template">The macro template to interpret and execute.</param>
    [SetsRequiredMembers]
    public DynamicCommandDialogAction(string template)
    {
        Template = template;
    }

    /// <summary>
    /// Gets the function macro template to interpret and execute.
    /// </summary>
    public required string Template { get; init; }
}
