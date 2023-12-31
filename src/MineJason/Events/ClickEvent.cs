using System.Text.Json.Serialization;

namespace MineJason.Events;

[JsonPolymorphic(TypeDiscriminatorPropertyName = "action")]
[JsonDerivedType(typeof(ChangePageClickEvent), "change_page")]
[JsonDerivedType(typeof(CopyToClipboardClickEvent), "copy_to_clipboard")]
[JsonDerivedType(typeof(OpenUrlClickEvent), "open_url")]
[JsonDerivedType(typeof(RunCommandClickEvent), "run_command")]
[JsonDerivedType(typeof(SuggestCommandClickEvent), "suggest_command")]
public abstract class ClickEvent
{
}
