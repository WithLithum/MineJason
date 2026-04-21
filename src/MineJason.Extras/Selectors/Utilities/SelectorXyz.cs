// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: LGPL-3.0-or-later

using System.Runtime.InteropServices;
using MineJason.Data;

namespace MineJason.Extras.Selectors.Utilities;

[StructLayout(LayoutKind.Sequential)]
internal readonly ref struct SelectorXyz
{
    public Vector3D? Origin { get; init; }
    public Vector3D? Diagonal { get; init; }
}