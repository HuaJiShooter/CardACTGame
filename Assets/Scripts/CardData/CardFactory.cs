using UnityEngine;
using System.Collections.Generic;

public class CardFactory : MonoBehaviour
{
    // 卡牌数据库（ScriptableObject资源）（暂时留空）
    public int cardDatabase = 0;

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

    }

    // 根据ID创建卡牌
    public Card CreateCard(string cardId)
    {
        //根据ID创建卡牌
        return new Card("测试卡牌",10);
    }
}