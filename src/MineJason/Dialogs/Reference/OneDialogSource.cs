// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: LGPL-3.0-or-later

namespace MineJason.Dialogs.Reference;

/// <summary>
/// Defines a reference that reference only a single dialog.
/// </summary>
public abstract record OneDialogSource : MultiDialogSource
{
    internal OneDialogSource()
    {
    }
}
