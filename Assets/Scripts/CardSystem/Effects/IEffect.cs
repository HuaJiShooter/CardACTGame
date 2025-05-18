using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEffect
{
    void Apply(Card self,CardUseContext ctx);

    void Apply(Card self, CardUseContext ctx, int param = 0);
}

//卡牌费用减少N点
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
        Debug.Log("手牌" + self.cardData.CardName + "费用已降为" + (Mathf.Max(0, self.cardData.BaseCost - (_amount * param)).ToString()));
    }

}
