namespace MineJason.Data.Selectors;

using System.Text;

internal static partial class EntitySelectorStringFormatter
{
    internal static string ToString(EntitySelector selector)
    {
        var kind = GetKindString(selector.Kind);
        
        var parameterString = GetParameterString(selector);

        return $"{kind}{(string.IsNullOrWhiteSpace(parameterString) ? string.Empty : $"[{parameterString}]")}";
    }

    private static string GetParameterString(EntitySelector selector)
    {
        var builder = new StringBuilder();
        var first = false;

        void Comma()
        {
            if (first)
            {
                builder!.Append(',');
            }

            first = true;
        }
        
        if (selector.Position.HasValue)
        {
            Comma();
            AddPosition(ref builder, selector.Position);
        }

        if (selector.Distance.HasValue)
        {
            Comma();
            builder.Append("distance=");
            builder.Append(selector.Distance);
        }

        if (selector.DiagonalRange.HasValue)
        {
            Comma();
            AddDiagonalRange(ref builder, selector.DiagonalRange);
        }

        if (selector.Tags.Count != 0)
        {
            Comma();
            builder.Append(selector.Tags);
        }

        if (selector.Scores.Count != 0)
        {
            Comma();
            builder.Append("scores=");
            var str = selector.Scores.ToString();
            builder.Append(str);
        }

        if (selector.Team != null)
        {
            Comma();
            if (!selector.Team.Value.IsValid())
            {
                throw new ArgumentException("The team property is invalid.", nameof(selector));
            }
            
            builder.Append(selector.Team);
        }

        if (selector.Limit > 0)
        {
            Comma();
            builder.Append("limit=").Append(selector.Limit);
        }

        if (selector.Sort.HasValue)
        {
            Comma();
            builder.Append("sort=").Append(selector.Sort.Value.ToString().ToLowerInvariant());
        }

        if (selector.Level.HasValue)
        {
            Comma();
            builder.Append("level=").Append(selector.Level.Value);
        }

        if (selector.GameMode.HasValue)
        {
            Comma();
            builder.Append(selector.GameMode.Value);
        }

        if (selector.Name.HasValue)
        {
            Comma();
            builder.Append(selector.Name.Value);
        }

        return builder.ToString();
    }

    private static string GetKindString(EntitySelectorKind kind)
    {
        return kind switch
        {
            EntitySelectorKind.Executor => "@s",
            EntitySelectorKind.AllEntities => "@e",
            EntitySelectorKind.AllPlayers => "@a",
            EntitySelectorKind.NearestPlayer => "@p",
            EntitySelectorKind.RandomPlayer => "@r",
            _ => throw new ArgumentException("Unknown kind", nameof(kind))
        };
    }
    
    private static void AddDiagonalRange(ref StringBuilder builder, Vector3D? diagonalRange)
    {
        if (!diagonalRange.HasValue)
        {
            return;
        }
        
        builder.Append("dx=")
            .Append(diagonalRange.Value.X).Append(',')
            .Append("dy=").Append(diagonalRange.Value.Y).Append(',')
            .Append("dz=").Append(diagonalRange.Value.Z);
    }

    private static void AddPosition(ref StringBuilder builder, Vector3D? selectorPosition)
    {
        if (!selectorPosition.HasValue)
        {
            return;
        }
        
        builder.Append("x=")
            .Append(selectorPosition.Value.X).Append(',')
            .Append("y=").Append(selectorPosition.Value.Y).Append(',')
            .Append("z=").Append(selectorPosition.Value.Z);
    }
}