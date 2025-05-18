using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CardUIController : MonoBehaviour
{
    [Header("UI组件")]
    public GameObject cardUIPrefab;//卡牌UI预制体
    public Image cardImage;

    [Header("References")]
    public Transform handPanel;
    public HandUI handUI;

    // 关联的对象
    private HandController _handController;
    public Text cardNameText;
    public Text costText;
    public Text descriptionText;

    private void Start()
    {
        
    }

    // 初始化UI
    public void Initialize(Card card, HandController handController)
    {
        Card _associatedCard = card;
        _handController = handController;

        // 更新UI显示
        cardNameText.text = card.cardData.CardName;
        costText.text = card.curCost.ToString();
        descriptionText.text = card.cardData.Description;
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