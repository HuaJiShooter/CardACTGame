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

        List<string> modifierList = new List<string> { "SameCardReduceCostBase" };
        List<string> conditionList = new List<string> { "SameCardInHand" };
        List<string> effectList = new List<string> { "ReduceCost-1" };


        ClearTable<CardsTable>(Connection);
        ClearTable<ModifiersTable>(Connection);
        // �� ������ʾ����FireBall
        var demoCard1 = new CardsTable
        {
            CardName = "FireBall",
            Description = "Զ�� 10 ������˺�������ÿ��һ��ͬ�����ƣ�������-1",
            Cost = 5,
            CardFace = "Sprites/Card_FireBall",
            ColdTime = 4f,
            WaitTime = -1f,
            ModifierList = modifierList.ToJson(),
            ObjectList = "[]"
        };
        Connection.Insert(demoCard1);

        // �� ������ʾ����NormalCard
        var demoCard2 = new CardsTable
        {
            CardName = "NormalCard",
            Description = "1�ѵ���ʾ��",
            Cost = 1,
            CardFace = "Sprites/Card_FireBall",
            ColdTime = 4f,
            WaitTime = -1f,
            ModifierList = null,
            ObjectList = "[]"
        };
        Connection.Insert(demoCard2);


        var demoModifier = new ModifiersTable
        {
            ModifierName = "SameCardReduceCostBase",
            Description = "ÿ��һ��ͬ�����ƣ�������-1",
            ConditionList = conditionList.ToJson(),
            EffectList = effectList.ToJson()
        };
        Connection.Insert(demoModifier);


        Debug.Log($"��TestInit����д��");
    }

    public static void ClearTable<T>(SQLiteConnection conn)
    {
        string table = typeof(T).Name;               // Ĭ������ = ����
        try
        {
            // 1) ɾ��������
            conn.Execute($"DELETE FROM {table};");

            // 2) ���������������У�SQLite ���ԣ�
            conn.Execute($"DELETE FROM sqlite_sequence WHERE name='{table}';");

            // 3) ��ѡ���ͷſռ�
            conn.Execute("VACUUM;");

            Debug.Log($"[DBTools] ��ձ� {table} �ɹ�");
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"[DBTools] ��ձ� {table} ʧ�ܣ�{ex.Message}");
        }
    }

    private void OnDestroy()
    {
        Connection?.Close();
    }
}