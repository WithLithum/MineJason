// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

namespace MineJason.Dialogs.Reference;

/// <summary>
/// Represents a dialog pointer that points to multiple dialog contained in a tag.
/// </summary>
public sealed record TagDialogSource : MultiDialogSource, IEquatable<TagDialogSource>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="TagDialogSource"/> class.
    /// </summary>
    public TagDialogSource(ResourceLocation id)
    {
        Id = id;
    }

    /// <summary>
    /// Gets the identifier of the tag to reference.
    /// </summary>
    public ResourceLocation Id { get; }
}
