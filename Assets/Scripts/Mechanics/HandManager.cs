using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class HandManager : MonoBehaviour
{
    [Header("Deck Settings")]
    public List<Card> deck = new List<Card>(); // 这是抽排堆
    public List<Card> hand = new List<Card>(); // 这是手牌堆
    public List<Card> fold = new List<Card>(); // 这是弃牌堆
    public int handLimit = 10;

    [Header("References")]
    public HandUI handUI;

    private void Start()
    {
        InitializeDeck();
        StartCoroutine(DrawCardRoutine());
    }

    private void InitializeDeck()
    {
        // 示例卡牌创建
        for (int i = 0; i < 1; i++)
        {
            deck.Add(CreateCard("ATK", 2));
            deck.Add(CreateCard("RUN", 1));
            deck.Add(CreateCard("DEF", 1));
            deck.Add(CreateCard("AAAA", 4));
        }
    }

    private Card CreateCard(string name, int cost)
    {

        Card card = new Card(name,cost);
        CardData carddata = card.CardData;

        carddata.cardName = name;
        carddata.cost = cost;

        // 设置其他默认值...
        return card;
    }

    // 抽卡的协程
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
            Debug.Log("手牌已达上限");
            return;
        }

        if (deck.Count == 0)
        {
            Debug.Log("牌库为空");
            return;
        }

        //获取抽牌堆的第一张牌，并将其移至手牌堆中
        Card drawnCard = deck[0];
        deck.RemoveAt(0);
        hand.Add(drawnCard);

        if (handUI != null)
        {
            Debug.Log($"抽取卡牌:{drawnCard.CardData.cardName}");
            handUI.AddCardToHand(drawnCard);
        }
    }

    public void RemoveCardFromHand(Card card)
    {

        if (hand.Contains(card))
        {
            hand.Remove(card);
            Debug.Log($"移除卡牌: {card.CardData.cardName}");
        }

    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}