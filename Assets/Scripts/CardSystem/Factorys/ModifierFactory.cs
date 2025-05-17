using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

static class ModifierFactory
{
    public static Modifier Create(string name)
    {
        //通过ModifierName获取一个ModifiersTable行对象
        var modifier = CardDB.GetModifierRow(name);

        // 对Modifier对象的Conditions和Effects字符串列表依次反序列化并放入工厂类加工
        var cs = modifier.ConditionList.ToStringList().Select(ConditionFactory.Create);
        var es = modifier.EffectList.ToStringList().Select(EffectFactory.Create);

        //返回Modifier实例
        return new Modifier(cs, es);
    }
}