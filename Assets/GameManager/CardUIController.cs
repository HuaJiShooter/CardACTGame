using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CardUIController : MonoBehaviour, IPointerClickHandler
{
    [Header("UI组件")]
    public Text cardNameText;
    public Text costText;
    public Text descriptionText;
    public Image cardImage;

    // 关联的卡牌对象
    private Card _associatedCard;
    private HandController _handController;

    // 初始化UI
    public void Initialize(Card card, HandController handController)
    {
        _associatedCard = card;
        _handController = handController;

        // 更新UI显示
        cardNameText.text = card.cardData.cardName;
        costText.text = card.cardData.cost.ToString();
        descriptionText.text = card.cardData.baseEffect.effectDescription;

        // 这里可以添加根据卡牌类型设置不同颜色等
        switch (card.cardData.cardType)
        {
            case Card.Data.CardType.Attack:
                cardImage.color = Color.red;
                break;
            case Card.Data.CardType.Defense:
                cardImage.color = Color.blue;
                break;
                // 其他类型...
        }
    }

    // 点击事件处理
    public void OnPointerClick(PointerEventData eventData)
    {
        if (_associatedCard == null) return;

        // 通知手牌控制器使用这张卡牌
        _handController.UseCard(this, _associatedCard);
    }

    // 销毁卡牌UI
    public void Discard()
    {
        Destroy(gameObject);
    }
}