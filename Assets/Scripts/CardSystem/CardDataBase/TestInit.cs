using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SQLite4Unity3d;

public class TestInit : MonoBehaviour
{
    public SQLiteConnection Connection;
    // Start is called before the first frame update
    void Start()
    {
        //参数1.数据库地址，一般放在StreamingAssets文件夹中，2.开启读写和创建数据库权限
        Connection = new SQLiteConnection(Application.streamingAssetsPath + "/CardGame.db", SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create);
        Connection.CreateTable<CardsTable>();
        Connection.CreateTable<ModifiersTable>();
    }

}
