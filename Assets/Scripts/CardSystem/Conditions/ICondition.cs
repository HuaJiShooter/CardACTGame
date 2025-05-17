using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICondition
{
    /// ���ڽ��յ�����Ȥ�¼�ʱ������
    bool Evaluate(Card self, GameEvent e, GameContext ctx);
    IEnumerable<GameEvt> SubscribedEvents { get; }   // ������ע����
}