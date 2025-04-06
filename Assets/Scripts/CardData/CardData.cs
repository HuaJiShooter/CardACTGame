using UnityEngine;

[CreateAssetMenu(fileName = "NewCard", menuName = "CardGame/CardData")]
public class CardData
{
    public string cardName;
    public int cost;

    public CardData(string name,int cost)
    {
        this.cardName = name;
        this.cost = cost;
    }
}
