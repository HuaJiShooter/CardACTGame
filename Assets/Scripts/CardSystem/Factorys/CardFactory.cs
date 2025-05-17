using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static class CardFactory
{
    public static Card Create(string name, GameContext ctx)
    {
        //ͨ��CardName��ȡһ��ModifiersTable�ж���
        CardsTable row = CardDB.GetCardRow(name);

        var card = new Card(row, ctx);

        return card;
    }
}