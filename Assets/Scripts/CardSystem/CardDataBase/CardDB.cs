using System;
using System.Collections.Generic;
using UnityEngine;
using SQLite4Unity3d;
using System.Linq;

public class CardDB : MonoBehaviour
{
    private static SQLiteConnection Connection;

    //�����ú�������ɾ
    void Start()
    {
        //����1.���ݿ��ַ��һ�����StreamingAssets�ļ����У�2.������д�ʹ������ݿ�Ȩ��
        Connection = new SQLiteConnection(Application.streamingAssetsPath + "/CardGame.db", SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create);
        Connection.CreateTable<CardsTable>();
        Connection.CreateTable<ModifiersTable>();
    }

    /// ����Ϸ����ʱ���ã����𴴽����Ӻ���ʾ����
    public static void Init()
    {
        string path = System.IO.Path.Combine(
            Application.persistentDataPath, "CardGame.db");

        Connection = new SQLiteConnection(path,
                 SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create);

        Connection.CreateTable<CardsTable>();
        Connection.CreateTable<ModifiersTable>();
    }

    /* ---------- ��ҵ�������� ---------- */
    public static CardsTable GetCardRow(string name)
    {

        CardsTable row = Connection.Table<CardsTable>()
                       .FirstOrDefault(_ => _.CardName == name);
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