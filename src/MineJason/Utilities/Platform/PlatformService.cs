// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: LGPL-3.0-or-later

namespace MineJason.Utilities.Platform;

/// <summary>
/// Provides platform support members.
/// </summary>
public static class PlatformService
{
    /// <summary>
    /// Gets the platform provider of the current application domain.
    /// </summary>
    public static IPlatformProvider? Provider { get; private set; }

    internal static void ClearPlatform()
    {
        Provider = null;
    }

    /// <summary>
    /// Sets the platform provider of the current application domain.
    /// </summary>
    /// <param name="provider">The provider.</param>
    /// <exception cref="InvalidOperationException">Provider were already set.</exception>
    public static void SetPlatform(IPlatformProvider? provider)
    {
        if (Provider != null)
        {
            throw new InvalidOperationException("Platform has already been provided.");
        }
        
        Provider = provider;
    }
}