// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

namespace MineJason.Dialogs.Reference;

using MineJason;
using System.Diagnostics.CodeAnalysis;

/// <summary>
/// References an existing dialog defined in server data registry.
/// </summary>
public sealed record DialogReference : OneDialogSource
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DialogReference"/> class.
    /// </summary>
    public DialogReference()
    {
    }

    /// <summary>
    /// Initializes a new instance of <see cref="DialogReference"/> with the specified identifier.
    /// </summary>
    /// <param name="identifier">The identifier to the dialog.</param>
    [SetsRequiredMembers]
    public DialogReference(ResourceLocation identifier)
    {
        Identifier = identifier;
    }

    /// <summary>
    /// Gets the identifier of the dialog to reference to.
    /// </summary>
    public required ResourceLocation Identifier { get; init; }
}
