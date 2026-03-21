// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: LGPL-3.0-or-later

namespace MineJason.Utilities;
using System;
using System.Diagnostics;
using System.Globalization;

internal static class DebugHelper
{
    [Conditional("DEBUG")]
    internal static void Output(object output)
    {
        Console.WriteLine("[! MJS-DBG]: {0}", output);
    }

    [Conditional("DEBUG")]
    internal static void Output(string output, params object[] args)
    {
        Console.WriteLine("[! MJS-DBG]: {0}", string.Format(CultureInfo.InvariantCulture, output, args));
    }
}
