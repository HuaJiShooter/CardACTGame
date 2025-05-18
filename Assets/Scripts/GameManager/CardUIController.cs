using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Linq;

public class CardUIController : MonoBehaviour
{
    [Header("UI组件")]
    public GameObject cardUIPrefab;//卡牌UI预制体
    public Image cardImage;

    [Header("References")]
    public Transform handPanel;
    public HandUI handUI;
    private HandController _handController;


    private void Start()
    {
        _handController = GameObject.Find("Player").GetComponent<HandController>();
        EventBus.Subscribe(GameEvt.HandChanged, RefreshHandErea);
    }


    //手牌变动，刷新手牌区域
    public void RefreshHandErea(GameEvent e)
    {
        Debug.Log("刷新手牌区域中...");
        var currentUICards = handPanel.GetComponentsInChildren<CardUI>(includeInactive: false);
        var currentHandCards = _handController._handPile;

        foreach (var cardUI in currentUICards)
        {
            RemoveCardFromHand(cardUI.associate_card);
        }

        foreach (var card in currentHandCards)
        {
            AddCardToHand(card);
        }
    }


    // 点击使用卡牌事件处理
    public void UseCard(CardUI cardUI)
    {
        Debug.Log("点击了卡牌");
    }

    // 添加卡牌到手牌（UI层面）
    public void AddCardToHand(Card card)
    {
        if (cardUIPrefab == null || handPanel == null) return;

        GameObject cardObj = Instantiate(cardUIPrefab, handPanel);    //在handPanel生成预制体
        CardUI cardUI = cardObj.GetComponent<CardUI>();

        if (cardUI != null)
        {
            cardUI.Setup(card);
        }
    }

    public void RemoveCardFromHand(Card card)
    {
        foreach (Transform child in handPanel)
        {
            CardUI cardUI = child.GetComponent<CardUI>();
            if (cardUI != null)
            {
                Destroy(child.gameObject);
                return;
            }
        }
    }

    // 销毁卡牌UI
    public void Discard()
    {
        Destroy(gameObject);
    }
}