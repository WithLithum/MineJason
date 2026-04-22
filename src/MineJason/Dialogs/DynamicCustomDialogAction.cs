// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

namespace MineJason.Dialogs;

using System;
using System.Diagnostics.CodeAnalysis;

/// <summary>
/// Represents a dialog action that triggers a <see cref="Text.Behaviour.Click.CustomClickEvent"/>
/// with its payload being a SNBT compound tag consisting of values from every input field.
/// </summary>
public sealed record DynamicCustomDialogAction : DialogAction
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DynamicCustomDialogAction"/> class.
    /// </summary>
    public DynamicCustomDialogAction()
    {
    }

    /// <summary>
    /// Initializes a new instance of <see cref="DynamicCustomDialogAction"/> with the specified
    /// event identifier and additional payload fields.
    /// </summary>
    /// <param name="id">The identifier used to identify the event.</param>
    /// <param name="additions">The additional fields to add to the payload.</param>
    [SetsRequiredMembers]
    public DynamicCustomDialogAction(ResourceLocation id,
        IReadOnlyDictionary<string, object>? additions = null)
    {
        Id = id;
    }

    /// <summary>
    /// Gets the identifier of the custom dialog action.
    /// </summary>
    public required ResourceLocation Id { get; init; }
}
