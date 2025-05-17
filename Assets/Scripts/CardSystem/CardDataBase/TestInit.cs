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
        //����1.���ݿ��ַ��һ�����StreamingAssets�ļ����У�2.������д�ʹ������ݿ�Ȩ��
        Connection = new SQLiteConnection(Application.streamingAssetsPath + "/CardGame.db", SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create);
        Connection.CreateTable<CardsTable>();
        Connection.CreateTable<ModifiersTable>();
    }

}
