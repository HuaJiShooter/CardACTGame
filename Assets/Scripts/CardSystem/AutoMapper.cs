using System;
using System.Collections.Generic;
using System.Reflection;

static class AutoMapper
{
    public static readonly Dictionary<string, Type> CondMap;
    public static readonly Dictionary<string, Type> EffMap;

    static AutoMapper()
    {            // ��̬���죺��������ʱִ��һ��
        CondMap = new Dictionary<string, Type>();
        EffMap = new Dictionary<string, Type>();

        var asm = Assembly.GetExecutingAssembly(); // Ҳ�ɱ��� AppDomain
        foreach (var t in asm.GetTypes())
        {
            if (typeof(ICondition).IsAssignableFrom(t))
            {
                var tag = t.GetCustomAttribute<ConditionTagAttribute>();
                if (tag != null) CondMap[tag.Id] = t;
            }
            if (typeof(IEffect).IsAssignableFrom(t))
            {
                var tag = t.GetCustomAttribute<EffectTagAttribute>();
                if (tag != null) EffMap[tag.Id] = t;
            }
        }
    }
}

//�Զ����ǩ

[AttributeUsage(AttributeTargets.Class, Inherited = false)]
sealed class ConditionTagAttribute : Attribute
{
    public string Id { get; }
    public ConditionTagAttribute(string id) => Id = id;
}

[AttributeUsage(AttributeTargets.Class, Inherited = false)]
sealed class EffectTagAttribute : Attribute
{
    public string Id { get; }
    public EffectTagAttribute(string id) => Id = id;
}