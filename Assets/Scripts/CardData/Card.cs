using UnityEngine;

public class Card
{
    // 卡牌实例属性
    public CardData CardData { get; private set; }
    public bool IsExhausted { get; private set; } // 是否已使用



    // 测试用构造函数
    public Card(string name, int cost)
    {
        this.CardData = new CardData(name, cost, 12f);
        this.IsExhausted = false;
    }

    //这里是构造函数
    public Card(CardData cardData)
    {
        
    }




    // 重置卡牌状态（从弃牌堆回收时调用）
    public void Reset()
    {
        IsExhausted = false;
    }
}
