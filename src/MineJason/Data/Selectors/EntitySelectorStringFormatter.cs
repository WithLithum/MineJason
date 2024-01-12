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
        var builder = new StringBuilder();
        var first = false;
        #if DEBUG
        var commas = 0;        
        #endif

        void Comma()
        {
            if (first)
            {
                builder.Append(',');
            }
            
            #if DEBUG
            Console.WriteLine("Comma selector: #{0}, first = {1}", commas++, first);
            #endif

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

        if (selector.Scores.Count > 0)
        {
            #if DEBUG
            Console.WriteLine("Selector score parsing: OK");            
            #endif
            
            // scores
            var str = selector.Scores.ToString();

            if (!string.IsNullOrWhiteSpace(str))
            {
                Comma();
                builder.Append("scores=");
                builder.Append(str);   
            }
        }

        // teams
        var teamStr = selector.Team.ToString();

        if (!string.IsNullOrWhiteSpace(teamStr))
        {
#if DEBUG
            Console.WriteLine("Selector team parsing: OK");            
#endif
            
            Comma();
            
            builder.Append(selector.Team);
        }
        
        if (selector.Limit > 0)
        {
#if DEBUG
            Console.WriteLine("Selector limit parsing: OK");            
#endif
            
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
        
        // Rotation
        if (selector.VerticalRotation.HasValue)
        {
            Comma();
            builder.Append("x_rotation=").Append(selector.VerticalRotation);
        }

        if (selector.HorizontalRotation.HasValue)
        {
            Comma();
            builder.Append("y_rotation=").Append(selector.HorizontalRotation);
        }
        
        if (selector.Name.HasValue)
        {
            var nameValue = selector.Name.Value.ToString();

            if (!string.IsNullOrWhiteSpace(nameValue))
            {
#if DEBUG
                Console.WriteLine("Selector name parsing: OK");            
#endif
            
                Comma();
                builder.Append(selector.Name.Value);
            }
        }

        if (selector.Type.HasValue)
        {
            Comma();
            builder.Append("type=").Append(selector.Type);
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