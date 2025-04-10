using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardUI : MonoBehaviour
{
    [Header("UI References")]
    public TextMeshProUGUI cardName;
    public TextMeshProUGUI charge;

    public Card card;


    private HandUI _handUI;



    //在UI中创建卡牌
    public void Setup(Card card,HandUI handUI)
    {

        //初始化数据
        this.card = card;
        cardName.text = card.CardData.cardName;
        charge.text = card.CardData.cost.ToString();
        _handUI = handUI;

        Button button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(OnCardClicked);
        }
        else
        {
            Debug.LogError("Button component not found on card!");
        }
    }

    //卡牌被使用时的触发
    private void OnCardClicked()
    {

        Debug.Log("卡牌被点击");
        if (_handUI != null && card != null)
        {
            //使用这张卡，先通知HandUI移出手牌
            _handUI.RemoveCardFromHand(gameObject);

            //然后通知HandManager将这张牌放入弃牌堆并使用它
        }
        else
        {
            Debug.LogError("HandManager or CardData is missing!");
        }

    }


}