using UnityEngine;
using System.Collections.Generic;

public class CardFactory : MonoBehaviour
{
    // 卡牌数据库（ScriptableObject资源）
    public CardDatabase cardDatabase;

    // 单例模式
    private static CardFactory _instance;
    public static CardFactory Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<CardFactory>();
                if (_instance == null)
                {
                    GameObject obj = new GameObject("CardFactory");
                    _instance = obj.AddComponent<CardFactory>();
                }
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(gameObject);

        // 加载卡牌数据库资源
        if (cardDatabase == null)
        {
            cardDatabase = Resources.Load<CardDatabase>("CardDatabase");
        }
    }

    // 根据ID创建卡牌
    public Card CreateCard(string cardId)
    {
        Card.Data Data = cardDatabase.GetCardData(cardId);
        if (Data == null)
        {
            Debug.LogError($"卡牌ID {cardId} 不存在于数据库中");
            return null;
        }

        // 这里可以根据卡牌类型创建不同的子类实例
        return new Card(Data);
    }

    // 随机创建卡牌
    public Card CreateRandomCard()
    {
        if (cardDatabase == null || cardDatabase.allCards.Length == 0)
        {
            Debug.LogError("卡牌数据库为空或未加载");
            return null;
        }

        int randomIndex = Random.Range(0, cardDatabase.allCards.Length);
        return new Card(cardDatabase.allCards[randomIndex]);
    }
}

// 卡牌数据库ScriptableObject
[CreateAssetMenu(fileName = "CardDatabase", menuName = "CardGame/CardDatabase")]
public class CardDatabase : ScriptableObject
{
    public Card.Data[] allCards;

    public Card.Data GetCardData(string cardId)
    {
        foreach (var card in allCards)
        {
            if (card.name == cardId)
            {
                return card;
            }
        }
        return null;
    }
}