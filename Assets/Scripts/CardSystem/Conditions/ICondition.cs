using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public interface ICondition
{
    /// ���ڽ��յ�����Ȥ�¼�ʱ������
    bool Evaluate(Card self, GameEvent e);
    IEnumerable<GameEvt> SubscribedEvents { get; }   // ������ע����
}
// ���������ݵ�ICondition
public interface IValueCondition : ICondition
{
    /// ��������ʱд����ֵ��������ʱ����
    int Value { get; }
}


//��������ͬ������
[ConditionTag("SameCardInHand")]
public class C_SameCardInHand : IValueCondition
{
    public int Value { get; private set; }

    public IEnumerable<GameEvt> SubscribedEvents
    {
        get { yield return GameEvt.HandChanged; }
    }

    public bool Evaluate(Card self, GameEvent e)
    {


        // ͳ�ơ����Լ�֮�⡱��ͬ����
        GameObject _owner = e.Context.Owner;
        int sameCount = _owner.GetComponent<HandController>()._handPile.Count(c => c != self && c.cardData.CardName == self.cardData.CardName);
        Value = sameCount;
        return sameCount > 0;
    }
}
