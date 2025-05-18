using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Card
{
    public readonly CardData cardData;
    /* ---------- 生成时字段 ---------- */
    public struct CardData { 
        public string CardName; 
        public int BaseCost;
        public string Description;
        public string CardFace;
        public float BaseColdTime;
        public float WaitTime;
        public List<string> ModifierList;
        public List<string> ObjectList;

        public CardData(CardsTable cardstable)
        {
            CardName = cardstable.CardName;
            BaseCost = cardstable.Cost;
            Description = cardstable.Description;
            CardFace = cardstable.CardFace;
            BaseColdTime = cardstable.ColdTime;
            WaitTime = cardstable.WaitTime;
            ModifierList = cardstable.ModifierList.ToStringList();
            ObjectList = cardstable.ObjectList.ToStringList();
        }
    }

    /* ---------- 运行时字段 ---------- */
    public float curColdTime;
    public int curCost;
    public bool playable = false;
    readonly List<Modifier> _modifiers = new();

    public Card(CardsTable row, Object obj)
    {
        cardData = new CardData(row);
        curColdTime = cardData.BaseColdTime;
        curCost = cardData.BaseCost;

        foreach (var modifier_name in cardData.ModifierList)
        {
            Modifier modifier = ModifierFactory.Create(modifier_name);
            _modifiers.Add(modifier);
            modifier.Bind(this);
        }
    }

    public void Play(CardUseContext ctx)
    {
        if (!this.playable) return;
        ctx.Owner.GetComponent<FeeManager>().ConsumeFee(curCost);
        EventBus.Publish(new GameEvent { Type = GameEvt.CardPlayed, Source = this });
    }
}
