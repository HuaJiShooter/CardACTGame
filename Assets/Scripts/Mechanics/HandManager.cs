using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class HandManager : MonoBehaviour
{
    public List<CardData> deck; // 卡组
    public List<CardData> hand; // 当前手牌
    public int handLimit = 10;  // 手牌上限

    public HandUI handUI;

    private void Start()
    {
        deck = new List<CardData>();
        for(int i = 0; i < 10; i++)
        {
            deck.Add(new CardData("ATK", 2));
            deck.Add(new CardData("RUN", 1));
            deck.Add(new CardData("DEF", 1));
            deck.Add(new CardData("AAAA", 4));
        }
        hand = new List<CardData>();
        StartCoroutine(DrawCardRoutine());
    }

    private IEnumerator DrawCardRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(2f);
            DrawCard();
        }
    }

    public void DrawCard()
    {
        if (hand.Count >= handLimit)
        {
            Debug.Log("手牌已满，无法抽取新卡牌。");
            return;
        }

        if (deck.Count == 0)
        {
            Debug.Log("卡组为空，无法抽取新卡牌。");
            return;
        }

        CardData drawnCard = deck[0];
        deck.RemoveAt(0);
        hand.Add(drawnCard);
        Debug.Log($"抽取了卡牌：{drawnCard.cardName}");
        // 在此处添加将卡牌显示在 UI 上的代码，需要补一个是否是玩家的判定；
        handUI.AddCardToHand(drawnCard);
    }

    // 移除指定的卡牌数据
    public void RemoveCardFromHand(CardData cardData)
    {
        if (hand.Contains(cardData))
        {
            hand.Remove(cardData);
            Debug.Log($"卡牌 {cardData.cardName} 已从手牌中移除。");
        }
        else
        {
            Debug.LogWarning("尝试移除的卡牌不在手牌中。");
        }
    }
}