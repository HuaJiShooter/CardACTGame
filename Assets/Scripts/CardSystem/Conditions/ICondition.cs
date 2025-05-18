using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public interface ICondition
{
    /// 仅在接收到感兴趣事件时被调用
    bool Evaluate(Card self, GameEvent e);
    IEnumerable<GameEvt> SubscribedEvents { get; }   // 供工厂注册用
}
// 带参数传递的ICondition
public interface IValueCondition : ICondition
{
    /// 满足条件时写入数值；不满足时忽略
    int Value { get; }
}


//手牌中有同名手牌
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


        // 统计“除自己之外”的同名卡
        GameObject _owner = e.Context.Owner;
        int sameCount = _owner.GetComponent<HandController>()._handPile.Count(c => c != self && c.cardData.CardName == self.cardData.CardName);
        Value = sameCount;
        return sameCount > 0;
    }
}
