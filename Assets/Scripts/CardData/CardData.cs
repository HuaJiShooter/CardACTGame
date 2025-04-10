using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CardData : ScriptableObject
{
    public string cardName;//������
    public int cost;//���ã���â��
    public float cooldown;//��ȴ

    public string description;

    // Modifier���ݴ洢��JSON���л��ֶΣ�
    [SerializeField] private string _triggerModifiersJson;
    [SerializeField] private string _conditionModifiersJson;
    [SerializeField] private string _effectModifiersJson;

    // ����ģ�����ݺͿ��棨��ʱ���գ�
    public int AttackPattern = 0;
    public int cardFace = 0;

    public CardData(string name,int cost,float cd)
    {
        this.cardName = name;
        this.cost = cost;
        this.cooldown = cd;
        description = "����һ�Ų��Կ���";
    }
}