using System;
using System.Collections.Generic;
using UnityEngine;
using SQLite4Unity3d;
using System.Linq;

public static class CardDB
{
    private static SQLiteConnection Connection;


    /// ����Ϸ����ʱ���ã����𴴽����Ӻ���ʾ����
    public static void Init()
    {
        Connection = new SQLiteConnection(Application.streamingAssetsPath + "/CardGame.db", SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create);

        Connection.CreateTable<CardsTable>();
        Connection.CreateTable<ModifiersTable>();
    }

    /* ---------- ��ҵ�������� ---------- */
    public static CardsTable GetCardRow(string name)
    {
        Debug.Log(name);

        CardsTable row = Connection.Table<CardsTable>()
                       .Where(_ => _.CardName == name).FirstOrDefault();

        if (row == null) throw new Exception($"Card {name} not found");

        return row;
    }

    public static ModifiersTable GetModifierRow(string name)
    {

        var row = Connection.Table<ModifiersTable>()
                       .FirstOrDefault(_ => _.ModifierName == name);
        if (row == null) throw new Exception($"Modifier {name} not found");

        return row;
    }
}



[System.Serializable]
public class Wrapper<T>
{
    public List<T> list;
}