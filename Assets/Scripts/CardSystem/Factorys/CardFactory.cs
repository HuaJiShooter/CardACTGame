using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static class CardFactory
{
    public static Card Create(string name, Object obj)
    {
        //通过CardName获取一个ModifiersTable行对象
        CardsTable row = CardDB.GetCardRow(name);

        var card = new Card(row, obj);

        return card;
    }
}