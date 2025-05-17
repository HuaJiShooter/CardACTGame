using System;
using System.Collections.Generic;
using UnityEngine;
using SQLite4Unity3d;
using System.Linq;

public class CardDB : MonoBehaviour
{
    private static SQLiteConnection Connection;

    //测试用函数，待删
    void Start()
    {
        //参数1.数据库地址，一般放在StreamingAssets文件夹中，2.开启读写和创建数据库权限
        Connection = new SQLiteConnection(Application.streamingAssetsPath + "/CardGame.db", SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create);
        Connection.CreateTable<CardsTable>();
        Connection.CreateTable<ModifiersTable>();
    }

    /// 在游戏启动时调用，负责创建连接和演示数据
    public static void Init()
    {
        string path = System.IO.Path.Combine(
            Application.persistentDataPath, "CardGame.db");

        Connection = new SQLiteConnection(path,
                 SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create);

        Connection.CreateTable<CardsTable>();
        Connection.CreateTable<ModifiersTable>();
    }

    /* ---------- 供业务代码调用 ---------- */
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