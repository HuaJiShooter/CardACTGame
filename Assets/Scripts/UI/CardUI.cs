using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardUI : MonoBehaviour
{
    [Header("UI References")]
    public TextMeshProUGUI cardName;
    public TextMeshProUGUI charge;

    [Header("Card Data")]
    private Card.Data _cardData; // 改为使用 Card.Data
    private HandManager _handManager;

    private void Start()
    {

    }

    // 修改为使用 Card.Data
    public void Setup(Card.Data data)
    {
        _cardData = data;
        cardName.text = data.cardName;
        charge.text = data.cost.ToString();

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
        if (_handManager != null && _cardData != null)
        {
            _handManager.RemoveCardFromHand(_cardData);
            Destroy(gameObject);
        }
        else
        {
            Debug.LogError("HandManager or CardData is missing!");
        }
    }

    // 公共属性供外部访问
    public Card.Data GetCardData() => _cardData;
}