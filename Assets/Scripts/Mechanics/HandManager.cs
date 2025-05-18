using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class HandManager : MonoBehaviour
{
    /*
    [Header("Deck Settings")]
    public List<Card.Data> deck = new List<Card.Data>(); // 这是抽排堆
    public List<Card.Data> hand = new List<Card.Data>(); // 这是手牌堆
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

    // 绘制卡的协程
    private IEnumerator DrawCardRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(2f);
            DrawCard();
        }
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }
    */
}