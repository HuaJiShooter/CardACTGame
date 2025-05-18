using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;

public class HandController : MonoBehaviour
{

    // 卡组
    private List<string> _cardNameList = new List<string>();


    // 牌堆列表
    public List<Card> _drawPile = new List<Card>();    //抽牌堆
    public List<Card> _discardPile = new List<Card>(); //弃牌堆
    public ObservableList<Card> _handPile = new ObservableList<Card>();  //手牌堆
    public List<Card> _waitPile = new List<Card>();   //待释放卡牌队列



    // 抽牌协程
    private IEnumerator DrawCardRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(2f);
            DrawCards(1);
        }
    }

    // 初始化抽牌堆
    public void InitializeDrawPile(List<string> _cardNameList, UnityEngine.Object obj)
    {
        Debug.Log("进入初始化牌组方法");
        this._cardNameList = _cardNameList;

        _drawPile.Clear();

        // 由每个名创建Card实例
        foreach (string cardName in _cardNameList)
        {
            _drawPile.Add(CardFactory.Create(cardName, obj));
        }

        // 洗牌
        Shuffle(_drawPile);

        //事件与初始化协程
        _handPile.OnListChanged += HandleHandChanged;
        StartCoroutine(DrawCardRoutine());

        Debug.Log("初始化牌组完毕");
    }


    // 弃牌
    public void MoveToDiscardPile(Card card)
    {
        if (!_handPile.Contains(card))
        {
            Debug.LogWarning("无法弃牌：卡牌不在手牌中");
            return;
        }

        _handPile.Remove(card);
        _discardPile.Add(card);
        Debug.Log($"弃掉卡牌: {card.cardData.CardName}");
    }

    // 回到洗排堆
    public void BackToDrawPile(Card card)
    {
        
    }

    // 从抽牌堆抽指定数量的牌
    public void DrawCards(int amount)
    {
        Debug.Log("进入抽牌函数");
        for (int i = 0; i < amount; i++)
        {
            if (_drawPile.Count == 0)
            {
                if (_drawPile.Count == 0)
                {
                    Debug.LogWarning("牌堆已空，无法抽牌");
                    return;
                }
            }
            Card drawnCard = _drawPile[0];
            _drawPile.RemoveAt(0);
            _handPile.Add(drawnCard);
            PublishDrawCard(drawnCard);


            Debug.Log($"抽到卡牌: {drawnCard.cardData.CardName}");
        }
    }


    // 洗牌算法
    private void Shuffle(List<Card> deck)
    {
        for (int i = 0; i < deck.Count; i++)
        {
            Card temp = deck[i];
            int randomIndex = UnityEngine.Random.Range(i, deck.Count);
            deck[i] = deck[randomIndex];
            deck[randomIndex] = temp;
        }
    }

    // 重洗弃牌堆
    private void ReshuffleDiscardPile()
    {
        Debug.Log("抽牌堆空了");

        _drawPile.AddRange(_discardPile);
        _discardPile.Clear();

        Shuffle(_drawPile);
    }


    // 使用卡牌
    public void UseCard(CardUIController cardUI, Card card, CardUseContext ctx)
    {
        // 执行卡牌效果
        card.Play(ctx);
        // 移至弃牌堆
        MoveToDiscardPile(card);
    }



    //以下为事件广播
    private void PublishDrawCard(Card drawncard)
    {

        Debug.Log("触发抽牌事件");

        EventBus.Publish(new GameEvent
        {
            Type = GameEvt.DrawCard,
            Source = this,
            Payload = drawncard,      // 直接把当前手牌列表一起抛出
            Context = new CardUseContext(gameObject)
        });
    }

    void HandleHandChanged()
    {

        Debug.Log("触发卡牌变化事件");

        EventBus.Publish(new GameEvent
        {                      // ② 后广播给 Condition
            Type = GameEvt.HandChanged,
            Source = this,
            Context = new CardUseContext(gameObject)
        });
    }
}

// 卡牌系统通用上下文
public class CardUseContext
{
    public GameObject Owner;
    public CardUseContext(GameObject _owner)
    {
        Owner = _owner;
    }

}

// 元素变化会自动触发事件的牌堆类
public class ObservableList<T> : IList<T>
{
    readonly List<T> _inner = new();

    public event Action OnListChanged;          // 列表变动时调用

    /* --- IList<T> 的核心实现 --- */
    public T this[int index] { get => _inner[index]; set { _inner[index] = value; Fire(); } }

    public int Count => _inner.Count;
    public bool IsReadOnly => false;

    public void Add(T item) { _inner.Add(item); Fire(); }
    public void Clear() { if (_inner.Count > 0) { _inner.Clear(); Fire(); } }
    public bool Remove(T item) { bool ok = _inner.Remove(item); if (ok) Fire(); return ok; }

    public bool Contains(T item) => _inner.Contains(item);
    public void CopyTo(T[] array, int i) => _inner.CopyTo(array, i);
    public int IndexOf(T item) => _inner.IndexOf(item);
    public void Insert(int i, T item) { _inner.Insert(i, item); Fire(); }
    public void RemoveAt(int i) { _inner.RemoveAt(i); Fire(); }

    public IEnumerator<T> GetEnumerator() => _inner.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => _inner.GetEnumerator();
    void Fire() => OnListChanged?.Invoke();
}