// Copyright (c) WithLithum & contributors 2023-2024. All rights reserved.
// Licensed under the GNU Lesser General Public License, either version 3 or
// (at your opinion) any later version.

namespace MineJason.Events;

using System.Text.Json.Serialization;

/// <summary>
/// Represents a click event.
/// </summary>
[JsonPolymorphic(TypeDiscriminatorPropertyName = "action")]
[JsonDerivedType(typeof(ChangePageClickEvent), "change_page")]
[JsonDerivedType(typeof(CopyToClipboardClickEvent), "copy_to_clipboard")]
[JsonDerivedType(typeof(OpenUrlClickEvent), "open_url")]
[JsonDerivedType(typeof(RunCommandClickEvent), "run_command")]
[JsonDerivedType(typeof(SuggestCommandClickEvent), "suggest_command")]
public abstract class ClickEvent
{
}
