using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardUI : MonoBehaviour
{
    [Header("UI References")]
    public TextMeshProUGUI cardName;
    public TextMeshProUGUI charge;
    public TextMeshProUGUI descriptionText;

    [Header("CardUI Data")]
    public Card associate_card;
    private CardUIController _cardUIController;

    private void Start()
    {

    }

    //设置卡牌至手牌区
    public void Setup(Card card, CardUIController controller)
    {
        Debug.Log("正在生成cardUI");
        associate_card = card;
        _cardUIController = controller;
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

    //设置卡牌至手牌区
    public void Remove()
    {
        Debug.Log("正在移除cardUI");

        // 销毁该UI对象
        Destroy(this.gameObject);
    }

    private void OnCardClicked()
    {
        Debug.Log("卡牌被点击");
        if (associate_card != null)
        {
            _cardUIController.UseCard(this);
        }
        else
        {
            Debug.LogError("CardData is missing!");
        }
    }

}