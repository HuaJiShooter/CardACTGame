using SQLite4Unity3d;

[Table("ModifiersTable")]
public class ModifiersTable
{
    [PrimaryKey, AutoIncrement]              //modifier_ID
    public int ModifierId { get; set; }

    [MaxLength(64)]                          //modifier_name
    public string ModifierName { get; set; }

    [MaxLength(1024)]                        //description
    public string Description { get; set; }

    public string ConditionList { get; set; }   //JSON: ["SameCard","EnoughMana"]
    public string EffectList { get; set; }   //JSON: ["Deal20","SetCost"]

    public override string ToString()
    {
        return $"[Modifier: Id={ModifierId}, Name={ModifierName}]";
    }
}