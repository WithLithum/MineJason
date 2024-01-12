namespace MineJason.Data.Selectors;

using System.Text;

/// <summary>
/// Provides parsing for entity selectors.
/// </summary>
public static partial class EntitySelectorStringFormatter
{
    internal static string ToString(EntitySelector selector)
    {
        var kind = GetKindString(selector.Kind);
        
        var parameterString = GetParameterString(selector);
        
        #if DEBUG
        Console.WriteLine("-- parameter string --");
        Console.WriteLine(parameterString ?? "parameter string null");        
        #endif

        return $"{kind}{(string.IsNullOrWhiteSpace(parameterString) ? string.Empty : $"[{parameterString}]")}";
    }

    private static string GetParameterString(EntitySelector selector)
    {
        var builder = new EntitySelectorArgumentBuilder();
        
        if (selector.Position.HasValue)
        {
            AddPosition(builder, selector.Position);
        }

        if (selector.Distance.HasValue)
        {
            builder.WritePair("distance", selector.Distance.Value);
        }

        if (selector.DiagonalRange.HasValue)
        {
            AddDiagonalRange(builder, selector.DiagonalRange);
        }

        if (selector.Tags.Count != 0)
        {
            selector.Tags.WriteToBuilder(builder);
        }

        if (selector.Scores.Count > 0)
        {
            #if DEBUG
            Console.WriteLine("Selector score parsing: OK");            
            #endif
            
            selector.Scores.WriteToBuilder(builder);
        }

        selector.Team?.WriteToBuilder(builder);

        if (selector.Limit > 0)
        {
            builder.WritePair("limit", selector.Limit);
        }

        if (selector.Sort.HasValue)
        {
            builder.WritePair("sort", selector.Sort.Value.ToString().ToLowerInvariant());
        }

        if (selector.Level.HasValue)
        {
            builder.WritePair("level", selector.Level.Value);
        }

        if (selector.GameMode.HasValue)
        {
            selector.GameMode.Value.WriteToBuilder(builder);
        }
        
        // Rotation
        if (selector.VerticalRotation.HasValue)
        {
            builder.WritePair("x_rotation", selector.VerticalRotation.Value);
        }

        if (selector.HorizontalRotation.HasValue)
        {
            builder.WritePair("y_rotation", selector.HorizontalRotation.Value);
        }

        selector.Name?.WriteToBuilder(builder);

        if (selector.Type.HasValue)
        {
            builder.WritePair("type", selector.Type.Value.ToString());
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
    
    private static void AddDiagonalRange(EntitySelectorArgumentBuilder builder, Vector3D? diagonalRange)
    {
        if (!diagonalRange.HasValue)
        {
            return;
        }
        
        builder.WritePair("dx", diagonalRange.Value.X);
        builder.WritePair("dy", diagonalRange.Value.Y);
        builder.WritePair("dz", diagonalRange.Value.Z);
    }

    private static void AddPosition(EntitySelectorArgumentBuilder builder, Vector3D? selectorPosition)
    {
        if (!selectorPosition.HasValue)
        {
            return;
        }
        
        builder.WritePair("x", selectorPosition.Value.X);
        builder.WritePair("y", selectorPosition.Value.Y);
        builder.WritePair("z", selectorPosition.Value.Z);
    }
}