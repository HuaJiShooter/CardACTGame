using System;

static class EffectFactory
{
    public static IEffect Create(string spec)
    {
        var (id, argStr) = ParamHelper.Split(spec);
        if (!AutoMapper.EffMap.TryGetValue(id, out var tp))
            throw new Exception($"Unknown Effect id:{id}");

        return argStr == null
            ? (IEffect)Activator.CreateInstance(tp)!
            : (IEffect)Activator.CreateInstance(tp,
                  ParamHelper.ConvertArg(tp.GetConstructors()[0]
                      .GetParameters()[0].ParameterType, (string)argStr))!;
    }
}