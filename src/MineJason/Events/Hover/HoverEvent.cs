using System.Text.Json.Serialization;

namespace MineJason.Events.Hover;

[JsonPolymorphic(TypeDiscriminatorPropertyName = "action")]
[JsonDerivedType(typeof(ShowTextHoverEvent), "show_text")]
[JsonDerivedType(typeof(ShowEntityHoverEvent), "show_entity")]
[JsonDerivedType(typeof(ShowItemHoverEvent), "show_item")]
public abstract class HoverEvent : IEquatable<HoverEvent>
{
    public abstract bool Equals(HoverEvent? other);
    public abstract override int GetHashCode();

    public override bool Equals(object? obj)
    {
        return Equals(obj as HoverEvent);
    }
}
