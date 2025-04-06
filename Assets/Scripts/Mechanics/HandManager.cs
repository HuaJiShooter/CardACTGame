using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class HandManager : MonoBehaviour
{
    public List<CardData> deck; // ����
    public List<CardData> hand; // ��ǰ����
    public int handLimit = 10;  // ��������

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
            Debug.Log("�����������޷���ȡ�¿��ơ�");
            return;
        }

        if (deck.Count == 0)
        {
            Debug.Log("����Ϊ�գ��޷���ȡ�¿��ơ�");
            return;
        }

        CardData drawnCard = deck[0];
        deck.RemoveAt(0);
        hand.Add(drawnCard);
        Debug.Log($"��ȡ�˿��ƣ�{drawnCard.cardName}");
        // �ڴ˴���ӽ�������ʾ�� UI �ϵĴ��룬��Ҫ��һ���Ƿ�����ҵ��ж���
        handUI.AddCardToHand(drawnCard);
    }

    // �Ƴ�ָ���Ŀ�������
    public void RemoveCardFromHand(CardData cardData)
    {
        if (hand.Contains(cardData))
        {
            hand.Remove(cardData);
            Debug.Log($"���� {cardData.cardName} �Ѵ��������Ƴ���");
        }
        else
        {
            Debug.LogWarning("�����Ƴ��Ŀ��Ʋ��������С�");
        }
    }
}