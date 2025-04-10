using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CardData : ScriptableObject
{
    public string cardName;//卡牌名
    public int cost;//费用（光芒）
    public float cooldown;//冷却

    public string description;

    // Modifier数据存储（JSON序列化字段）
    [SerializeField] private string _triggerModifiersJson;
    [SerializeField] private string _conditionModifiersJson;
    [SerializeField] private string _effectModifiersJson;

    // 攻击模型数据和卡面（暂时留空）
    public int AttackPattern = 0;
    public int cardFace = 0;

    public CardData(string name,int cost,float cd)
    {
        this.cardName = name;
        this.cost = cost;
        this.cooldown = cd;
        description = "这是一张测试卡牌";
    }
}