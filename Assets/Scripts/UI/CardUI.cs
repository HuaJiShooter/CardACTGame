using UnityEngine;
using UnityEngine.UI;
using TMPro; // 使用 TextMeshPro

public class CardUI : MonoBehaviour
{
    public TextMeshProUGUI cardName;
    public TextMeshProUGUI charge;
    private CardData cardData;
    public HandManager handManager;
    private void Start()
    {
        GameObject targetGameObject = GameObject.Find("Player");
        handManager = targetGameObject.GetComponent<HandManager>();
    }

    public void Setup(CardData data)
    {
        cardData = data;
        cardName.text = data.cardName;
        charge.text = data.cost.ToString();
        GetComponent<Button>().onClick.AddListener(OnCardClicked);
    }

    private void OnCardClicked()
    {
        Debug.Log("测试");
        // 这里可以添加使用卡牌的逻辑，例如：
        // 检查费用是否足够，执行卡牌效果等
        //Debug.Log($"使用了卡牌：{cardData.cardName}");
        //cardData.effect?.Invoke();
        // 使用后销毁卡牌
        handManager.RemoveCardFromHand(cardData);
        Destroy(gameObject);
    }
}