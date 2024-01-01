namespace MineJason.Extensions.SharpNbt;
using MineJason.Data;
using SharpNBT;
using SharpNBT.SNBT;

public static class NbtProviderExtensions
{
    public static CompoundTag ToCompoundTag(this NbtProvider provider)
    {
        ArgumentNullException.ThrowIfNull(provider);

        return StringNbt.Parse(provider.ToString());
    }

    public static ListTag ToListTag(this NbtProvider provider)
    {
        ArgumentNullException.ThrowIfNull(provider);

        return StringNbt.ParseList(provider.ToString());
    }

    public static NbtProvider ToMineJason(this ListTag tag)
    {
        ArgumentNullException.ThrowIfNull(tag);

        return new NbtProvider(tag.Stringify());
    }

    public static NbtProvider ToMineJason(this CompoundTag tag)
    {
        return new NbtProvider(tag.Stringify());
    }
}
