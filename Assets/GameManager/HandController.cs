using UnityEngine;
using System.Collections.Generic;

public class HandController : MonoBehaviour
{
    [Header("UI设置")]
    public GameObject cardUIPrefab;
    public Transform cardsContainer;

    [Header("卡牌位置设置")]
    public float cardSpacing = 100f;
    public float cardScale = 1f;

    // 当前手牌
    private List<Card> _handCards = new List<Card>();
    private List<CardUIController> _cardUIList = new List<CardUIController>();

    // 添加卡牌到手牌
    public void AddCardToHand(Card card)
    {
        _handCards.Add(card);

        // 创建UI
        GameObject cardUI = Instantiate(cardUIPrefab, cardsContainer);
        CardUIController controller = cardUI.GetComponent<CardUIController>();
        controller.Initialize(card, this);

        _cardUIList.Add(controller);

        // 更新手牌布局
        UpdateHandLayout();
    }

    // 使用卡牌
    public void UseCard(CardUIController cardUI, Card card)
    {
        // 执行卡牌效果
        card.Use();

        // 通知游戏管理器将卡牌移至弃牌堆
        GameManager.Instance.MoveToDiscardPile(card);

        // 从手牌中移除
        int index = _cardUIList.IndexOf(cardUI);
        if (index >= 0)
        {
            _handCards.RemoveAt(index);
            _cardUIList.RemoveAt(index);
            cardUI.Discard();
        }

        // 更新手牌布局
        UpdateHandLayout();
    }

    // 更新手牌布局
    private void UpdateHandLayout()
    {
        float totalWidth = (_handCards.Count - 1) * cardSpacing;
        float startX = -totalWidth / 2f;

        for (int i = 0; i < _cardUIList.Count; i++)
        {
            float xPos = startX + i * cardSpacing;
            _cardUIList[i].transform.localPosition = new Vector3(xPos, 0, 0);
            _cardUIList[i].transform.localScale = Vector3.one * cardScale;
        }
    }
}