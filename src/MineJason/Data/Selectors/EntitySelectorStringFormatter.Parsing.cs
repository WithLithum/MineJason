namespace MineJason.Data.Selectors;

internal static partial class EntitySelectorStringFormatter
{
    internal static bool TryParseSortMode(string value, out EntitySelectorSortMode mode)
    {
        mode = EntitySelectorSortMode.Arbitrary;

        switch (value)
        {
            case "arbitrary":
                mode = EntitySelectorSortMode.Arbitrary;
                return true;
            case "random":
                mode = EntitySelectorSortMode.Random;
                return true;
            case "furthest":
                mode = EntitySelectorSortMode.Furthest;
                return true;
            case "nearest":
                mode = EntitySelectorSortMode.Nearest;
                return true;
        }

        return false;
    }
}