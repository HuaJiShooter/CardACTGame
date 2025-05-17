using SQLite4Unity3d;

[Table("CardsTable")]
public class CardsTable
{
    [PrimaryKey, AutoIncrement]              //card_ID
    public int CardId { get; set; }

    [MaxLength(64)]                          //card_name
    public string CardName { get; set; }

    [MaxLength(1024)]                        //description
    public string Description { get; set; } = "�˿�����������";

    public int Cost { get; set; } = 0;  //���ڰ����ü���

    public string CardFace { get; set; }  //ͼƬ���ַ

    public float ColdTime { get; set; } = 0f;   //��ȴ

    public float WaitTime { get; set; } = -1f;  //�ȴ�

    /* �� JSON �ַ�������ɱ䳤������ */
    public string ModifierList { get; set; }  //["M1","M2", ��]

    public string ObjectList { get; set; }  //�Զ������ҵ�

    public override string ToString()
    {
        return $"[Card: Id={CardId}, Name={CardName}, Cost={Cost}, Cold={ColdTime}, Wait={WaitTime}]";
    }
}
