// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: LGPL-3.0-or-later

namespace MineJason.Data.Profile;

/// <summary>
/// Specifies the model type used to render the skin of a player.
/// </summary>
public enum PlayerModel
{
    /// <summary>
    /// The wide arms (classic or 'Steve') model. This is the original model supported since the
    /// introduction of multi-player in Classic.
    /// </summary>
    Wide,
    /// <summary>
    /// The slim arms ('Alex') model. This is the newer model introduced with the Alex skin in the
    /// 1.8 update.
    /// </summary>
    Slim
}