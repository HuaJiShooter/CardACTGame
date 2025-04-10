using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class GameManager : MonoBehaviour
{
    // 单例实例
    public static GameManager Instance { get; private set; }

    // 牌堆列表
    private List<Card> _drawPile = new List<Card>();
    private List<Card> _discardPile = new List<Card>();
    private List<Card> _handPile = new List<Card>();

    // 所有卡牌数据资源（在Inspector中赋值）
    [SerializeField] private Card.Data[] _allCardData;
    // 在 GameManager 类中添加：
    public void MoveToDiscardPile(Card card)
    {
        if (!_handPile.Contains(card))
        {
            Debug.LogWarning("无法弃牌：卡牌不在手牌中");
            return;
        }

        _handPile.Remove(card);
        _discardPile.Add(card);
        Debug.Log($"弃掉卡牌: {card.cardData.cardName}");
    }
    private void Awake()
    {
        // 单例初始化
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
            // 可选：如果希望GameManager跨场景存在
            // DontDestroyOnLoad(this.gameObject);
        }
    }

    void Start()
    {
        InitializeCardSystem();
    }

    // 初始化卡牌系统
    public void InitializeCardSystem()
    {
        // 1. 加载所有卡牌数据（如果没在Inspector中赋值）
        if (_allCardData == null || _allCardData.Length == 0)
        {
            _allCardData = Resources.LoadAll<Card.Data>("CardData");
            if (_allCardData == null || _allCardData.Length == 0)
            {
                Debug.LogError("没有找到卡牌数据！");
                return;
            }
        }

        // 2. 创建卡牌实例并初始化抽牌堆
        InitializeDrawPile();

        // 3. 初始抽牌（例如抽5张）
        DrawCards(5);
    }

    // 初始化抽牌堆
    private void InitializeDrawPile()
    {
        _drawPile.Clear();

        // 为每个Card.Data创建Card实例
        foreach (var data in _allCardData)
        {
            // 每种卡牌创建3张实例（示例）
            for (int i = 0; i < 3; i++)
            {
                _drawPile.Add(new Card(data));
            }
        }

        // 洗牌
        Shuffle(_drawPile);
    }

    // 洗牌算法
    private void Shuffle(List<Card> deck)
    {
        for (int i = 0; i < deck.Count; i++)
        {
            Card temp = deck[i];
            int randomIndex = Random.Range(i, deck.Count);
            deck[i] = deck[randomIndex];
            deck[randomIndex] = temp;
        }
    }

    // 抽指定数量的牌
    public void DrawCards(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            if (_drawPile.Count == 0)
            {
                ReshuffleDiscardPile();
                if (_drawPile.Count == 0)
                {
                    Debug.LogWarning("牌堆已空，无法抽牌");
                    return;
                }
            }

            Card drawnCard = _drawPile[0];
            _drawPile.RemoveAt(0);
            _handPile.Add(drawnCard);

            Debug.Log($"抽到卡牌: {drawnCard.cardData.cardName}");
        }
    }

    // 重洗弃牌堆
    private void ReshuffleDiscardPile()
    {
        Debug.Log("抽牌堆空了，重新洗入弃牌堆...");

        _drawPile.AddRange(_discardPile);
        _discardPile.Clear();

        foreach (var card in _drawPile)
        {
            card.Reset(); // 重置所有卡牌状态
        }

        Shuffle(_drawPile);
    }

    // 使用卡牌
    public void UseCard(Card card)
    {
        if (!_handPile.Contains(card))
        {
            Debug.LogWarning("尝试使用不在手中的卡牌");
            return;
        }

        card.Use();
        _handPile.Remove(card);
        _discardPile.Add(card);

        Debug.Log($"使用卡牌: {card.cardData.cardName}");
    }

    // 示例：获取当前手牌（UI显示用）
    public IEnumerable<Card> GetHandCards()
    {
        return _handPile.AsReadOnly();
    }

    // 获取抽牌堆剩余数量（UI显示用）
    public int GetDrawPileCount() => _drawPile.Count;

    // 获取弃牌堆数量（UI显示用）
    public int GetDiscardPileCount() => _discardPile.Count;
}