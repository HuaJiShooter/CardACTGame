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

        List<string> modifierList = new List<string> { "SameCardReduceCostBase" };
        List<string> conditionList = new List<string> { "SameCardInHand" };
        List<string> effectList = new List<string> { "ReduceCost-1" };


        ClearTable<CardsTable>(Connection);
        ClearTable<ModifiersTable>(Connection);
        // ④ 插入演示卡牌FireBall
        var demoCard1 = new CardsTable
        {
            CardName = "FireBall",
            Description = "远程 10 点火焰伤害，手中每有一张同名卡牌，费用则-1",
            Cost = 5,
            CardFace = "Sprites/Card_FireBall",
            ColdTime = 4f,
            WaitTime = -1f,
            ModifierList = modifierList.ToJson(),
            ObjectList = "[]"
        };
        Connection.Insert(demoCard1);

        // ④ 插入演示卡牌NormalCard
        var demoCard2 = new CardsTable
        {
            CardName = "NormalCard",
            Description = "1费的演示卡",
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
            Description = "每有一张同名卡牌，费用则-1",
            ConditionList = conditionList.ToJson(),
            EffectList = effectList.ToJson()
        };
        Connection.Insert(demoModifier);


        Debug.Log($"【TestInit】已写入");
    }

    public static void ClearTable<T>(SQLiteConnection conn)
    {
        string table = typeof(T).Name;               // 默认类名 = 表名
        try
        {
            // 1) 删除所有行
            conn.Execute($"DELETE FROM {table};");

            // 2) 重置自增主键序列（SQLite 特性）
            conn.Execute($"DELETE FROM sqlite_sequence WHERE name='{table}';");

            // 3) 可选：释放空间
            conn.Execute("VACUUM;");

            Debug.Log($"[DBTools] 清空表 {table} 成功");
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"[DBTools] 清空表 {table} 失败：{ex.Message}");
        }
    }

    private void OnDestroy()
    {
        Connection?.Close();
    }
}