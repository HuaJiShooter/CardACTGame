using UnityEngine;
using UnityEngine.UI;
using TMPro; // ʹ�� TextMeshPro

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
        Debug.Log("����");
        // ����������ʹ�ÿ��Ƶ��߼������磺
        // �������Ƿ��㹻��ִ�п���Ч����
        //Debug.Log($"ʹ���˿��ƣ�{cardData.cardName}");
        //cardData.effect?.Invoke();
        // ʹ�ú����ٿ���
        handManager.RemoveCardFromHand(cardData);
        Destroy(gameObject);
    }
}