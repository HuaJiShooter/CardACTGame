using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardUI : MonoBehaviour
{
    [Header("UI References")]
    public TextMeshProUGUI cardName;
    public TextMeshProUGUI charge;
    public TextMeshProUGUI descriptionText;

    [Header("Card Data")]
    public Card associate_card;
    private CardUIController _cardUIController;

    private void Start()
    {

    }

    //设置卡牌至手牌区
    public void Setup(Card card)
    {
        Debug.Log("正在生成cardUI");
        associate_card = card;
        cardName.text = card.cardData.CardName;
        charge.text = card.curCost.ToString();

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

    private void OnCardClicked()
    {
        Debug.Log("卡牌被点击");
        if (_cardUIController != null && associate_card != null)
        {
            _cardUIController.UseCard(this);
        }
        else
        {
            Debug.LogError("HandManager or CardData is missing!");
        }
    }

}