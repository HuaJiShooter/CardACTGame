using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICondition
{
    /// 仅在接收到感兴趣事件时被调用
    bool Evaluate(Card self, GameEvent e);
    IEnumerable<GameEvt> SubscribedEvents { get; }   // 供工厂注册用
}