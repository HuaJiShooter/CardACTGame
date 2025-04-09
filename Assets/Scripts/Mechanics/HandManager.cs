using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class HandManager : MonoBehaviour
{
    [Header("Deck Settings")]
    public List<Card.Data> deck = new List<Card.Data>(); // 使用 Card.Data
    public List<Card.Data> hand = new List<Card.Data>();
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
        for (int i = 0; i < 10; i++)
        {
            deck.Add(CreateCardData("ATK", 2));
            deck.Add(CreateCardData("RUN", 1));
            deck.Add(CreateCardData("DEF", 1));
            deck.Add(CreateCardData("AAAA", 4));
        }
    }

    private Card.Data CreateCardData(string name, int cost)
    {
        Card.Data data = ScriptableObject.CreateInstance<Card.Data>();
        data.cardName = name;
        data.cost = cost;
        // 设置其他默认值...
        return data;
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
            Debug.Log("手牌已达上限");
            return;
        }

        if (deck.Count == 0)
        {
            Debug.Log("牌库为空");
            return;
        }

        Card.Data drawnCard = deck[0];
        deck.RemoveAt(0);
        hand.Add(drawnCard);

        if (handUI != null)
        {
            handUI.AddCardToHand(drawnCard);
        }
    }

    public void RemoveCardFromHand(Card.Data cardData)
    {
        if (hand.Contains(cardData))
        {
            hand.Remove(cardData);
            Debug.Log($"移除卡牌: {cardData.cardName}");
        }
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}