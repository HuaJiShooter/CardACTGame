using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEffect
{
    void Apply(Card self,CardUseContext ctx);

    void Apply(Card self, CardUseContext ctx, int param = 0);
}

//���Ʒ��ü���N��
[EffectTag("ReduceCost")]
public class E_ReduceCost : IEffect
{
    private int _amount;

    public E_ReduceCost(int amt) => _amount = amt;

    public void Apply(Card self, CardUseContext ctx)
    {
        self.curCost = Mathf.Max(0, self.cardData.BaseCost - (_amount));
    }

    public void Apply(Card self,CardUseContext ctx, int param)
    {
        self.curCost = Mathf.Max(0, self.cardData.BaseCost - (_amount* param));
        Debug.Log("����" + self.cardData.CardName + "�����ѽ�Ϊ" + (Mathf.Max(0, self.cardData.BaseCost - (_amount * param)).ToString()));
    }

}
