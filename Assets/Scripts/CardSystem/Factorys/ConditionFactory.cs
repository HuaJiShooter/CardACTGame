using System;

static class ConditionFactory
{
    public static ICondition Create(string spec)
    {
        var (id, argStr) = ParamHelper.Split(spec);
        if (!AutoMapper.CondMap.TryGetValue(id, out var tp))
            throw new Exception($"Unknown Condition id:{id}");

        return argStr == null
            ? (ICondition)Activator.CreateInstance(tp)!
            : (ICondition)Activator.CreateInstance(tp,
                  ParamHelper.ConvertArg(tp.GetConstructors()[0]
                      .GetParameters()[0].ParameterType, (string)argStr))!;
    }

}