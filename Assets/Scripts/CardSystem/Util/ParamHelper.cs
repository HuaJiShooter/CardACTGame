using System;

static class ParamHelper
{
    public static (string id, object? arg) Split(string spec)
    {
        var parts = spec.Split(new[] { '-', ':', '|' }, 2);
        return (parts[0], parts.Length > 1 ? parts[1] : null);
    }

    public static object? ConvertArg(Type targetType, string? raw)
    {
        if (raw == null) return null;
        return targetType switch
        {
            Type t when t == typeof(int) => int.Parse(raw),
            Type t when t == typeof(float) => float.Parse(raw),
            Type t when t == typeof(double) => double.Parse(raw),
            _ => raw          // д╛хо string
        };
    }
}