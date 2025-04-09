using UnityEngine;

public class Card
{
    // 内部类 - 卡牌数据
    [System.Serializable]
    public class Data : ScriptableObject
    {
        public string cardName;//卡牌名
        public int cost;//费用（光芒）
        public float cooldown;//cd

        public enum CardType
        {
            Attack,//攻击
            Defense, //防御
            Recovery, //回转
            Support//辅助
        }
        public CardType cardType;

        [System.Serializable]
        public struct BaseEffect//基础效果结构
        {
            public string effectDescription;//效果描述
            public float attackMultiplier;//攻击倍率
            public float defenseMultiplier;//防御倍率
        }
        public BaseEffect baseEffect;

        [System.Serializable]
        public struct AdditionalEffect
        {
            public enum EffectType//额外效果
            {
                Buff,//增益
                Debuff,//减益
                Heal,//治疗
                DamageOverTime, //持续伤害（dot)
                Stun, //触发概率（0-1）
                DrawCard//效果描述
            }

            public EffectType effectType;//额外效果结构
            public float value;//效果数值
            public int duration;//持续时间（回合数）
            public float probability;//触发概率（0-1）
            public string description;//效果描述
        }

        public AdditionalEffect[] additionalEffects;//额外效果数组

        public float CalculateTotalAttack(float baseAttack)// 计算总攻击力(基础攻击力 * 攻击倍率)
        {
            return baseAttack * baseEffect.attackMultiplier;
        }

        public float CalculateTotalDefense(float baseDefense)//计算总防御力(基础防御力* 防御倍率)
        {
            return baseDefense * baseEffect.defenseMultiplier;
        }
    }

    // 卡牌实例属性
    public Data cardData { get; private set; }
    public bool isExhausted { get; private set; } // 是否已使用

    // 构造函数
    public Card(Data data)
    {
        this.cardData = data;
        this.isExhausted = false;
    }

    // 使用卡牌
    public void Use()
    {
        if (isExhausted) return;

        OnUse(); // 调用使用效果
        isExhausted = true;
    }

    // 卡牌使用效果（子类重写）
    protected virtual void OnUse()
    {
        // 基础效果实现
        Debug.Log($"使用卡牌: {cardData.cardName}");

        // 这里可以添加基础效果逻辑

        // 处理额外效果
        foreach (var effect in cardData.additionalEffects)
        {
            if (Random.value <= effect.probability)
            {
                ApplyAdditionalEffect(effect);
            }
        }
    }

    // 应用额外效果
    protected virtual void ApplyAdditionalEffect(Data.AdditionalEffect effect)
    {
        switch (effect.effectType)
        {
            case Data.AdditionalEffect.EffectType.Buff:
                Debug.Log($"应用增益效果: {effect.description}");
                break;
            case Data.AdditionalEffect.EffectType.Debuff:
                Debug.Log($"应用减益效果: {effect.description}");
                break;
            case Data.AdditionalEffect.EffectType.Heal:
                Debug.Log($"治疗效果: {effect.value}");
                break;
                // 其他效果类型处理...
        }
    }

    // 重置卡牌状态（从弃牌堆回收时调用）
    public void Reset()
    {
        isExhausted = false;
    }
}
