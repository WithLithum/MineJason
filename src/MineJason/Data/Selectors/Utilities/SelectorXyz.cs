// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: LGPL-3.0-or-later

namespace MineJason.Data.Selectors.Utilities;

internal readonly ref struct SelectorXyz
{
    public Vector3D? Origin { get; init; }
    public Vector3D? Diagonal { get; init; }
}