// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: LGPL-3.0-or-later

namespace MineJason.Dialogs.Reference;

/// <summary>
/// Represents a pointer referencing all dialog referenced by the specified pointer of the given
/// type.
/// </summary>
public sealed record CollectionDialogSource : MultiDialogSource,
    IEquatable<CollectionDialogSource>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CollectionDialogSource"/> class.
    /// </summary>
    /// <param name="values">The values.</param>
    public CollectionDialogSource(IReadOnlyCollection<OneDialogSource> values)
    {
        Values = values;
    }

    /// <summary>
    /// Gets a collection of pointers contained within this instance.
    /// </summary>
    public IReadOnlyCollection<OneDialogSource> Values { get; }
}
