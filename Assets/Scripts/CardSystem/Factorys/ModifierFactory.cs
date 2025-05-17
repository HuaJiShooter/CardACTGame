using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

static class ModifierFactory
{
    public static Modifier Create(string name)
    {
        //ͨ��ModifierName��ȡһ��ModifiersTable�ж���
        var modifier = CardDB.GetModifierRow(name);

        // ��Modifier�����Conditions��Effects�ַ����б����η����л������빤����ӹ�
        var cs = modifier.ConditionList.ToStringList().Select(ConditionFactory.Create);
        var es = modifier.EffectList.ToStringList().Select(EffectFactory.Create);

        //����Modifierʵ��
        return new Modifier(cs, es);
    }
}