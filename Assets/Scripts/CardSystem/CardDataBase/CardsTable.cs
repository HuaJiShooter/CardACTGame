using SQLite4Unity3d;

[Table("CardsTable")]
public class CardsTable
{
    [PrimaryKey, AutoIncrement]              //card_ID
    public int CardId { get; set; }

    [MaxLength(64)]                          //card_name
    public string CardName { get; set; }

    [MaxLength(1024)]                        //description
    public string Description { get; set; } = "此卡牌暂无描述";

    public int Cost { get; set; } = 0;  //便于按费用检索

    public string CardFace { get; set; }  //图片或地址

    public float ColdTime { get; set; } = 0f;   //冷却

    public float WaitTime { get; set; } = -1f;  //等待

    /* 以 JSON 字符串保存可变长度数据 */
    public string ModifierList { get; set; }  //["M1","M2", …]

    public string ObjectList { get; set; }  //自定义对象挂点

    public override string ToString()
    {
        return $"[Card: Id={CardId}, Name={CardName}, Cost={Cost}, Cold={ColdTime}, Wait={WaitTime}]";
    }
}
